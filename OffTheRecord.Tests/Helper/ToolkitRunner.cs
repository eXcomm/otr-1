using System;
using System.Diagnostics;
using System.IO;

namespace OffTheRecord.Tests.Helper
{
    public static class ToolkitRunner
    {
        public static Tuple<int, string> Run(string location, string filename, string arguments = null,
            string input = null)
        {
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = Path.Combine(location, filename);
            p.StartInfo.Arguments = arguments;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.RedirectStandardOutput = true;

            if (!string.IsNullOrEmpty(input))
            {
                p.StartInfo.RedirectStandardInput = true;
            }

            if (!File.Exists((p.StartInfo.FileName)))
            {
                throw new FileNotFoundException(p.StartInfo.FileName);
            }

            bool started = p.Start();

            if (!started)
            {
                throw new Exception("Fail to start application.");
            }

            if (p.StartInfo.RedirectStandardInput)
            {
                using (StreamWriter s = p.StandardInput)
                {
                    s.WriteLine(input);
                }
            }

            string result = p.StandardOutput.ReadToEnd();

            p.WaitForExit();
            int exitcode = p.ExitCode;
            p.Close();

            return new Tuple<int, string>(exitcode, result);
        }
    }
}