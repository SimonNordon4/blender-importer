using UnityEditor;

namespace BlenderImporter.Processors
{
    /// <summary>
    /// Pre Process .blend files to set it's importer to our custom blend importer.
    /// </summary>
    public class BlendPreProcessor : AssetPostprocessor
    {
        // https://gist.github.com/TJHeuvel/f74acbbbcfe8e84e59fa41ebff774f35
        private void OnPreprocessAsset()
        {
            var path = assetPath;
            
            if (!path.Contains(".blend")) return;
            
            if (AssetDatabase.GetImporterOverride(path) != typeof(BlendImporter))
                AssetDatabase.SetImporterOverride<BlendImporter>(path);
        }
    }
}