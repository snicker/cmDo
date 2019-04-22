using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toodledo.Model;
using Toodledo.Client;
using Toodledo.Model.API;
using s7.cmDo.Notifications;
using System.Diagnostics;

namespace s7.cmDo
{
    public static class cmDo
    {
        static void Main(string[] args)
        {
            Growler.Initialize();
            try
            {
                if (args.Contains("-config"))
                {
                    string[] credentials = GetCredentials(args);
                    Cfg.cmDoConfig cfg = Cfg.cmDoConfig.Load();
                    cfg.UserID = credentials[0];
                    cfg.ProtectedPassword = Security.Protector.Protect(credentials[1]);
                    cfg.Save();
                    SendNotification("The configuration for cmDo is now set.", "Configuration Complete", Growler.GeneralNotification);
                }
                else if (args.Length > 0 && args[0].ToLower() == "-setdefault")
                {
                    string[] taskInfo = SanitizeArgs(args);
                    TaskBuilder tb = new TaskBuilder(taskInfo);
                    Task t = tb.ToTask();
                    Cfg.cmDoConfig cfg = Cfg.cmDoConfig.Load();
                    cfg.DefaultTask = DefaultTask.FromTask(t);
                    cfg.Save();
                    SendNotification("Default task values updated.", "Defaults updated", Growler.GeneralNotification);
                }
                else
                {
                    if (!Cfg.cmDoConfig.Exists)
                        throw new System.IO.FileNotFoundException("Could not find the configuration file with authentication information.\nPlease run cmDo with the -config argument and specify your username and password.", Cfg.cmDoConfig.ConfigPath);
                    Cfg.cmDoConfig cfg = Cfg.cmDoConfig.Load();
                    ToodleDo.Connect(cfg.UserID, Security.Protector.Unprotect(cfg.ProtectedPassword));

                    List<string[]> tasks = new List<string[]>();
                    if (args.Length > 0 && args[0].ToLower() == "-pipe")
                    {
                        while (Console.In.Peek() != -1)
                        {
                            string pipedinput = Console.In.ReadLine();
                            tasks.Add(SanitizeArgs(pipedinput.Trim().Split(new char[] { ' ', '\t' })));
                        }
                    }
                    else if (args.Length > 0)
                    {
                        tasks.Add(SanitizeArgs(args));
                    }

					bool success = false;
                    foreach (string[] taskInfo in tasks)
                    {
						success = false;
						while (!success) {
							try {
								TaskBuilder tb = new TaskBuilder(taskInfo, GetDefaultTask());
								Task t = tb.ToTask();

								ToodleDo.AddTask(t);
								success = true;
								SendNotification("Task successfully added!", "Task added", Growler.SuccessNotification);
							}
							catch (Exception e) {
								SendNotification("An error occured while sending '" + String.Join(" ",taskInfo) + "' to Toodledo! Waiting 30 seconds before retrying.\n\nError: " + e.Message, "Error", Growler.ErrorNotification);
								System.Threading.Thread.Sleep(30000);
							}
						}
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                SendNotification(e.Message, "Error", Growler.ErrorNotification);
            }
        }
        
        private static bool IsPipedInput()
        {
            try
            {
                System.IO.Stream stream = Console.OpenStandardInput();
                Type nulltype = System.IO.Stream.Null.GetType();
                Type thisstreamtype = stream.GetType();
                System.Windows.Forms.MessageBox.Show(stream.GetType().ToString());
                bool canread = stream.CanRead;
                bool isKey = Console.In.Peek() > 0;
                return false;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return true;
            }
        }

        private static void SendNotification(string message, string title, Growl.Connector.NotificationType type)
        {
            if (!Growler.Growl(type, title, message))
                System.Windows.Forms.MessageBox.Show(message, title);
        }

        private static Task GetDefaultTask()
        {
            Cfg.cmDoConfig cfg = Cfg.cmDoConfig.Load(false);
            return cfg.DefaultTask == null ? new Task() : cfg.DefaultTask.ToTask();
        }

        private static string[] GetCredentials(string[] args)
        {
            string userID = string.Empty;
            string password = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower().StartsWith("u:"))
                    userID = args[i].Substring(2);
                if (args[i].ToLower().StartsWith("p:"))
                    password = args[i].Substring(2);
            }
            if (string.IsNullOrEmpty(userID))
                throw new ArgumentException("You must specify a user id with 'u:<username@domain.tld>'", "u:");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("You must specify a password with 'p:<mytoodledopassword>'", "p:");
            return new string[2] { userID, password};
        }

        private static string[] SanitizeArgs(string[] args)
        {
            string[] verboten = new string[] { "u:", "p:", "-" };
            List<string> toKeep = new List<string>();
            foreach (string arg in args)
            {
                bool keep = true;
                foreach (string tokill in verboten)
                    keep &= !arg.ToLower().StartsWith(tokill.ToLower());
                if (keep)
                    toKeep.Add(arg);
            }
            return toKeep.ToArray();
        }
    }
}
