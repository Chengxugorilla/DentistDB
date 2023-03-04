using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentistDatabase
{
    public partial class 主界面 : Form
    {
        public 主界面()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            查询 frm3 = new 查询();
            frm3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            患者建档 bda = new 患者建档();
            bda.Show();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
            登录 sign_In = new 登录();
            sign_In.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            选择患者编辑 updateArchive = new 选择患者编辑();
            updateArchive.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            查询 qt= new 查询();
            qt.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            就诊记录查询 rs = new 就诊记录查询();
            rs.Show();
        }
    }
}
