﻿using UnityEditor;

namespace BlenderImporter
{
    /// <summary>
    /// We have to apply the BlenderScriptedImporter to any .blend files we want to import.
    /// </summary>
    public class BlendPreProcessor : AssetPostprocessor
    {
        //https://gist.github.com/TJHeuvel/f74acbbbcfe8e84e59fa41ebff774f35
        private void OnPreprocessAsset()
        {
            var path = assetPath;
            if (!path.Contains(".blend")) return;

            var currentOverride = AssetDatabase.GetImporterOverride(path);
            if (currentOverride == null) AssetDatabase.SetImporterOverride<BlenderScriptedImporter>(path);
        }
    }
}