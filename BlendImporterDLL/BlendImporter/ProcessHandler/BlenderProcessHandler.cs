﻿using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace BlenderImporter.ProcessHandler
{
    public class BlenderProcessHandler
    {
        public delegate void BlenderProcessEvent(string blendFilePath, bool success);
        public static BlenderProcessEvent OnBlenderProcessFinished;
        
        public static void RunBlender(string blenderExecutable,
            string pythonScriptPath,
            string blendFilePath,
            string args,
            Action Callback)
        {
            // set initial process arguments.
            var start = new ProcessStartInfo
            {
                FileName = blenderExecutable,
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
                // TODO: Add a check or tag to better filter messages.
                
                if (outputArgs != null)
                {
                    //Debug.Log("\tpython:" + outputArgs.Data);
                }
            };

            process.ErrorDataReceived += (sender, errorArgs) =>
            {
                if (errorArgs != null)
                {
                    //Debug.LogError("\tpython ERROR: " + errorArgs.Data);
                }
            };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
            process.CancelErrorRead();
            
            Callback.Invoke();
        }
    }
}