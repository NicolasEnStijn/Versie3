using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Linq;
using System.IO;

namespace WebApplication3
{
    public static class data
    {
        public static remoteprojects p = null;
        private static int order = 0;
        private static remoteprojectsProject[] rp;

        public static void ordertable(object sender)
        {

            Button button = sender as Button;
            if (button == null)
            {
                TextBox tb = sender as TextBox;
                if (tb == null)
                {
                    if (p == null)
                    {
                        //string xml = File.ReadAllText(@"C:\Users\Nicolas\Documents\VIVES\stage\remoteprojects.xml");
                        string xml = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\stage\remoteprojects.xml");
                        var catalog1 = xml.ParseXML<remoteprojects>();
                        p = (remoteprojects)catalog1;
                        rp = new remoteprojectsProject[p.projects.Count() - 1];
                        Array.Copy(p.projects, 1, rp, 0, (p.projects.Count() - 1));
                        Task.Run(() => Connectcheck());
                    }
                }
                else if (tb.ID.Equals("TextBoxNaam"))
                {
                    var q = from x in rp
                            where x.name.ToLower().Contains(tb.Text.ToLower())
                            select x;
                    rp = (remoteprojectsProject[])q.ToArray();
                }
                else if (tb.ID.Equals("TextBoxMunicipality"))
                {
                    var q = from x in rp
                            where x.municipality.ToLower().StartsWith(tb.Text.ToLower())
                            select x;
                    rp = (remoteprojectsProject[])q.ToArray();
                }

            }
            else if (button.ID.Equals("name"))
            {
                Array.Copy(p.projects, 1, rp, 0, (p.projects.Count() - 2));
                if (order == 1)
                {
                    rp = rp.OrderByDescending(u => u.name).ToArray();
                    order = 0;
                }
                else
                {
                    order = 1;
                    rp = rp.OrderBy(u => u.name).ToArray();

                }
            }
            else if (button.ID.Equals("municipality"))
            {
                if (order == 2)
                {
                    rp = rp.OrderByDescending(u => u.municipality).ToArray();
                    order = 0;
                }
                else
                {
                    rp = rp.OrderBy(u => u.municipality).ToArray();
                    order = 2;
                }
            }
        }



        public static void Connectcheck()
        {
            int i = 1;
            for (i = 0; i < p.projects.Count(); i++)
            {
                if (p.projects[i].notitions.Equals(""))
                {
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.TypeOfService, 0x03);
                    String ip = "";
                    if (p.projects[i].ipaddress.Equals(""))
                    {
                        ip = p.projects[i].hostname;
                    }
                    else
                    {
                        ip = p.projects[i].ipaddress;
                    }

                    try
                    {
                        var result = sock.BeginConnect(ip, Convert.ToInt16(p.projects[i].tcpport), null, null);
                        bool success = result.AsyncWaitHandle.WaitOne(1000, true);
                        if (success)
                        {
                            sock.EndConnect(result);
                            byte[] bytes = new byte[8];
                            sock.Receive(bytes);
                            String txt = bytes[1].ToString();
                            if (txt.Equals(""))
                            {
                                p.projects[i].berijkbaar = remoteprojectsProject.bereikbaarheid.nietberijkbaar;
                            }
                            {
                                p.projects[i].berijkbaar = remoteprojectsProject.bereikbaarheid.berijkbaar;
                            }
                        }
                        else
                        {
                            throw new SocketException(10060); // Connection timed out.
                        }
                    }
                    catch
                    {
                        p.projects[i].berijkbaar = remoteprojectsProject.bereikbaarheid.nietberijkbaar;
                    }
                    finally
                    {
                        sock.Close();
                    }
                }
                else
                {
                    p.projects[i].berijkbaar = remoteprojectsProject.bereikbaarheid.geeninfo;
                }
            }

        }

       /* public static async Task updatepings()
        {
            int i = 1;
            for (i = 0; i < p.projects.Count(); i++)
            {
                if (p.projects[i].notitions.Equals(""))
                {
                    Ping pi = new Ping();
                    PingReply r;
                    string pingreq = "192.0.0.0";
                    if (p.projects[i].ipaddress.Equals(""))
                    {

                    }
                    else
                    {
                        pingreq = p.projects[i].ipaddress;
                    }
                    r = await pi.SendPingAsync(pingreq, 2000);
                    if (r.Status == IPStatus.Success)
                    {
                        p.projects[i].berijkbaar = remoteprojectsProject.bereikbaarheid.berijkbaar;
                    }
                    else
                    {
                        p.projects[i].berijkbaar = remoteprojectsProject.bereikbaarheid.nietberijkbaar;
                    }
                }
                else
                {
                    p.projects[i].berijkbaar = remoteprojectsProject.bereikbaarheid.geeninfo;
                }
            }

        }*/

        public static String getName(int index)
        {
            return rp[index].name;
        }

        public static String getMunicipality(int index)
        {
            return rp[index].municipality;
        }

        public static int getLength()
        {
            return rp.GetLength(0);
        }

        public static string getHostname(int index)
        {
            return rp[index].hostname;
        }

        public static String getIPAdress(int index)
        {
            return rp[index].ipaddress;
        }

        public static String getProgramToLaunch(int index)
        {
            return rp[index].programtolaunch;
        }

        public static String getNotification(int index)
        {
            return rp[index].notitions;
        }

        public static remoteprojectsProject.bereikbaarheid getBereikbaarheid(int index)
        {
            return rp[index].berijkbaar;
        }
    }
}