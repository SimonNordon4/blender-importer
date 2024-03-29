using System;
using System.Collections.Generic;
using BlenderImporter.Data;
using BlenderImporter.ProcessHandler;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

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
        public static readonly Dictionary<string, BlendImporter> BlendImporters = new Dictionary<string, BlendImporter>();

        /// <summary>
        /// Unique action for when the blender process associated with this importer has finished.
        /// </summary>
        public Action BlenderProcessFinished() => BlendProcessFinished;
        
        public BlendImportSettings blendSettings = new BlendImportSettings();
        public FBXImportSettings fbxSettings = new FBXImportSettings();

        /// <summary>
        /// Called when the .blend file is imported.
        /// </summary>
        // Happy Path:
        //  - Find blender.exe and the python path.
        //  - Save blender export settings to json file.
        //  - Start blender process.
        //  - Export FBX From Blender
        //  - Import FBX into Unity. Apply FBX Settings prior to import.
        public override void OnImportAsset(AssetImportContext ctx)
        {
            InitialiseImporter();
            var blenderExe = BlendDefaultApplicationFinder.GetExecFileAssociatedToExtension(".blend");
            // TODO: Find the python script.
            var pythonScript = FindPythonPath();
            var blendFilePath = ctx.assetPath;
            var args = "";
            
            // create a json file of the Blender Settings.
            var settingsJson = JsonUtility.ToJson(blendSettings);
            var settingsPath = blendFilePath + ".json";
            System.IO.File.WriteAllText(settingsPath, settingsJson);
            
            BlenderProcessHandler.RunBlender(blenderExe, pythonScript, blendFilePath, args,BlenderProcessFinished());
        }

        private void InitialiseImporter()
        {
            if (BlendImporters.ContainsKey(assetPath))
            {
                BlendImporters[assetPath] = this;
            }
            else
            {
                BlendImporters.Add(assetPath, this);
            }
        }

        private void BlendProcessFinished()
        {
           // Debug.Log("Blend Process has Finished!");
           // delete the json file.
            var settingsPath = assetPath + ".json";
            AssetDatabase.DeleteAsset(settingsPath);
        }
        public void FBXImported(GameObject g)
        {
           //Debug.Log(g.name + " has been imported");
        }

        private static string FindPythonPath()
        {
            var pythonFileName = @"blend-exporter";
            var pyGUIDs =AssetDatabase.FindAssets(pythonFileName);
            
            foreach (var guid in pyGUIDs)
            {
                var pyPath = AssetDatabase.GUIDToAssetPath(guid);
                // check if the asset name is equal to "blend-exporter.py"
                if (pyPath.EndsWith(pythonFileName + ".py"))
                {
                    return pyPath;
                }
            }
            
            throw new System.Exception("Could not find the python script");
        }
    }
}

