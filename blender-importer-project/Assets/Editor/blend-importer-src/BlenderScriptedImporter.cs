using UnityEditor.AssetImporters;
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
            
            BlenderProcessHandler.OnBlenderProcessFinished onBlenderProcessFinished = BlendProcessFinished;
            
            // Gather blender process arguments.
            var blenderExecutablePath = "C:\\Program Files\\Blender Foundation\\Blender 2.83\\blender.exe";
            var pythonScript = "";
            var blendFilePath = ctx.assetPath;
            var args = "";
            
            // get blender association.
            var path = BlendDefaultApplicationFinder.AssocQueryString(AssocStr.Executable, "blend");
            Debug.Log(path);
            
            BlenderProcessHandler.RunBlender(blenderExecutablePath, pythonScript, blendFilePath, args, onBlenderProcessFinished);
            
            
        }

        private void BlendProcessFinished(string outputPath, bool success)
        {
            
        }
    }
}