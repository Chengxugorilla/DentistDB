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
    public partial class 一期 : Form
    {
        public 一期(String ID)
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

        private void 一期_Load(object sender, EventArgs e)
        {
            BindData2Dgv(dataGridView1);
        }

        private void BindData2Dgv(DataGridView dgv)
        {
            String sqlText = @"SELECT  [牙位-患者表].种植牙位, 一期信息.就诊记录, 一期信息.手术日期, 一期信息.骨质类型, 一期信息.牙龈厚度, 一期信息.种植体品牌,
                   一期信息.种植体型号, 一期信息.种植体直径, 一期信息.种植体长度, 一期信息.植入扭矩, 一期信息.愈合方式, 一期信息.覆盖螺丝尺寸, 一期信息.愈合基台型号,
                   愈合基台表.愈合基台直径, 愈合基台表.愈合基台高度, 一期信息.是否植骨, 一期信息.植骨术式, 一期信息.植骨量, 一期信息.植骨材料品牌,
                   一期信息.是否使用屏障膜, 一期信息.屏障膜长度, 一期信息.屏障膜宽度, 一期信息.屏障膜品牌, 一期信息.伤口愈合情况, 一期信息.是否做即刻修复, 一期信息.是否做数字化导板, 一期信息.导板制作公司, 
                   一期信息.导板打印日期, 一期信息.导板使用日期, 一期信息.导板就位情况, 
                   一期信息.导板协议是否改动, 一期信息.后续是否有拍CT进行前后对比, 一期信息.导板设计类型, 
                   一期信息.导板使用方式, 一期信息.有无混合自由手, 一期信息.特殊点, 一期信息.伤口愈合情况, 
                   一期信息.是否拍1期CBCT, 一期信息.[1期CBCT链接], 一期信息.是否拍1期小牙片, 一期信息.[1期小牙片链接], 一期信息.是否拍1期全景片,
                   一期信息.[1期全景片链接], 一期信息.是否拍1期手术视频, 一期信息.[1期手术视频链接], 
                   一期信息.[临床照片（多张）], 一期信息.病历, 一期信息.病历链接  " +
                                "FROM [牙位-患者表] INNER JOIN 档案目录 ON [牙位-患者表].患者代码 = 档案目录.患者代码 INNER JOIN 一期信息 ON [牙位-患者表].牙位ID = 一期信息.牙位ID LEFT OUTER JOIN 愈合基台表 ON 一期信息.愈合基台型号 = 愈合基台表.愈合基台型号 " +
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
                    strSQL = @" IF((SELECT COUNT(*) 
                                    FROM 一期信息
                                    WHERE 牙位ID = '" + ID + dr["种植牙位"].ToString() + @"') = 1)
                                BEGIN
                                    RAISERROR('一期信息表中已存在数据', 16, 1)
                                END
                                ELSE
                                BEGIN
                                    IF((SELECT COUNT(*)
                                        FROM [牙位-患者表]
                                        WHERE 牙位ID = '" + ID + dr["种植牙位"].ToString() + @"') = 0)
                                    BEGIN
                                     INSERT INTO [dbo].[牙位-患者表]
                                           ([牙位ID]
                                           ,[患者代码]
                                           ,[种植牙位])
                                     VALUES
                                           ( '" + ID + dr["种植牙位"].ToString() + @"'
                                           ,'" + ID.ToString() + @"'
                                           ,'" + dr["种植牙位"].ToString() + @"')
                                    INSERT INTO [dbo].[一期信息]
                                           ([牙位ID]
                                           ,[就诊记录]
                                           ,[手术日期]
                                           ,[骨质类型]
                                           ,[牙龈厚度]
                                           ,[种植体品牌]
                                           ,[种植体型号]
                                           ,[种植体直径]
                                           ,[种植体长度]
                                           ,[植入扭矩]
                                           ,[愈合方式]
                                           ,[覆盖螺丝尺寸]
                                           ,[愈合基台型号]
                                           ,[是否植骨]
                                           ,[植骨术式]
                                           ,[植骨量]
                                           ,[植骨材料品牌]
                                           ,[是否使用屏障膜]
                                           ,[屏障膜长度]
                                           ,[屏障膜宽度]
                                           ,[屏障膜品牌]
                                           ,[是否做即刻修复]
                                           ,[是否做数字化导板]
                                           ,[导板制作公司]
                                           ,[导板打印日期]
                                           ,[导板使用日期]
                                           ,[导板就位情况]
                                           ,[导板协议是否改动]
                                           ,[后续是否有拍CT进行前后对比]
                                           ,[导板设计类型]
                                           ,[导板使用方式]
                                           ,[有无混合自由手]
                                           ,[特殊点]
                                           ,[伤口愈合情况]
                                           ,[是否拍1期CBCT]
                                           ,[1期CBCT链接]
                                           ,[是否拍1期小牙片]
                                           ,[1期小牙片链接]
                                           ,[是否拍1期全景片]
                                           ,[1期全景片链接]
                                           ,[是否拍1期手术视频]
                                           ,[1期手术视频链接]
                                           ,[临床照片（多张）]
                                           ,[病历]
                                           ,[病历链接])
                                     VALUES
                                           ('" + ID + dr["种植牙位"].ToString() + @"'
                                           ,'" + dr["就诊记录"].ToString() + @"'
                                           ,'" + dr["手术日期"].ToString() + @"'
                                           ,'" + dr["骨质类型"].ToString() + @"'
                                           ,'" + dr["牙龈厚度"].ToString() + @"'
                                           ,'" + dr["种植体品牌"].ToString() + @"'
                                           ,'" + dr["种植体型号"].ToString() + @"'
                                           ,'" + dr["种植体直径"].ToString() + @"'
                                           ,'" + dr["种植体长度"].ToString() + @"'
                                           ,'" + dr["植入扭矩"].ToString() + @"'
                                           ,'" + dr["愈合方式"].ToString() + @"'
                                           ,'" + dr["覆盖螺丝尺寸"].ToString() + @"'
                                           ,'" + dr["愈合基台型号"].ToString() + @"'
                                           ,'" + dr["是否植骨"].ToString() + @"'
                                           ,'" + dr["植骨术式"].ToString() + @"'
                                           ,'" + dr["植骨量"].ToString() + @"'
                                           ,'" + dr["植骨材料品牌"].ToString() + @"'
                                           ,'" + dr["是否使用屏障膜"].ToString() + @"'
                                           ,'" + dr["屏障膜长度"].ToString() + @"'
                                           ,'" + dr["屏障膜宽度"].ToString() + @"'
                                           ,'" + dr["屏障膜品牌"].ToString() + @"'
                                           ,'" + dr["是否做即刻修复"].ToString() + @"'
                                           ,'" + dr["是否做数字化导板"].ToString() + @"'
                                           ,'" + dr["导板制作公司"].ToString() + @"'
                                           ,'" + dr["导板打印日期"].ToString() + @"'
                                           ,'" + dr["导板使用日期"].ToString() + @"'
                                           ,'" + dr["导板就位情况"].ToString() + @"'
                                           ,'" + dr["导板协议是否改动"].ToString() + @"'
                                           ,'" + dr["后续是否有拍CT进行前后对比"].ToString() + @"'
                                           ,'" + dr["导板设计类型"].ToString() + @"'
                                           ,'" + dr["导板使用方式"].ToString() + @"'
                                           ,'" + dr["有无混合自由手"].ToString() + @"'
                                           ,'" + dr["特殊点"].ToString() + @"'
                                           ,'" + dr["伤口愈合情况"].ToString() + @"'
                                           ,'" + dr["是否拍1期CBCT"].ToString() + @"'
                                           ,'" + dr["1期CBCT链接"].ToString() + @"'
                                           ,'" + dr["是否拍1期小牙片"].ToString() + @"'
                                           ,'" + dr["1期小牙片链接"].ToString() + @"'
                                           ,'" + dr["是否拍1期全景片"].ToString() + @"'
                                           ,'" + dr["1期全景片链接"].ToString() + @"'
                                           ,'" + dr["是否拍1期手术视频"].ToString() + @"'
                                           ,'" + dr["1期手术视频链接"].ToString() + @"'
                                           ,'" + dr["临床照片（多张）"].ToString() + @"'
                                           ,'" + dr["病历"].ToString() + @"'
                                           ,'" + dr["病历链接"].ToString() + @"')
                                       END
                                      ELSE
                                      BEGIN
                                      INSERT INTO [dbo].[一期信息]
                                           ([牙位ID]
                                           ,[就诊记录]
                                           ,[手术日期]
                                           ,[骨质类型]
                                           ,[牙龈厚度]
                                           ,[种植体品牌]
                                           ,[种植体型号]
                                           ,[种植体直径]
                                           ,[种植体长度]
                                           ,[植入扭矩]
                                           ,[愈合方式]
                                           ,[覆盖螺丝尺寸]
                                           ,[愈合基台型号]
                                           ,[是否植骨]
                                           ,[植骨术式]
                                           ,[植骨量]
                                           ,[植骨材料品牌]
                                           ,[是否使用屏障膜]
                                           ,[屏障膜长度]
                                           ,[屏障膜宽度]
                                           ,[屏障膜品牌]
                                           ,[是否做即刻修复]
                                           ,[是否做数字化导板]
                                           ,[导板制作公司]
                                           ,[导板打印日期]
                                           ,[导板使用日期]
                                           ,[导板就位情况]
                                           ,[导板协议是否改动]
                                           ,[后续是否有拍CT进行前后对比]
                                           ,[导板设计类型]
                                           ,[导板使用方式]
                                           ,[有无混合自由手]
                                           ,[特殊点]
                                           ,[伤口愈合情况]
                                           ,[是否拍1期CBCT]
                                           ,[1期CBCT链接]
                                           ,[是否拍1期小牙片]
                                           ,[1期小牙片链接]
                                           ,[是否拍1期全景片]
                                           ,[1期全景片链接]
                                           ,[是否拍1期手术视频]
                                           ,[1期手术视频链接]
                                           ,[临床照片（多张）]
                                           ,[病历]
                                           ,[病历链接])
                                     VALUES
                                           ('" + ID + dr["种植牙位"].ToString() + @"'
                                           ,'" + dr["就诊记录"].ToString() + @"'
                                           ,'" + dr["手术日期"].ToString() + @"'
                                           ,'" + dr["骨质类型"].ToString() + @"'
                                           ,'" + dr["牙龈厚度"].ToString() + @"'
                                           ,'" + dr["种植体品牌"].ToString() + @"'
                                           ,'" + dr["种植体型号"].ToString() + @"'
                                           ,'" + dr["种植体直径"].ToString() + @"'
                                           ,'" + dr["种植体长度"].ToString() + @"'
                                           ,'" + dr["植入扭矩"].ToString() + @"'
                                           ,'" + dr["愈合方式"].ToString() + @"'
                                           ,'" + dr["覆盖螺丝尺寸"].ToString() + @"'
                                           ,'" + dr["愈合基台型号"].ToString() + @"'
                                           ,'" + dr["是否植骨"].ToString() + @"'
                                           ,'" + dr["植骨术式"].ToString() + @"'
                                           ,'" + dr["植骨量"].ToString() + @"'
                                           ,'" + dr["植骨材料品牌"].ToString() + @"'
                                           ,'" + dr["是否使用屏障膜"].ToString() + @"'
                                           ,'" + dr["屏障膜长度"].ToString() + @"'
                                           ,'" + dr["屏障膜宽度"].ToString() + @"'
                                           ,'" + dr["屏障膜品牌"].ToString() + @"'
                                           ,'" + dr["是否做即刻修复"].ToString() + @"'
                                           ,'" + dr["是否做数字化导板"].ToString() + @"'
                                           ,'" + dr["导板制作公司"].ToString() + @"'
                                           ,'" + dr["导板打印日期"].ToString() + @"'
                                           ,'" + dr["导板使用日期"].ToString() + @"'
                                           ,'" + dr["导板就位情况"].ToString() + @"'
                                           ,'" + dr["导板协议是否改动"].ToString() + @"'
                                           ,'" + dr["后续是否有拍CT进行前后对比"].ToString() + @"'
                                           ,'" + dr["导板设计类型"].ToString() + @"'
                                           ,'" + dr["导板使用方式"].ToString() + @"'
                                           ,'" + dr["有无混合自由手"].ToString() + @"'
                                           ,'" + dr["特殊点"].ToString() + @"'
                                           ,'" + dr["伤口愈合情况"].ToString() + @"'
                                           ,'" + dr["是否拍1期CBCT"].ToString() + @"'
                                           ,'" + dr["1期CBCT链接"].ToString() + @"'
                                           ,'" + dr["是否拍1期小牙片"].ToString() + @"'
                                           ,'" + dr["1期小牙片链接"].ToString() + @"'
                                           ,'" + dr["是否拍1期全景片"].ToString() + @"'
                                           ,'" + dr["1期全景片链接"].ToString() + @"'
                                           ,'" + dr["是否拍1期手术视频"].ToString() + @"'
                                           ,'" + dr["1期手术视频链接"].ToString() + @"'
                                           ,'" + dr["临床照片（多张）"].ToString() + @"'
                                           ,'" + dr["病历"].ToString() + @"'
                                           ,'" + dr["病历链接"].ToString() + @"')
                                      END
                                      END";
                }
                else if (dr.RowState == System.Data.DataRowState.Deleted) //删除
                {
                    strSQL = @"DELETE FROM [dbo].[一期信息]
                                WHERE 牙位ID = '" + ID + dr["种植牙位", DataRowVersion.Original].ToString() + "'";
                }
                else if (dr.RowState == System.Data.DataRowState.Modified) //修改
                {
                    strSQL = @"UPDATE [一期信息]
                               SET [就诊记录] = '" + dr["就诊记录"].ToString() + @"'
                                  ,[手术日期] = '" + dr["手术日期"].ToString() + @"'
                                  ,[骨质类型] = '" + dr["骨质类型"].ToString() + @"'
                                  ,[牙龈厚度] = '" + dr["牙龈厚度"].ToString() + @"'
                                  ,[种植体品牌] = '" + dr["种植体品牌"].ToString() + @"'
                                  ,[种植体型号] = '" + dr["种植体型号"].ToString() + @"'
                                  ,[种植体直径] = '" + dr["种植体直径"].ToString() + @"'
                                  ,[种植体长度] = '" + dr["种植体长度"].ToString() + @"'
                                  ,[植入扭矩] = '" + dr["植入扭矩"].ToString() + @"'
                                  ,[愈合方式] = '" + dr["愈合方式"].ToString() + @"'
                                  ,[覆盖螺丝尺寸] = '" + dr["覆盖螺丝尺寸"].ToString() + @"'
                                  ,[愈合基台型号] = '" + dr["愈合基台型号"].ToString() + @"'
                                  ,[是否植骨] = '" + dr["是否植骨"].ToString() + @"'
                                  ,[植骨术式] = '" + dr["植骨术式"].ToString() + @"'
                                  ,[植骨量] = '" + dr["植骨量"].ToString() + @"'
                                  ,[植骨材料品牌] = '" + dr["植骨材料品牌"].ToString() + @"'
                                  ,[是否使用屏障膜] = '" + dr["是否使用屏障膜"].ToString() + @"'
                                  ,[屏障膜长度] = '" + dr["屏障膜长度"].ToString() + @"'
                                  ,[屏障膜宽度] = '" + dr["屏障膜宽度"].ToString() + @"'
                                  ,[屏障膜品牌] = '" + dr["屏障膜品牌"].ToString() + @"'
                                  ,[是否做即刻修复] = '" + dr["是否做即刻修复"].ToString() + @"'
                                  ,[是否做数字化导板] = '" + dr["是否做数字化导板"].ToString() + @"'
                                  ,[导板制作公司] = '" + dr["导板制作公司"].ToString() + @"'
                                  ,[导板打印日期] = '" + dr["导板打印日期"].ToString() + @"'
                                  ,[导板使用日期] = '" + dr["导板使用日期"].ToString() + @"'
                                  ,[导板就位情况] = '" + dr["导板就位情况"].ToString() + @"'
                                  ,[导板协议是否改动] = '" + dr["导板协议是否改动"].ToString() + @"'
                                  ,[后续是否有拍CT进行前后对比] = '" + dr["后续是否有拍CT进行前后对比"].ToString() + @"'
                                  ,[导板设计类型] = '" + dr["导板设计类型"].ToString() + @"'
                                  ,[导板使用方式] = '" + dr["导板使用方式"].ToString() + @"'
                                  ,[有无混合自由手] = '" + dr["有无混合自由手"].ToString() + @"'
                                  ,[特殊点] = '" + dr["特殊点"].ToString() + @"'
                                  ,[伤口愈合情况] = '" + dr["伤口愈合情况"].ToString() + @"'
                                  ,[是否拍1期CBCT] = '" + dr["是否拍1期CBCT"].ToString() + @"'
                                  ,[1期CBCT链接] = '" + dr["1期CBCT链接"].ToString() + @"'
                                  ,[是否拍1期小牙片] = '" + dr["是否拍1期小牙片"].ToString() + @"'
                                  ,[1期小牙片链接] = '" + dr["1期小牙片链接"].ToString() + @"'
                                  ,[是否拍1期全景片] = '" + dr["是否拍1期全景片"].ToString() + @"'
                                  ,[1期全景片链接] = '" + dr["1期全景片链接"].ToString() + @"'
                                  ,[是否拍1期手术视频] = '" + dr["是否拍1期手术视频"].ToString() + @"'
                                  ,[1期手术视频链接] = '" + dr["1期手术视频链接"].ToString() + @"'
                                  ,[临床照片（多张）] = '" + dr["临床照片（多张）"].ToString() + @"'
                                  ,[病历] = '" + dr["病历"].ToString() + @"'
                                  ,[病历链接] = '" + dr["病历链接"].ToString() + @"'
                             WHERE 牙位ID = (SELECT  牙位ID
					                FROM [牙位-患者表]
					                WHERE 患者代码 = '" + ID + "' AND 种植牙位 = '" + dr["种植牙位", DataRowVersion.Original].ToString() + "')";
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
