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
    public partial class 登录 : Form
    {
        public static String Doctor; 
        public 登录()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Doctor = textBox1.Text;
            String PWD = textBox2.Text;

            if (Doctor == "" || PWD == "")
            {
                MessageBox.Show("医生姓名和密码不能为空");
                return;
            }
            if (!CheckDocAndPWD(Doctor, PWD))
            {
                MessageBox.Show("用户名或密码错误");
            }
            if (CheckDocAndPWD(Doctor, PWD))
            {
                this.Close();
                主界面 fm2 = new 主界面();
                fm2.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            注册 register = new 注册();
            register.Show();
        }

        public bool CheckDocAndPWD(String userid, String pass)
        {
            //数据库连接,SQL验证和windows验证
            OleDbConnection conn = new OleDbConnection(DBInfo.ConnectString);
            //定义数据库查询
            String sqlText = "select * from 医生表 where 医生姓名 = '" + userid + "' and  登录密码 ='" + pass + "'";

            //定义数据集对象
            System.Data.DataTable dt = null;
            //定义command对象，开始查询
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                conn.Open();    //打开数据库
                cmd.Connection = conn;
                cmd.CommandText = sqlText;

                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                    return true;
                else return false;
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
