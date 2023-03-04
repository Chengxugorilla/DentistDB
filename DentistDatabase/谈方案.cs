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
    public partial class 谈方案 : Form
    {
        public 谈方案(String ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        public String ID { get; set; }
        private static System.Data.DataTable dt = null;
        private static OleDbConnection conn;
        private static System.Data.DataTable changeDt = null;


        private void 谈方案_Load(object sender, EventArgs e)
        {
            BindData2Dgv(dataGridView1);
        }

        private void BindData2Dgv(DataGridView dgv)
        {
            String sqlText = "SELECT 谈方案.就诊记录, 谈方案.谈方案日期,谈方案.是否谈方案, 谈方案.是否签手术同意书, 谈方案.种植态度, 谈方案.是否种植模拟, 谈方案.病历, 谈方案.病历链接 " +
                "FROM 谈方案 INNER JOIN 档案目录 ON 谈方案.患者代码 = 档案目录.患者代码 " +
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
                    strSQL = @"INSERT INTO [dbo].[谈方案]
                                       ([患者代码]
                                       ,[谈方案日期]
                                       ,[就诊记录]
                                       ,[是否谈方案]
                                       ,[是否签手术同意书]
                                       ,[种植态度]
                                       ,[是否种植模拟]
                                       ,[病历]
                                       ,[病历链接])
                                 VALUES
                                       ('" + ID  + @"'
                                       ,'" + dr["谈方案日期"].ToString() + @"'
                                       ,'" + dr["就诊记录"].ToString() + @"'
                                       ,'" + dr["是否谈方案"].ToString() + @"'
                                       ,'" + dr["是否签手术同意书"].ToString() + @"'
                                       ,'" + dr["种植态度"].ToString() + @"'
                                       ,'" + dr["是否种植模拟"].ToString() + @"'
                                       ,'" + dr["病历"].ToString() + @"'
                                       ,'" + dr["病历链接"].ToString() + "')";
                }
                else if (dr.RowState == System.Data.DataRowState.Deleted) //删除
                {
                    strSQL = @"DELETE FROM [dbo].[谈方案]
                                WHERE 患者代码 = '" + ID + "' AND 谈方案日期 = '" + dr["谈方案日期", DataRowVersion.Original].ToString() + "'";
                }
                else if (dr.RowState == System.Data.DataRowState.Modified) //修改
                {
                    strSQL = @"UPDATE [dbo].[谈方案]
                               SET [就诊记录] =  '" + dr["就诊记录"].ToString() + @"'
                                  ,[谈方案日期] = '" + dr["谈方案日期"].ToString() + @"'
                                  ,[是否谈方案] = '" + dr["是否谈方案"].ToString() + @"'
                                  ,[是否签手术同意书] = '" + dr["是否签手术同意书"].ToString() + @"'
                                  ,[种植态度] = '" + dr["种植态度"].ToString() + @"'
                                  ,[是否种植模拟] = '" + dr["是否种植模拟"].ToString() + @"'
                                  ,[病历] = '" + dr["病历"].ToString() + @"'
                                  ,[病历链接] = '" + dr["病历链接"].ToString() + @"'
                              WHERE 患者代码='" + ID + "' AND 谈方案日期 = '" + dr["谈方案日期", DataRowVersion.Original].ToString() + "'";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
