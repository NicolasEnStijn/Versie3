using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace WebApplication3
{
    public partial class brandcentrales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            data.ordertable(sender);
            int i = 7;
            for (i = Table1.Rows.Count - 1; i > 0; i--)
            {
                Table1.Rows.Remove(Table1.Rows[i]);
            }
            for (i = 0; i < data.getLength(); i++)
            {
                TableRow tRow = new TableRow();
                Table1.Rows.Add(tRow);
                Button btn = new Button();
                btn.ID = i.ToString();
                btn.Text = "open";
                btn.UseSubmitBehavior = false;
                btn.Click += new EventHandler(SelectButton_Click);
                TableCell tcb = new TableCell();
                tcb.Controls.Add(btn);
                tcb.CssClass = "open";
                tRow.Cells.Add(tcb);
                TableCell tcn = new TableCell();
                tcn.Text = data.getName(i);
                tcn.CssClass = "naam";
                tRow.Cells.Add(tcn);
                TableCell tcs = new TableCell();
                tcs.Text = data.getMunicipality(i);
                tcs.CssClass = "stad";
                tRow.Cells.Add(tcs);
                TableCell tcp = new TableCell();
                tcp.ID = "tcp" + i.ToString();
                tcp.CssClass = "icon";
                if (data.getBereikbaarheid(i) == remoteprojectsProject.bereikbaarheid.berijkbaar)
                {
                    tcp.Text = string.Format("<img src='{0}' />", "https://image.flaticon.com/icons/svg/63/63586.svg");
                }
                else if (data.getBereikbaarheid(i) == remoteprojectsProject.bereikbaarheid.nietberijkbaar)
                {
                    tcp.Text = string.Format("<img src='{0}' />", "https://image.flaticon.com/icons/svg/63/63596.svg");
                }
                else if (data.getBereikbaarheid(i) == remoteprojectsProject.bereikbaarheid.geeninfo)
                {
                    tcp.Text = string.Format("<img src='{0}' />", "https://image.flaticon.com/icons/svg/63/63923.svg");
                }

                tRow.Cells.Add(tcp);
            }
        }

        protected void SelectButton_Click(object sender, EventArgs e)
        {
            MsgBox("het programma wordt opgestart", this.Page, this);
            Button button = sender as Button;
            if (data.getIPAdress(Convert.ToInt32(button.ID)).Equals(""))
            {
                COMPorthelpers.changehostname(data.getProgramToLaunch(Convert.ToInt32(button.ID)), data.getHostname(Convert.ToInt32(button.ID))); // vul  het juiste hostname  in bij de juiste comport
            }
            else
            {
                COMPorthelpers.changecomport(data.getProgramToLaunch(Convert.ToInt32(button.ID)), data.getIPAdress(Convert.ToInt32(button.ID))); // vul het juist IP-adres in bij de juiste comport
            }
        }

        protected void check(object sender, EventArgs e)
        {
            Task.Run(() => data.Connectcheck());
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }

    internal static class ParseHelpers
    {

        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T ParseXML<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }
    }
}