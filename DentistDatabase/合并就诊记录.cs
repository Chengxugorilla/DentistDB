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
    public partial class 合并就诊记录 : Form
    {
        public 合并就诊记录(String PatientID)
        {
            this.PtID = PatientID;
            InitializeComponent();
        }
        public String PtID { get; set; }

        private void 合并就诊记录_Load(object sender, EventArgs e)
        {
            BindPatients2Dgv(dgv);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BindPatients2Dgv(DataGridView dgv)
        {
            String sqlText = @"SELECT *
                                FROM (SELECT 诊疗 = '初诊', 牙位 = SUBSTRING(初诊.牙位ID, 6,2),日期 = 初诊日期, 就诊记录
                                FROM 初诊
                                WHERE 牙位ID in(SELECT 牙位ID
                                FROM [牙位-患者表]
                                WHERE 患者代码 = '" + PtID + @"')
                                UNION
                                SELECT 诊疗 = '谈方案', 牙位 = '',谈方案日期, 就诊记录
                                FROM 谈方案
                                WHERE 患者代码 = '" + PtID + @"'
                                UNION
                                SELECT 诊疗 = '1期',牙位 = SUBSTRING(牙位ID, 6,2),手术日期, 就诊记录
                                FROM 一期信息
                                WHERE 牙位ID in(SELECT 牙位ID
                                FROM [牙位-患者表]
                                WHERE 患者代码 = '" + PtID + @"')
                                UNION
                                SELECT 诊疗 = '取模',牙位 = SUBSTRING(牙位ID, 6,2),日期, 就诊记录
                                FROM 取模
                                WHERE 牙位ID in(SELECT 牙位ID
                                FROM [牙位-患者表]
                                WHERE 患者代码 = '" + PtID + @"')
                                UNION
                                SELECT 诊疗 = '取模',牙位 = SUBSTRING(牙位ID, 6,2),日期, 就诊记录
                                FROM 取模
                                WHERE 牙位ID in(SELECT 牙位ID
                                FROM [牙位-患者表]
                                WHERE 患者代码 = '" + PtID + @"')
                                UNION
                                SELECT 诊疗 = '过渡义齿',牙位 = SUBSTRING(牙位ID, 6,2),日期, 就诊记录
                                FROM 过渡义齿
                                WHERE 牙位ID in(SELECT 牙位ID
                                FROM [牙位-患者表]
                                WHERE 患者代码 = '" + PtID + @"')
                                UNION
                                SELECT 诊疗 = '戴牙',牙位 = SUBSTRING(牙位ID, 6,2),日期, 就诊记录
                                FROM 戴牙
                                WHERE 牙位ID in(SELECT 牙位ID
                                FROM [牙位-患者表]
                                WHERE 患者代码 = '" + PtID + @"')
                                UNION
                                SELECT 诊疗 = '戴牙后复诊',牙位 = SUBSTRING(牙位ID, 6,2),日期, 就诊记录
                                FROM 戴牙后复诊
                                WHERE 牙位ID in(SELECT 牙位ID
                                FROM [牙位-患者表]
                                WHERE 患者代码 = '" + PtID + @"')
                                ) AS 合并就诊记录
                                ORDER BY case 诊疗 WHEN '初诊' then 1 
                                                    WHEN '谈方案' then 2 
                                                    WHEN '1期' then 3 
                                                    WHEN '2期' then 4 
                                                    WHEN '取模' then 5 
                                                    WHEN '过渡义齿' then 6 
                                                    WHEN '戴牙' then 7 
                                                    WHEN '戴牙后复诊' then 8 END";

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            主界面 main = new 主界面();
            main.Show();
        }
    }
}
