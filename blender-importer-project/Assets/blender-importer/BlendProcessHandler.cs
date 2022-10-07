using System.IO;
using System.Diagnostics;
using UnityEngine;

namespace BlenderImporter
{
    public static class BlenderProcessHandler
    {
        public delegate void OnBlenderProcessFinished(string blendFilePath, bool success);
        public static void RunBlender(string blenderExecutablePath, string pythonExectuablePath, string blendFilePath, string args, OnBlenderProcessFinished callback)
        {
            // set initial process arguments.
            var start = new ProcessStartInfo();
            start.FileName = blenderExecutablePath;
            // This is the command line argument for everything that comes after ../blender.exe
            start.Arguments = $"--background {blendFilePath} --python {pythonExectuablePath} -- {args}";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;

            // begin the blender process.
            Process process = new Process();
            process.StartInfo = start;
            process.EnableRaisingEvents = true;

            // We debug.log everytime a print line is registered in the blender process.
            process.OutputDataReceived += (sender, args) =>
            {
                if(args is not null)
                    f.print(args.Data,BlenderImporterGlobalSettings.instance.PythonConsoleTextColor,"\tpy",BlenderImporterGlobalSettings.instance.PythonConsoleLabelColor);
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                if(args is not null)
                    f.printError(args.Data,"\tpy");
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
            process.CancelErrorRead();

            callback.Invoke(blendFilePath, true);

            return;
        }
    }

    public static class BlenderProcessHandler1
    {
        public delegate void OnBlenderProcessFinished1(string blendFilePath, bool success);
        public static void RunBlender(string blenderExecutablePath, string pythonExectuablePath, string blendFilePath, string args, OnBlenderProcessFinished1 callback)
        {
            // This is the command line argument for everything that comes after ../blender.exe
            var command = $"--background {blendFilePath} --python {pythonExectuablePath} -- {args}";

            return; 
        }
    }
}