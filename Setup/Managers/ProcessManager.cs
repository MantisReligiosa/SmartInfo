using Setup.Data;
using System;
using System.Diagnostics;

namespace Setup.Managers
{
    public class ProcessManager
    {
        public event EventHandler<ProcessDataEventArgs> OutputDataRecieved;

        public int ExecProcess(string processName, string parameters)
        {
            var processStartInfo = new ProcessStartInfo()
            {
                FileName = processName,
                Arguments = parameters,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            using (var process = new Process
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            })
            {
                process.Start();
                process.OutputDataReceived += (sender, dataRecievedEventsArgs) =>
                {
                    var textLine = dataRecievedEventsArgs.Data;
                    if (textLine != null)
                        OutputDataRecieved?.Invoke(this,
                            new ProcessDataEventArgs(textLine));
                };
                process.BeginOutputReadLine();

                process.WaitForExit();
                return process.ExitCode;
            }
        }
    }
}
