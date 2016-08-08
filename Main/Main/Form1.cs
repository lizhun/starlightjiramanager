using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Main
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private Dictionary<string, bool> isDevConfirmedDic = new Dictionary<string, bool>();
        private Dictionary<string, bool> isMoblieConfirmedDic = new Dictionary<string, bool>();
        private Dictionary<string, bool> isSMSDoneDic = new Dictionary<string, bool>();
        private Dictionary<string, bool> isMobileDoneDic = new Dictionary<string, bool>();
        private Dictionary<string, bool> isInPatchDic = new Dictionary<string, bool>();
        private bool NeedCompare = true;
        private DateTime SprintStartDay = DateTime.Now;
        private OpenFileDialog openfile = new OpenFileDialog();
        private void btn_old_Click(object sender, EventArgs e)
        {
            openfile.Multiselect = false;
            var result = openfile.ShowDialog();
            tb_old.Text = openfile.FileName;

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            openfile.Multiselect = false;
            var result = openfile.ShowDialog();
            tb_new.Text = openfile.FileName;
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tb_old.Text))
            {
                MessageBox.Show("请选择文件");
                return;
            }
            if (string.IsNullOrEmpty(tb_new.Text))
            {
                tb_new.Text = tb_old.Text;
                this.NeedCompare = false;
            }
            if (!DateTime.TryParse(this.tb_startDay.Text, out this.SprintStartDay))
            {
                MessageBox.Show("请选择Sprint的开始日期! " + Application.StartupPath + "aaa.xls");
                this.tb_startDay.Focus();
                return;
            }
            // this.SprintStartDay = this.SprintStartDay.AddHours(-13);
            this.btn_run.Enabled = false;
            this.run();

            //Thread thread = new Thread(new ThreadStart(this.run));
            //thread.Start();    

        }
        private void run()
        {
            InitUsers();
            int rowvalue = (int)this.nup_row.Value;
            var dic = new Dictionary<string, Type>();
            dic.Add("Due Date", typeof(DateTime));
            dic.Add("Created", typeof(DateTime));
            var dt = ExcelManager.getDataFormExcel(tb_old.Text);
            dt = ExcelManager.ResetDataTableByRow(dt, rowvalue, dic);
            this.removeGenerateInfo(dt);

            var dt2 = ExcelManager.getDataFormExcel(tb_new.Text);
            dt2 = ExcelManager.ResetDataTableByRow(dt2, rowvalue, dic);
            this.removeGenerateInfo(dt2);


            var manager = new JIRAManaager();
            manager.LoginJira("zhun.li", "168168");
            getJIRAData(dt, manager);
            getJIRAData(dt2, manager);
            this.addPortGroup(dt, "Responsible Developer");
            this.addPortGroup(dt2, "Responsible Developer");

            this.removePatchAndDevConfirmed(dt);
            this.removePatchAndDevConfirmed(dt2);



            DataTable dtadded = ExcelManager.GetAddData(dt, dt2, "KEY");
            DataTable dtremoved = ExcelManager.GetRemoveData(dt, dt2, "KEY");
            DataTable lastdata = mergeAndHandleTable(dtadded, dtremoved);
            lastdata.Columns.Add("oldcount");
            lastdata.Columns.Add("curcount");
            int oldtotalcount = dt.Rows.Count;
            int newtotalcount = dt2.Rows.Count;
            for (int i = 0; i < lastdata.Rows.Count; i++)
            {
                lastdata.Rows[i]["oldcount"] = oldtotalcount;
                lastdata.Rows[i]["curcount"] = newtotalcount;
            }
            //if (dt2.Rows.Count > 5)
            //{
            //    lastdata.Columns.Add("group name");
            //    lastdata.Columns.Add("group count");
            //    lastdata.Rows[0]["group name"] = "Affiliate Group";
            //    lastdata.Rows[1]["group name"] = "Interal Group";
            //    lastdata.Rows[2]["group name"] = "Mobile Group";
            //    lastdata.Rows[3]["group name"] = "API Group";
            //    lastdata.Rows[4]["group name"] = "Client Group";

            //    lastdata.Rows[0]["group count"] = dt2.Select("[Portal Group] = 'Affiliate Group'").Count();
            //    lastdata.Rows[1]["group count"] = dt2.Select("[Portal Group] = 'Interal Group'").Count();
            //    lastdata.Rows[2]["group count"] = dt2.Select("[Portal Group] = 'Mobile Group'").Count();
            //    lastdata.Rows[3]["group count"] = dt2.Select("[Portal Group] = 'API Group'").Count();
            //    lastdata.Rows[4]["group count"] = dt2.Select("[Portal Group] = 'Client Group'").Count();
            //}
            if (dt2.Rows.Count > 0)
            {
                //lastdata.Columns.Add("group name");
                //lastdata.Columns.Add("group count");                
                lastdata.Rows.Add(lastdata.NewRow());
                lastdata.Rows.Add(lastdata.NewRow());
                lastdata.Rows.Add(lastdata.NewRow());
                lastdata.Rows.Add(lastdata.NewRow());
                lastdata.Rows.Add(lastdata.NewRow());

                lastdata.Rows[lastdata.Rows.Count][0] = "Affiliate Group";
                lastdata.Rows[lastdata.Rows.Count + 1][0] = "Interal Group";
                lastdata.Rows[lastdata.Rows.Count + 2][0] = "Mobile Group";
                lastdata.Rows[lastdata.Rows.Count + 3][0] = "API Group";
                lastdata.Rows[lastdata.Rows.Count + 4][0] = "Client Group";

                lastdata.Rows[lastdata.Rows.Count][1] = dt2.Select("[Portal Group] = 'Affiliate Group'").Count();
                lastdata.Rows[lastdata.Rows.Count + 11][1] = dt2.Select("[Portal Group] = 'Interal Group'").Count();
                lastdata.Rows[lastdata.Rows.Count + 2][1] = dt2.Select("[Portal Group] = 'Mobile Group'").Count();
                lastdata.Rows[lastdata.Rows.Count + 3][1] = dt2.Select("[Portal Group] = 'API Group'").Count();
                lastdata.Rows[lastdata.Rows.Count + 4][1] = dt2.Select("[Portal Group] = 'Client Group'").Count();
            }

            // dataGridView1.DataSource = lastdata;

            ExcelManager.NpoiExcel(lastdata, Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");

            this.btn_run.Enabled = true;
            this.tb_result.AppendText("finished");

            foreach (var keyvalue in this.isDevConfirmedDic)
            {
                if (keyvalue.Value)
                {

                    tb_smsconfirm.AppendText(keyvalue.Key + ",");
                }
            }
            foreach (var keyvalue in this.isMoblieConfirmedDic)
            {
                if (keyvalue.Value)
                {

                    tb_mobileconfirm.AppendText(keyvalue.Key + ",");
                }
            }

            foreach (var keyvalue in this.isSMSDoneDic)
            {
                if (!keyvalue.Value)
                {

                    tb_smsundo.AppendText(keyvalue.Key + ",");
                }
            }
            foreach (var keyvalue in this.isMobileDoneDic)
            {
                if (!keyvalue.Value)
                {
                    tb_mobileundo.AppendText(keyvalue.Key + ",");
                }
            }
        }




        private static void InitUsers()
        {
            getConfigUsertoList(JIRAManaager.JIRACSUsers, "JIRACSUsers");
            getConfigUsertoList(JIRAManaager.JIRAAffiliateUsers, "JIRAAffiliateUsers");
            getConfigUsertoList(JIRAManaager.JIRAClientUsers, "JIRAClientUsers");
            getConfigUsertoList(JIRAManaager.JIRAInteralUsers, "JIRAInteralUsers");
            getConfigUsertoList(JIRAManaager.JIRAMobileUsers, "JIRAMobileUsers");
            getConfigUsertoList(JIRAManaager.JIRAAPIUsers, "JIRAAPIUsers");
        }
        private static void getConfigUsertoList(IList<string> list, string configSectionName)
        {
            string csstr = System.Configuration.ConfigurationManager.AppSettings[configSectionName].ToString();
            if (!string.IsNullOrEmpty(csstr))
            {
                csstr = csstr.ToLower();
                var cslist = csstr.Split(";".ToCharArray());
                foreach (var item in cslist)
                {
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }
                }
            }
        }

        private void getJIRAData(DataTable dt, JIRAManaager manager)
        {
            foreach (DataRow row in dt.Rows)
            {
                var jiranumber = row["KEY"].ToString();
                if (!this.isDevConfirmedDic.ContainsKey(jiranumber) && jiranumber.ToLower().StartsWith("one"))
                {
                    this.tb_result.AppendText(jiranumber + ",");
                    var doc = manager.GetJIRADetail(jiranumber);
                    //  if(row["SMS1 Product"].ToString().ToLower()== "Mobile".ToLower())
                    if (string.Compare(row["SMS1 Product LEGACY"].ToString(), "Mobile", true) != 0)
                    {
                        if (!this.isDevConfirmedDic.ContainsKey(jiranumber))
                        {
                            this.isDevConfirmedDic.Add(jiranumber, manager.IsDevConfirmed(doc, SprintStartDay));
                        }
                        if (!this.isSMSDoneDic.ContainsKey(jiranumber))
                        {
                            this.isSMSDoneDic.Add(jiranumber, manager.IsDone(doc, ProductType.SMS));
                        }
                    }
                    else {
                        if (!this.isMoblieConfirmedDic.ContainsKey(jiranumber))
                        {
                            this.isMoblieConfirmedDic.Add(jiranumber, manager.IsMobileConfirmed(doc, SprintStartDay));
                        }
                        if (!this.isMobileDoneDic.ContainsKey(jiranumber))
                        {
                            this.isMobileDoneDic.Add(jiranumber, manager.IsDone(doc, ProductType.Moblie));
                        }
                    }
                    //this.isInPatchDic.Add(jiranumber, manager.IsInPatch(doc));
                }
            }
        }

        private DataTable mergeAndHandleTable(DataTable dtadd, DataTable dtremove)
        {
            DataTable dttemp = new DataTable();
            if (dtadd != null)
            {
                foreach (DataColumn dc in dtadd.Columns)
                {
                    dttemp.Columns.Add(dc.ColumnName, dc.DataType);
                }
            }
            else {
                foreach (DataColumn dc in dtremove.Columns)
                {
                    dttemp.Columns.Add(dc.ColumnName, dc.DataType);
                }

            }

            dttemp.Columns.Add("assigned team");
            dttemp.Columns.Add("change detail");
            dttemp.Columns.Add("statistical date", typeof(DateTime));

            foreach (DataRow dr in dtadd.Rows)
            {
                var datarow = dttemp.NewRow();
                foreach (DataColumn dc in dtadd.Columns)
                {
                    datarow[dc.ColumnName] = dr[dc.ColumnName];
                }

                if (JIRAManaager.JIRACSUsers.Contains(datarow["Responsible Developer"].ToString().ToLower()))
                {
                    datarow["assigned team"] = "CS Team";
                }
                else {
                    datarow["assigned team"] = "US Team";
                }
                datarow["change detail"] = "Added";
                datarow["statistical date"] = DateTime.Now.ToString("yyyy-MM-dd");
                dttemp.Rows.Add(datarow);
            }

            foreach (DataRow dr in dtremove.Rows)
            {
                var datarow = dttemp.NewRow();
                foreach (DataColumn dc in dtremove.Columns)
                {
                    datarow[dc.ColumnName] = dr[dc.ColumnName];
                }

                if (JIRAManaager.JIRACSUsers.Contains(datarow["Responsible Developer"].ToString().ToLower()))
                {
                    datarow["assigned team"] = "CS Team";
                }
                else {
                    datarow["assigned team"] = "US Team";
                }
                datarow["change detail"] = "Removed";

                datarow["statistical date"] = DateTime.Now.ToString("yyyy-MM-dd");
                dttemp.Rows.Add(datarow);

            }


            return dttemp;

        }

        private void removePatchAndDevConfirmed(DataTable dt)
        {
            List<DataRow> list = new List<DataRow>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["KEY"].ToString().ToLower().StartsWith("one"))
                {
                    if (string.Compare(dr["SMS1 Product LEGACY"].ToString(), "Mobile", true) == 0)
                    {
                        if (this.isMoblieConfirmedDic[dr["KEY"].ToString()])
                        {
                            list.Add(dr);
                        }
                    }
                    else {
                        if (this.isDevConfirmedDic[dr["KEY"].ToString()])
                        {
                            list.Add(dr);
                        }

                    }
                }


            }
            list.ForEach(x => dt.Rows.Remove(x));
        }


        private void addPortGroup(DataTable dt, string columnName)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("Portal Group");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string username = dt.Rows[i][columnName].ToString().ToLower();
                    if (!string.IsNullOrEmpty(username))
                    {
                        if (JIRAManaager.JIRAAffiliateUsers.Contains(username))
                        {
                            dt.Rows[i]["Portal Group"] = "Affiliate Group";
                        }
                        else if (JIRAManaager.JIRAClientUsers.Contains(username))
                        {
                            dt.Rows[i]["Portal Group"] = "Client Group";

                        }
                        else if (JIRAManaager.JIRAInteralUsers.Contains(username))
                        {
                            dt.Rows[i]["Portal Group"] = "Interal Group";
                        }
                        else if (JIRAManaager.JIRAMobileUsers.Contains(username))
                        {
                            dt.Rows[i]["Portal Group"] = "Mobile Group";
                        }
                        else if (JIRAManaager.JIRAAPIUsers.Contains(username))
                        {
                            dt.Rows[i]["Portal Group"] = "API Group";
                        }
                    }
                }
            }
        }



        private void removeGenerateInfo(DataTable dt)
        {
            var datalist = dt.Select("KEY like '%Generated%' or  isNull(KEY,'') =''");
            foreach (var data in datalist)
            {
                dt.Rows.Remove(data);
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.tb_startDay.Text = e.Start.ToString("yyyy-MM-dd");
            this.monthCalendar1.Visible = false;

        }

        private void tb_startDay_Click(object sender, EventArgs e)
        {
            this.monthCalendar1.Visible = true;
        }


    }


    /*
        [Sheet1$]
        连接字符串2007：Provider=Microsoft.ACE.OLEDB.12.0;Data Source=strFileName;Extended Properties=Excel 12.0;HDR=NO;
        连接字符串2003：Provider=Microsoft.Jet.OLEDB.4.0;Data Source=d:\test.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'
        provider：表示提供程序名称
        Data Source：这里填写Excel文件的路径
        Extended Properties：设置Excel的特殊属性
        Extended Properties 取值：
        Excel 8.0 针对Excel2000及以上版本，Excel5.0 针对Excel97。
        HDR=Yes 表示第一行包含列名,在计算行数时就不包含第一行
        IMEX 0:导入模式,1:导出模式:2混合模式
    */
}
