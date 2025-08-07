using Newtonsoft.Json;
using System.Reflection;

namespace TaskyRevamp.WebAPI.Exeptions
{
    public static class LoggingUtility
    {
        // Log an Exception
        public static string LogException(Exception exc, string source)
        {
            // Generate token for easy searchability
            var token = Guid.NewGuid().ToString();

            // Get the absolute path to the log file
            var logPath = string.Format("Logs/Errors/{0:yyyy}/{0:MMMM}", DateTime.UtcNow);
            // logPath = HttpContext.Current.Server.MapPath(logPath);

            // Create the log folders, if not existing
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            // Get the absolute log file path
            var logFile = string.Format("{0}/{1:yyyyMMdd}-Log.txt", logPath, DateTime.UtcNow);

            // Open the log file for append and write the log
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine("****************************** {0} : {1} ******************************", token, DateTime.UtcNow);
                sw.WriteLine(JsonConvert.SerializeObject(exc));
                sw.WriteLine("******************************");

                sw.WriteLine("Source: " + source);
                if (exc.InnerException != null)
                {
                    sw.Write("Inner Exception Type: ");
                    sw.WriteLine(exc.InnerException.GetType().ToString());
                    sw.Write("Inner Exception: ");
                    sw.WriteLine(exc.InnerException.Message);
                    sw.Write("Inner Source: ");
                    sw.WriteLine(exc.InnerException.Source);
                    if (exc.InnerException.StackTrace != null)
                    {
                        sw.WriteLine("Inner Stack Trace: ");
                        sw.WriteLine(exc.InnerException.StackTrace);
                    }
                }
                sw.Write("Exception Type: ");
                sw.WriteLine(exc.GetType().ToString());
                sw.WriteLine("Exception: " + exc.Message);
                sw.WriteLine("Stack Trace: ");
                if (exc.StackTrace != null)
                {
                    sw.WriteLine(exc.StackTrace);
                    sw.WriteLine();
                    sw.Close();
                }
            }

            return token;
        }

        // Notify System Operators about an exception
        public static void NotifySystemOps()
        {
            // Include code for notifying IT system operators
        }

        // Notify System Operators about an exception
        public static void LogInfo(string info)
        {
            string infoString = "";

            if (string.IsNullOrEmpty(info))
                infoString = "info is Null";
            else
                infoString = info;



            var logPath = string.Format("Logs/Info/{0:yyyy}/{0:MMMM}", DateTime.UtcNow);
            //logPath = HttpContext.Current.Server.MapPath(logPath);

            // Create the log folders, if not existing
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            // Get the absolute log file path
            var logFile = string.Format("{0}/{1:yyyyMMdd}-Log.txt", logPath, DateTime.UtcNow);

            // Open the log file for append and write the log
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine("******************************  {0} ******************************", DateTime.UtcNow);
                sw.WriteLine("Source: ");
                sw.Write("Calling Assemply: ");
                sw.WriteLine(Assembly.GetCallingAssembly().GetName());
                sw.Write("Executing Assembly: ");
                sw.WriteLine(Assembly.GetExecutingAssembly().GetName());
                sw.Write("Method Name: ");
                sw.WriteLine(MethodBase.GetCurrentMethod().Name);
                //sw.Write("Paramatetrs: ");
                //sw.WriteLine(MethodInfo.GetCurrentMethod().GetParameters.fir);
                sw.Write("Info: ");
                sw.WriteLine(infoString);
                sw.Close();
            }
        }
    }
}
