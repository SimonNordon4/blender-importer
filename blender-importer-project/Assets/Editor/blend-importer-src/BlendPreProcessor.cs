﻿using System;
using UnityEditor;
using UnityEngine;

namespace BlenderImporterV1
{
    /// <summary>
    /// We have to apply the BlenderScriptedImporter to any .blend files we want to import.
    /// </summary>
    public class BlendPreProcessor : AssetPostprocessor
    {
        //https://gist.github.com/TJHeuvel/f74acbbbcfe8e84e59fa41ebff774f35
        private void OnPreprocessAsset()
        {
            // var path = assetPath;
            // if (!path.Contains(".blend")) return;
            //
            // Debug.Log("Overriding importer for " + path);
            // var currentOverride = AssetDatabase.GetImporterOverride(path);
            // Debug.Log("Current override: " + currentOverride);
            // if (currentOverride != typeof(BlendImporter)) AssetDatabase.SetImporterOverride<BlendImporter>(path);
        }
    }
}