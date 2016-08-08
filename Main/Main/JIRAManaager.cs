using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Main
{
    public class JIRAManaager
    {

        private const string JIRALoginUrl = "http://jira.sms-assist.com/login.jsp";
        private const string JIRADetailUrl = "http://jira.sms-assist.com/browse/{0}?page=com.atlassian.jira.plugin.system.issuetabpanels:all-tabpanel";
        private string[] JIRAStatus = "dev confirmed".Split(",".ToCharArray());
        public static List<string> JIRACSUsers = new List<string>();
        public static List<string> JIRAAffiliateUsers = new List<string>();
        public static List<string> JIRAClientUsers = new List<string>();
        public static List<string> JIRAInteralUsers = new List<string>();
        public static List<string> JIRAMobileUsers = new List<string>();
        public static List<string> JIRAAPIUsers = new List<string>();

        private WebClientEx client = new WebClientEx();
        public JIRAManaager()
        {
            string csstr = System.Configuration.ConfigurationManager.AppSettings["JIRACSUsers"].ToString();
            if (!string.IsNullOrEmpty(csstr))
            {
                csstr = csstr.ToLower();
                JIRACSUsers.Concat(csstr.Split(";".ToCharArray()));
            }
        }
        public Boolean LoginJira(string username, string password)
        {
            client.BaseAddress = JIRALoginUrl;
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            string postString = "os_password=" + password + "&os_username=" + username;
            byte[] postData = Encoding.ASCII.GetBytes(postString);
            var responseData = client.UploadData(JIRALoginUrl, "POST", postData);
            return true;
        }

        public HtmlAgilityPack.HtmlDocument GetJIRADetail(string jiraNumber)
        {
            var data = client.DownloadData(string.Format(JIRADetailUrl, jiraNumber));
            string datastr = Encoding.UTF8.GetString(data);
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(datastr);
            return doc;
        }
        public bool IsDevConfirmed(HtmlAgilityPack.HtmlDocument doc, DateTime mindate)
        {
            bool result = false;
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='issue-data-block']");
            if (nodes != null)
            {                 
                foreach (var node in nodes)
                {
                    var ondevnode = node.SelectSingleNode(".//td[@class='activity-new-val']/text()");

                    if (ondevnode != null && ondevnode.InnerText.Contains("Dev Confirmed"))
                    {
                        var str = node.SelectSingleNode(".//div[@class='action-details']//span/time").GetAttributeValue("datetime", "");
                        var date = DateTime.Parse(str);
                        if (date < mindate)
                        {
                            result = true;
                            break;
                        }
                    }
                }

            }
            return result;
        }

        public bool IsDone(HtmlAgilityPack.HtmlDocument doc, ProductType type)
        {
            bool result = false;
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='issue-data-block']");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var ondevnode = node.SelectSingleNode(".//td[@class='activity-new-val']/text()");

                    if (ondevnode != null )
                    {
                        if (type == ProductType.SMS)
                        {
                            if (ondevnode.InnerText.Contains("Dev Confirmed"))
                            {
                                var str = node.SelectSingleNode(".//div[@class='action-details']//span/time").GetAttributeValue("datetime", "");
                                result = true;
                                break;
                            }
                        }
                        else {
                            if (ondevnode.InnerText.Contains("On Dev"))
                            {
                                var str = node.SelectSingleNode(".//div[@class='action-details']//span/time").GetAttributeValue("datetime", "");
                                result = true;
                                break;
                            }
                        } 
                    }
                }

            }
            return result;
        }

        public bool IsMobileConfirmed(HtmlAgilityPack.HtmlDocument doc, DateTime mindate)
        {
            bool result = false;
            // var nodes = doc.DocumentNode.SelectNodes("//td[@class='activity-new-val']/text()");
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='issue-data-block']");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var ondevnode = node.SelectSingleNode(".//td[@class='activity-new-val']/text()");

                    if (ondevnode != null && ondevnode.InnerText.Contains("On Dev"))
                    {
                        var str = node.SelectSingleNode(".//div[@class='action-details']//span/time").GetAttributeValue("datetime", "");
                        var date = DateTime.Parse(str);
                        if (date < mindate)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public bool IsInPatch(HtmlAgilityPack.HtmlDocument doc)
        {
            bool result = false;
            var node = doc.DocumentNode.SelectSingleNode("//div[@id='wrap-labels']/div/ul/li/a/span");
            if (node != null)
            {
                string labeltext = node.InnerText;
                Regex regstr = new Regex(@"\d{8}");
                result = regstr.IsMatch(labeltext);
            }
            else {
                result = false;
            }
            return result;

        }

       
    }

    public enum ProductType {
        Moblie,
        SMS
    }
}
