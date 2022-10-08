using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace BlenderImporter
{
    public static class BlenderProcessHandler
    {
        public delegate void OnBlenderProcessFinished(string blendFilePath, bool success);


        public static void RunBlender(string blendFile)
        {
            // Start a new process to open the blendFile
            Process blenderProcess = new Process();
            blenderProcess.StartInfo.FileName = "blender";
            blenderProcess.StartInfo.Arguments = blendFile;
            blenderProcess.StartInfo.UseShellExecute = false;
            blenderProcess.StartInfo.RedirectStandardOutput = true;
            blenderProcess.StartInfo.RedirectStandardError = true;
            blenderProcess.StartInfo.CreateNoWindow = true;
            blenderProcess.EnableRaisingEvents = true;
        }
        
        public static void RunBlender1(string pythonScriptPath,
            string blendFilePath,
            string args, OnBlenderProcessFinished callback)
        {
            // set initial process arguments.
            var start = new ProcessStartInfo
            {
                FileName = blendFilePath,
                // This is the command line argument for everything that comes after ../blender.exe
                Arguments = $"--background {blendFilePath} --python {pythonScriptPath} -- {args}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            // begin the blender process.
            var process = new Process();
            process.StartInfo = start;
            process.EnableRaisingEvents = true;

            // We debug.log everytime a print line is registered in the blender process.
            process.OutputDataReceived += (sender, outputArgs) =>
            {  
                if (outputArgs != null)
                {
                    Debug.Log(outputArgs.Data);
                }
            };

            process.ErrorDataReceived += (sender, errorArgs) =>
            {
                if (errorArgs != null) Debug.LogError(errorArgs.Data);
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
            process.CancelErrorRead();

            callback.Invoke(blendFilePath, true);
        }
    }
}