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
    public partial class 患者编辑 : Form
    {
        public 患者编辑(String ID)
        {
            InitializeComponent();
            this.ID = ID;
        }
        public String ID { get; set; }
        private String stat_tag;
        private void textName_TextChanged(object sender, EventArgs e)
        {

        }

        public 患者编辑()
        {
            InitializeComponent();
        }

        private void UpdateArchive_Load(object sender, EventArgs e)
        {

        }

        private void LoadForm(object Form)
        {
            if (this.MainPanel.Controls.Count > 0)
                this.MainPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.MainPanel.Controls.Add(f);
            this.MainPanel.Tag = f;
            f.Show();
        }

        private void MainPanel_Paint(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadForm(new 过渡义齿(ID));
            stat_tag = "过渡义齿";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadForm(new 初诊(ID));
            stat_tag = "初诊";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadForm(new 谈方案(ID));
            stat_tag = "谈方案";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadForm(new 一期(ID));
            stat_tag = "一期";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadForm(new 二期(ID));
            stat_tag = "二期";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadForm(new 取模(ID));
            stat_tag = "取模";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoadForm(new 戴牙(ID));
            stat_tag = "戴牙";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadForm(new 软硬组织库(ID));
            stat_tag = "软硬组织库";
        }


        private void button11_Click(object sender, EventArgs e)
        {
            LoadForm(new 患者个性化标签库(ID));
            stat_tag = "患者个性化标签库";
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
            主界面 main = new 主界面();
            main.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
        }

        private void button16_Click(object sender, EventArgs e)
        {
            LoadForm(new 戴牙后复诊(ID));
            stat_tag = "戴牙后复诊";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (stat_tag == "档案目录")
            {
                DentistDatabase.档案目录.Save();
                LoadForm(new 档案目录(ID));
            }
            else if (stat_tag == "初诊")
            {
                初诊.Save(ID);
                LoadForm(new 初诊(ID));
            }
            else if (stat_tag == "谈方案")
            {
                谈方案.Save(ID);
                LoadForm(new 谈方案(ID));
            }
            else if (stat_tag == "一期")
            {
                一期.Save(ID);
                LoadForm(new 一期(ID));
            }
            else if (stat_tag == "二期")
            {
                二期.Save(ID);
                LoadForm(new 二期(ID));
            }
            else if (stat_tag == "取模")
            {
                取模.Save(ID);
                LoadForm(new 取模(ID));
            }
            else if (stat_tag == "过渡义齿")
            {
                过渡义齿.Save(ID);
                LoadForm(new 过渡义齿(ID));
            }
            else if (stat_tag == "戴牙")
            {
                戴牙.Save(ID);
                LoadForm(new 戴牙(ID));
            }
            else if (stat_tag == "戴牙后复诊")
            {
                戴牙后复诊.Save(ID);
                LoadForm(new 戴牙后复诊(ID));
            }
            else if (stat_tag == "患者个性化标签库")
            {
                患者个性化标签库.Save(ID);
                LoadForm(new 患者个性化标签库(ID));
            }
            else if (stat_tag == "软硬组织库")
            {
                软硬组织库.Save(ID);
                LoadForm(new 软硬组织库(ID));
            }
        }
    }
}
