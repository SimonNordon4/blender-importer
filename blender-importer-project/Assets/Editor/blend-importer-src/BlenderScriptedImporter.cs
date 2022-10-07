using UnityEditor.AssetImporters;

namespace BlenderImporter
{
    [ScriptedImporter(1, new[] { "made_by_simon" }, new[] { "blend" })]
    public class BlenderScriptedImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // TODO: Start a Blender Process
            // TODO: Export everything in the Blender File.
            // TODO: Mark when it's completed.
            // To begin with, let's just export the file by opening a blender process.
            throw new System.NotImplementedException();
        }
    }
}