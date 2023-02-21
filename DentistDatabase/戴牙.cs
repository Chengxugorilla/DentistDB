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
    public partial class 戴牙 : Form
    {
        public 戴牙(String ID)
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

        private void 戴牙_Load(object sender, EventArgs e)
        {
            BindData2Dgv(dataGridView1);
        }

        private void BindData2Dgv(DataGridView dgv)
        {
            String sqlText = @"SELECT  [牙位-患者表].种植牙位, 戴牙.就诊记录, 戴牙.日期, 戴牙.基台编号, 戴牙.基台类型, 
                   戴牙.基台直径, 戴牙.基台高度, 戴牙.连接类型, 戴牙.修复体类型, 戴牙.是否被动就位, 
                   戴牙.调合, 戴牙.戴牙后备注, 戴牙.是否拍戴牙CBCT, 戴牙.是否拍戴牙小牙片, 
                   戴牙.是否拍戴牙全景片, 戴牙.[临床照片（多张）]
                                FROM      [牙位-患者表] INNER JOIN
                   档案目录 ON [牙位-患者表].患者代码 = 档案目录.患者代码 INNER JOIN
                   戴牙 ON [牙位-患者表].牙位ID = 戴牙.牙位ID
                                WHERE 档案目录.患者代码='" + ID + "'";

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
                    strSQL = "";
                }
                else if (dr.RowState == System.Data.DataRowState.Deleted) //删除
                {
                    strSQL = "";
                }
                else if (dr.RowState == System.Data.DataRowState.Modified) //修改
                {
                    strSQL = @"UPDATE [dbo].[戴牙]
                               SET [就诊记录] = '" + dr["就诊记录"].ToString() + @"'
                                  ,[日期] = '" + dr["日期"].ToString() + @"'
                                  ,[基台编号] = '" + dr["基台编号"].ToString() + @"'
                                  ,[基台类型] = '" + dr["基台类型"].ToString() + @"'
                                  ,[基台直径] = '" + dr["基台直径"].ToString() + @"'
                                  ,[基台高度] = '" + dr["基台高度"].ToString() + @"'
                                  ,[连接类型] = '" + dr["连接类型"].ToString() + @"'
                                  ,[修复体类型] = '" + dr["修复体类型"].ToString() + @"'
                                  ,[是否被动就位] = '" + dr["是否被动就位"].ToString() + @"'
                                  ,[调合] = '" + dr["调合"].ToString() + @"'
                                  ,[戴牙后备注] = '" + dr["戴牙后备注"].ToString() + @"'
                                  ,[是否拍戴牙CBCT] = '" + dr["是否拍戴牙CBCT"].ToString() + @"'
                                  ,[是否拍戴牙小牙片] = '" + dr["是否拍戴牙小牙片"].ToString() + @"'
                                  ,[是否拍戴牙全景片] = '" + dr["是否拍戴牙全景片"].ToString() + @"'
                                  ,[临床照片（多张）] = '" + dr["临床照片（多张）"].ToString() + @"'
                              WHERE 牙位ID = (SELECT  牙位ID
					                FROM [牙位-患者表]
					                WHERE 患者代码 = '" + ID + "' AND 种植牙位 = '" + dr["种植牙位"].ToString() + "')";
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
