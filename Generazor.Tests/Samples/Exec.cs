using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Generazor.Tests.Samples
{
    public static class Exec
    {
        public class ExecResults
        {
            public int              ExitCode;
            public IList<string>    Output      = new List<string>();
            public IList<string>    Errors      = new List<string>();
        }

        public static ExecResults CmdInProjectFolder(string fileName, string arguments, string projectFolder)
        {
            var solutionFile = "Generazor.sln";
            var solutionFolder = Directory.GetCurrentDirectory();

            while (!File.Exists(Path.Combine(solutionFolder, solutionFile)))
            {
                var parent = Directory.GetParent(solutionFolder);

                if (parent == null)
                    Assert.Fail($"Could not find {solutionFile} in parent of {Directory.GetCurrentDirectory()}");

                solutionFolder = parent.FullName;
            }

            var workingDirectory = Path.Combine(solutionFolder, projectFolder);
            return Cmd(fileName, arguments, workingDirectory);
        }

        public static ExecResults Cmd(string fileName, string arguments, string workingDirectory)
        {
            using (Process process = new Process())
            {
                var result = new ExecResults();

                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = workingDirectory;

                TestContext.Progress.WriteLine($"Running {process.StartInfo.FileName} {process.StartInfo.Arguments} (in {process.StartInfo.WorkingDirectory})");

                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, eventArgs) =>
                    {
                        if (eventArgs.Data == null)
                            outputWaitHandle.Set();
                        else
                        {
                            result.Output.Add(eventArgs.Data);
                            TestContext.Progress.WriteLine($"INFO:  {eventArgs.Data}");
                        }
                    };

                    process.ErrorDataReceived += (sender, eventArgs) =>
                    {
                        if (eventArgs.Data == null)
                            outputWaitHandle.Set();
                        else
                        {
                            result.Errors.Add(eventArgs.Data);
                            TestContext.Progress.WriteLine($"ERROR: {eventArgs.Data}");
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();

                    TestContext.Progress.WriteLine($"Process exited with code {process.ExitCode}");
                    result.ExitCode = process.ExitCode;
                    result.ExitCode.Should().Be(0);

                    return result;
                }
            }
        }
    }
}
