namespace MgmtCardLib
{
    using System;
    using System.IO;

    public class ErrorLog
    {
        private static readonly int MaxFileSize = 1048576;

        public static void SaveLog(string log, string file = "")
        {
            string temppath;
            string message = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] - " + log;
            try
            {
                if (file.Trim() == string.Empty)
                {
                    temppath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "ErrorLog_" + DateTime.Now.ToString("yyyyMM") + ".txt");
                }
                else
                {
                    temppath = file;
                }

                if (!File.Exists(temppath))
                {
                    File.Create(temppath).Close();
                    SaveLog(message, temppath);
                }
                else
                {
                    if (new FileInfo(temppath).Length > MaxFileSize)
                    {
                        string newfilename = temppath.Substring(0, temppath.LastIndexOf(".txt")) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                        File.Move(temppath, newfilename);
                        SaveLog(message, temppath);
                    }
                    else
                    {
                        SaveToFile(message, temppath);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SaveLog(ex.Message);
                throw new Exception(string.Format(ex.Message));
            }
        }

        private static void SaveToFile(string log, string path)
        {
            try
            {
                using (StreamWriter tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(log);
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SaveLog(ex.Message);
                throw new Exception(string.Format(ex.Message));
            }
        }
    }
}
