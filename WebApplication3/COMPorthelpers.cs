using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Web;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;

namespace WebApplication3
{
    public class COMPorthelpers
    {
        const String launchprogram0 = @"C:\Program Files\Limotec\MD2000 2\pc_md2000.exe";
        const String launchprogram1 = @"C:\Program Files\Limotec\MD2400 1\pc_md2400.exe";
        const String launchprogram2 = @"C:\Program Files\Limotec\MD2400 PC Config\pc_md2400_v2.exe";

        public static bool changecomport(String launchprogram, String ipadress)
        {
            int instance = 5;
            Guid remotedirectguid = new Guid("{50906cb8-ba12-11d1-bf5d-0000f805f530}");
            if (launchprogram.Equals(launchprogram0))
            {
                instance = 0;
            }
            else if (launchprogram.Equals(launchprogram1))
            {
                instance = 1;
            }
            else if (launchprogram.Equals(launchprogram2))
            {
                instance = 2;
            }

            string instancePath = @"ROOT\MULTIPORTSERIAL\000" + instance.ToString();
            DeviceHelper.SetDeviceEnabled(remotedirectguid, instancePath, false); // disable de coressponderende driver van de multi port serial
            Thread.Sleep(100);

            RegistryKey mykey = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Enum\\Root\\MULTIPORTSERIAL\\000" + instance.ToString() + "\\Device Parameters", true);
            if (mykey != null)
            {
                mykey.SetValue("AddressConfigType", 0, RegistryValueKind.DWord);
                mykey.SetValue("IPAddress", ipadress, RegistryValueKind.String); //verander IP-adres in registry

            }
            mykey.Close();

            RegistryKey mykey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Enum\\Root\\MULTIPORTSERIAL\\000" + instance.ToString(), true);
            if (mykey2 != null)
            {
                mykey2.SetValue("FriendlyName", "MD2400 RABBIT 2000 (" + ipadress + ")", RegistryValueKind.String); //verander Friendly Name in registry
            }
            mykey2.Close();

            Thread.Sleep(100);

            if (!IsServiceRunning())
            {
                throw new Exception("blackbox is gestopt");
            }

            DeviceHelper.SetDeviceEnabled(remotedirectguid, instancePath, true); // driver terug inschakelen
            try
            {
                Process.Start(launchprogram);  //programma opstarten
            }
            catch (Exception ex)
            {
                throw new Exception("programma is al open", ex);

            }
            return true;

        }


        public static bool changehostname(String launchprogram, String hostname)
        {
            int instance = 5;
            Guid remotedirectguid = new Guid();
            if (launchprogram.Equals(launchprogram0))
            {
                instance = 0;
                remotedirectguid = new Guid("{50906cb8-ba12-11d1-bf5d-0000f805f530}");
            }

            else if (launchprogram.Equals(launchprogram1))
            {
                instance = 1;
                remotedirectguid = new Guid("{50906cb8-ba12-11d1-bf5d-0000f805f530}");
            }
            else if (launchprogram.Equals(launchprogram2))
            {
                instance = 2;
                remotedirectguid = new Guid("{50906cb8-ba12-11d1-bf5d-0000f805f530}");
            }

            string instancePath = @"ROOT\MULTIPORTSERIAL\000" + instance.ToString();
            DeviceHelper.SetDeviceEnabled(remotedirectguid, instancePath, false);
            Thread.Sleep(100);

            RegistryKey mykey = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Enum\\Root\\MULTIPORTSERIAL\\000" + instance.ToString() + "\\Device Parameters", true);
            if (mykey != null)
            {

                mykey.SetValue("TerminalServerName", hostname, RegistryValueKind.String);
                mykey.SetValue("AddressConfigType", 2, RegistryValueKind.DWord);

            }
            mykey.Close();

            RegistryKey mykey2 = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Enum\\Root\\MULTIPORTSERIAL\\000" + instance.ToString(), true);
            if (mykey2 != null)
            {
                mykey2.SetValue("FriendlyName", "MD2400 RABBIT 2000 (" + hostname + ")", RegistryValueKind.String);
            }
            mykey2.Close();

            Thread.Sleep(100);

            if (!IsServiceRunning())
            {
                throw new Exception("blackbox is gestopt");
            }

            DeviceHelper.SetDeviceEnabled(remotedirectguid, instancePath, true);
            try
            {
                Process.Start(launchprogram);
            }
            catch (Exception ex)
            {
                throw new Exception("programma is al open", ex);



            }
            return true;

        }

        public static bool IsServiceRunning()
        {
            ServiceController service = new ServiceController("COMredirectSrv");
            string text = service.Status.ToString();

            if (text == "Running")
            {
                RestartService(service);
                return true;
            }
            else
            {
                startService(service);
                return false;
            }

        }

        public static bool startService(ServiceController service2)
        {
            try
            {
                int timeoutMilliseconds = 500;
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                //service2.Stop();
                //service2.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service2.Start();
                service2.WaitForStatus(ServiceControllerStatus.Running, timeout);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool RestartService(ServiceController service2)
        {
            try
            {
                int timeoutMilliseconds = 500;
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service2.Stop();
                service2.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service2.Start();
                service2.WaitForStatus(ServiceControllerStatus.Running, timeout);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}