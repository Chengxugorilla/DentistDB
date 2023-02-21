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
    public partial class 注册 : Form
    {
        public 注册()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            登录 fm1 = new 登录();
            fm1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void confirm_Click(object sender, EventArgs e)
        {
            String Doctor = textBox1.Text;
            String PWD = textBox2.Text;
            String PWDC = textBox3.Text;

            if (Doctor == "" || PWD == "" || PWDC == "")
            {
                MessageBox.Show("医生姓名和密码不能为空");
                return;
            }
            if (PWD != PWDC)
            {
                MessageBox.Show("两次输入密码不一致!");
                return; 
            }
            Register_Doctor(textBox1.Text, textBox3.Text);
        }

        private void Register_Doctor(String Name, String PWD)
        {
            String sqlText = "INSERT INTO 医生表 VALUES ('" + Name + "', '" + PWD + "')";

            OleDbConnection conn = new OleDbConnection(DBInfo.ConnectString);//创建一个新的连接
            OleDbCommand cmd = new OleDbCommand();//OleDbCommand表示要对数据源执行的sql语句或存储过程；初始化此实例
            OleDbTransaction trans = null;
            try
            {
                conn.Open();//打开连接
                
                trans = conn.BeginTransaction();
                
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = sqlText;   //连接到数据库并执行SQL语言
                
                int result = cmd.ExecuteNonQuery();  //更新都用这个语句。

                trans.Commit();

                MessageBox.Show("注册成功");
                this.Close();
                登录 sign_In = new 登录();
                sign_In.Show();
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
}
