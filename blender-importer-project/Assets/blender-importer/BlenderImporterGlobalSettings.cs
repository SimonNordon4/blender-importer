using UnityEngine;
using UnityEditor;

namespace BlenderImporter
{
    [CreateAssetMenu(fileName = "BlenderGlobalImportSettings", menuName = "BlenderImporter/BlenderGlobalImportSettings")]
    public class BlenderImporterGlobalSettings : ScriptableObject
    {
        private static BlenderImporterGlobalSettings _instance;
        public static BlenderImporterGlobalSettings instance
        {
            get
            {
                if (_instance is null)
                {
                    var settings = ScriptableObject.CreateInstance<BlenderImporterGlobalSettings>();
                    AssetDatabase.CreateAsset(settings, "Assets/blender-importer/BlenderGlobalImportSettings.asset");
                    AssetDatabase.SaveAssets();
                    _instance = settings;
                    AssetDatabase.Refresh();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        private string _blenderExecutablePath;
        private string _pythonScriptName = "blender_importer.py";
        private string _pythonScriptLocation = "";
        public string PythonScriptLocation { get; private set;}

        [Header("Console Logging")]
        public Color PythonConsoleLabelColor = new Color(0.22f, 1f, 0.0f);
        public Color PythonConsoleTextColor = new Color(0.44f, 1f, 0.22f);
        public Color ErrorConsoleLabelColor = new Color(1f, 0.4f, 0.4f);
        public Color ErrorConsoleTextColor = new Color(1f, 0.6f, 0.6f);
        public Color StopWatchColor = Color.white;
        public Color BlendDataColor = Color.white;
        public Color AssetCreationColor = Color.white;

        public string GetBlenderExecutablePath()
        {
            // TODO - Actually find the blender executable. Serialize its locatioin, always check if it exists when executing. Give user a chance to manually set it if all else fails.
            return @"C:\Program Files\Blender Foundation\Blender 3.2\blender.exe";
        }

        /// <summary>
        /// Find the script location of the blender-importer.py script. If it isn't in the default location, search the unity directory for it.
        /// </summary>
        /// <returns>the path to the python importer script</returns>
        public string GetPythonScriptLocation()
        {
            var defaultLocation = Application.dataPath + "/blender-importer/" + _pythonScriptName;

            if(System.IO.File.Exists(defaultLocation))
            {
                _pythonScriptLocation = defaultLocation;
                return _pythonScriptLocation;
            }

            Debug.LogWarning($"Could not find blender script at {defaultLocation}. Searching for it in the Unity directory.");

            string[] files = System.IO.Directory.GetFiles(Application.dataPath, "*"+_pythonScriptName, System.IO.SearchOption.AllDirectories);

            if(files.Length > 0)
            {
                _pythonScriptLocation = files[0];
            }

            throw new System.Exception($"Could not find {defaultLocation} in the project, unable to import blend files.");
        }
    }

    public class ScriptableObjectPostProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            for (var index = 0; index < importedAssets.Length; index++)
            {
                var importedAsset = importedAssets[index];

                if (importedAsset.Contains(".asset"))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<Object>(importedAsset);

                    if (asset.GetType() == typeof(BlenderImporterGlobalSettings))
                    {
                        BlenderImporterGlobalSettings importSettings = (BlenderImporterGlobalSettings)asset;
                        if(importSettings != BlenderImporterGlobalSettings.instance)
                        {
                            Debug.LogWarning("Only one instance of BlenderImporterGlobalSettings is allowed! Deleting the duplicate.");
                            AssetDatabase.DeleteAsset(importedAsset);
                        }
                    }
                }
            }
        }
    }
}