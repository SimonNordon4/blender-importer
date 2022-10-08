using System.Security.Principal;
using UnityEditor.AssetImporters;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace BlenderImporter
{
    [ScriptedImporter(1, new[] { "made_by_simon" }, new[] { "blend" })]
    public class BlendScriptedImporter : ScriptedImporter
    {
        
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // TODO: Start a Blender Process
            // TODO: Export everything in the Blender File.
            // TODO: Mark when it's completed.
            // To begin with, let's just export the file by opening a blender process.
            Debug.Log("Starting Custom Blender Importer");
            BlenderProcessHandler.OnBlenderProcessFinished onBlenderProcessFinished = BlendProcessFinished;
            
            
            // Gather blender process arguments.
            var pythonScript = @"E:\repos\blender-importer\blender-importer-project\Assets\Editor\blend-importer-src\blend-exporter.py";
            var blendFilePath = ctx.assetPath;
            var args = "";
            
            // get blender association.
            
            // BlenderProcessHandler.RunBlender(pythonScript, blendFilePath, args, onBlenderProcessFinished);
            
            // BlenderProcessHandler.RunBlender(blendFilePath);
        }

        private void BlendProcessFinished(string outputPath, bool success)
        {
            Debug.Log("Blender Process Finished");
        }
    }
}