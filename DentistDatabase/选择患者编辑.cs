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
    public partial class 选择患者编辑 : Form
    {
        public static String PatientID;
        String name;

        public 选择患者编辑()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;
            BindPatients2Dgv(dgv);
        }

        private void BindPatients2Dgv(DataGridView dgv)
        {
            if (!(textBox1.Text == ""))
            {
                String sqlText = @"SELECT  患者代码, 姓名, 性别, 出生年月, 职业, 住址, 出生地
                                    FROM 档案目录
                                    WHERE 姓名 = '" + name + "'";

                OleDbConnection conn = new OleDbConnection(DBInfo.ConnectString);//创建一个新的连接
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
                        //  dt = ds.Tables[0];
                        dgv.DataSource = ds.Tables[0];  //传递数据到界面上的DataGridView控件
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
        }

        private void 选择患者编辑_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dgv.Rows.Count != 0)
            {
                PatientID = dgv.CurrentRow.Cells["患者代码"].Value.ToString();
                this.Close();
                患者编辑 edit = new 患者编辑(PatientID);
                edit.Show();
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            主界面 main = new 主界面();
            main.Show();
        }
    }
}
