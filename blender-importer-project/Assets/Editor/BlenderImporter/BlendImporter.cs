using System.Collections.Generic;
using BlenderImporter.Data;
using UnityEditor.AssetImporters;

namespace BlenderImporter
{
    /// <summary>
    /// The Blend Importer overrides the default behavior of imported .blend files.
    /// </summary>
    [ScriptedImporter(1, new[] {"made_by_simon"}, new[]{"blend"})]
    public class BlendImporter : ScriptedImporter
    {
        /// <summary>
        /// A static dictionary of all active blend importers, so that imported assets can associate with their importer.
        /// </summary>
        public static Dictionary<string, BlendImporter> BlendImporters = new Dictionary<string, BlendImporter>();

        //public BlendImportSettings BlendSettings;
        public bool BlendSettings;
        public bool Test = true;
    
        public override void OnImportAsset(AssetImportContext ctx)
        {
            
        }
    }
}

