using UnityEngine;

namespace BlenderImporter
{
    /// <summary>
    /// Utility Class for Blender Importer.
    /// </summary>
    public class f
    {
        public static void print(object obj){ UnityEngine.Debug.Log(obj); }

        public static void print(object obj, Color color)
        { 
            UnityEngine.Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{obj}</color>");
        }

        public static void print(object obj, Color color ,string label, Color labelColor)
        { 
            UnityEngine.Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(labelColor)}><b>{label}: </b></color> <color=#{ColorUtility.ToHtmlStringRGB(color)}>{obj}</color>");
        }
        public static void printError(object obj)
        { 
            UnityEngine.Debug.LogError(obj);
        }
        public static void printError(object obj, string label)
        {
            var labelColor = ColorUtility.ToHtmlStringRGB(BlenderImporterGlobalSettings.instance.ErrorConsoleLabelColor);
            var textColor = ColorUtility.ToHtmlStringRGB(BlenderImporterGlobalSettings.instance.ErrorConsoleTextColor);
            UnityEngine.Debug.LogWarning($"<color=#{labelColor}><b>{label}: </b></color><color=#{textColor}>{obj}</color>");
        }
        
    }
}