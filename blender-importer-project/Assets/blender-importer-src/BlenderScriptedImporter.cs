using UnityEditor.AssetImporters;

namespace BlenderImporter
{
    [ScriptedImporter(1, new[] { "made_by_simon" }, new[] { "blend" })]
    public class BlenderScriptedImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            // We want the UI to have some kind of "Apply" on import.
            throw new System.NotImplementedException();
        }
    }
}