using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DentistDatabase
{
    public partial class 患者个性化标签库 : Form
    {
        public 患者个性化标签库(String ID)
        {
            InitializeComponent();
            this.ID = ID;
        }
        public String ID { get; set; }
        private static System.Data.DataTable dt = null;
        private static OleDbConnection conn;
        private static System.Data.DataTable changeDt = null;

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 特殊患者_Load(object sender, EventArgs e)
        {
            BindData2Dgv(dataGridView1);
        }

        private void BindData2Dgv(DataGridView dgv)
        {
            String sqlText = @"SELECT  患者个性化标签库.特殊患者类型, 患者个性化标签库.特殊描述, 患者个性化标签库.新增标签功能 " +
                                "FROM 档案目录 INNER JOIN 患者个性化标签库 ON 档案目录.患者代码 = 患者个性化标签库.患者代码 " +
                                "WHERE 档案目录.患者代码='" + ID + "'";

            conn = new OleDbConnection(DBInfo.ConnectString);//创建一个新的连接
            OleDbCommand cmd = new OleDbCommand();//OleDbCommand表示要对数据源执行的sql语句或存储过程；初始化此实例
            try
            {
                conn.Open();//打开连接
                cmd.Connection = conn;
                cmd.CommandText = sqlText;   //连接到数据库并执行SQL语言

                DataSet ds = new DataSet(); //new一个数据集实例
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);//OleDbDataAdapter 充当 DataSet 和数据源之间的桥梁，用于检索和保存数据
                adapter.Fill(ds);//使用 Fill 将数据从数据源加载到 DataSet 中
                //Fill()方法有一个重载版本，就是Fill(DataSet ds,Tables tablesName),可见，它需要两个参数，第一个参数是DataSet对象，不可省略.
                //第二参数是“表名”对象（这个表名是自己给命名的），如果省略的话，电脑默认把这个表从零“0”开始储存，
                //即有了ds.Tables[0]，当然这个对象可以替换成在使用Fill（）方法时命名的表名。
                //例如：用Fill()填充DataSet对象写作：Fill(ds,"MyNewTable"),
                //这样就看出 MyNewTable其实是自己随便取的名字,再读取的时候当然就得用：
                //ds.Tables["MyNewTable"].
                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];

                dgv.DataSource = dt;  //传递数据到界面上的DataGridView控件
            }
            catch (Exception e)
            {
                throw new ApplicationException("获取DataSet查询异常：" + e.Message + "(" + sqlText + ")");
            }
            finally
            {
                conn.Close();
            }
        }

        public static void Save(String ID)
        {
            conn = new OleDbConnection(DBInfo.ConnectString);
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction trans = null;

            changeDt = dt.GetChanges();
            if (changeDt == null)
                return;
            foreach (DataRow dr in changeDt.Rows)
            {
                String strSQL = string.Empty;
                if (dr.RowState == System.Data.DataRowState.Added) //增加
                {
                    strSQL = @"IF((SELECT COUNT(*)
                                    FROM 患者个性化标签库
                                    WHERE 患者代码 = '" + ID + "' AND 特殊患者类型 = '" + dr["特殊患者类型"].ToString() + @"') = 1)
                               BEGIN
                                    RAISERROR('个性化标签库中已存在数据', 16, 1)
                               END
                               ELSE
                               BEGIN
                                 INSERT INTO [dbo].[患者个性化标签库]
                                       ([患者代码]
                                       ,[特殊患者类型]
                                       ,[特殊描述]
                                       ,[新增标签功能])
                                 VALUES
                                       ('" + ID + @"'
                                       ,'" + dr["特殊患者类型"].ToString() + @"'
                                       ,'" + dr["特殊描述"].ToString() + @"'
                                       ,'" + dr["新增标签功能"].ToString() + @"')
                               END";
                }
                else if (dr.RowState == System.Data.DataRowState.Deleted) //删除
                {
                    strSQL = @"DELETE FROM[dbo].[患者个性化标签库]
                               WHERE 患者代码 = '" + ID + "' AND 特殊患者类型 = '" + dr["特殊患者类型", DataRowVersion.Original].ToString() + "'";
                }
                else if (dr.RowState == System.Data.DataRowState.Modified) //修改
                {
                    strSQL = @"UPDATE 患者个性化标签库
                               SET [特殊患者类型] = '" + dr["特殊患者类型"].ToString() + @"'
                                  ,[特殊描述] = '" + dr["特殊描述"].ToString() + @"'
                                  ,[新增标签功能] = '" + dr["新增标签功能"].ToString() + @"'
                              WHERE 患者代码 = '" + ID + "'";
                }
                try
                {
                    conn.Open();

                    trans = conn.BeginTransaction();

                    cmd.Connection = conn;
                    cmd.CommandText = strSQL;
                    cmd.Transaction = trans;
                    int result = cmd.ExecuteNonQuery();  //更新都用这个语句。

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show(ex.Message);
                    // throw new ApplicationException("执行SQL语句异常:" + ex.Message + "(" + sqlText + ")");
                }
                finally
                {
                    conn.Close();
                }
            }
            MessageBox.Show("suceed!");
            return;
        }
    }
}
