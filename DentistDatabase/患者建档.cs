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
    public partial class 患者建档 : Form
    {
        public 患者建档()
        {
            InitializeComponent();
        }

        private void BuildArchive_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            主界面 main = new 主界面();
            main.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = textName.Text.Trim();
            int sex = 1;
            DateTime birth = textBirth.Value;
            String vocation = textVocation.Text;
            String address = textAddress.Text;
            String birthplace = textBirthPlace.Text;
            String doctor = 登录.Doctor;
            //String disease = textDisease.Text;
            //String lostside = textLostSide.Text;
            //DateTime lostdate = textLostDate.Value;
            //String lostreason = textLostReason.Text;

            if (name.Length == 0)
            {
                MessageBox.Show("姓名不能为空", "提示");
                return;
            }

            if (textSex.Text == "男")
                sex = 1;
            else if (textSex.Text == "女")
                sex = 0;

            String sqlText = "";
            OleDbConnection conn = new OleDbConnection(DBInfo.ConnectString);
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();


                cmd.Connection = conn;
                cmd.Transaction = trans;
                sqlText = "EXECUTE 建档 @姓名='" + name + "',@性别='" + sex + "',@出生年月='" + birth + "',@职业 = '" + vocation + "',@住址 = '" + address + "',@出生地 = '" + birthplace + "',@负责医生 = '" + doctor + "'";
                cmd.CommandText = sqlText;

                int result = cmd.ExecuteNonQuery();  //更新都用这个语句。

                trans.Commit();

                MessageBox.Show("添加成功");

                this.Close();
                主界面 main = new 主界面();
                main.Show();

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textDisease_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String name = textName.Text.Trim();
            int sex = 1;
            DateTime birth = textBirth.Value;
            String vocation = textVocation.Text;
            String address = textAddress.Text;
            String birthplace = textBirthPlace.Text;
            String doctor = 登录.Doctor;
            //String disease = textDisease.Text;
            //String lostside = textLostSide.Text;
            //String lostsite = textLostSite.Text;
            //DateTime lostdate = textLostDate.Value;
            //String lostreason = textLostReason.Text;

            if (name.Length == 0)
            {
                MessageBox.Show("姓名不能为空", "提示");
                return;
            }

            if (textSex.Text == "男")
                sex = 1;
            else if (textSex.Text == "女")
                sex = 0;

            String sqlText = "";
            OleDbConnection conn = new OleDbConnection(DBInfo.ConnectString);
            OleDbCommand cmd = new OleDbCommand();
            OleDbTransaction trans = null;
            try
            {
                conn.Open();

                trans = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = trans;
                sqlText = "EXECUTE 建档 @姓名='" + name + "',@性别='" + sex + "',@出生年月='" + birth + "',@职业 = '" + vocation + "',@住址 = '" + address + "',@出生地 = '" + birthplace + "',@负责医生 = '" + doctor + "'";
                cmd.CommandText = sqlText;

                int result = cmd.ExecuteNonQuery();  //更新都用这个语句。

                trans.Commit();

                MessageBox.Show("添加成功");

                this.Close();
                患者编辑 edit = new 患者编辑();
                edit.Show();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new ApplicationException("执行SQL语句异常:" + ex.Message + "(" + sqlText + ")");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
