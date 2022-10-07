using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;

namespace BlenderImporter
{
    public class BlendPreProcessor : AssetPostprocessor
    {
        //https://gist.github.com/TJHeuvel/f74acbbbcfe8e84e59fa41ebff774f35
        public bool TestButton;
        void OnPreprocessAsset()
            {
                var path = assetPath;
                if(path.Contains(".blend1")) return;
                if(path.Contains(".blend"))
                {
                    var currentOveride = AssetDatabase.GetImporterOverride(path);
                    if (currentOveride == null)
                    {
                        AssetDatabase.SetImporterOverride<BlendImporter>(path);
                    }
                }
            }
    }
}