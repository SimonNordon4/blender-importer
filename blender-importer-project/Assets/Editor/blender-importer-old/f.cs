using UnityEngine;

namespace BlenderImporterOld
{
    /// <summary>
    /// Utility Class for Blender Importer.
    /// </summary>
    public class f
    {
        public static void print(object obj)
        {
            Debug.Log(obj);
        }

        public static void print(object obj, Color color)
        {
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{obj}</color>");
        }

        public static void print(object obj, Color color, string label, Color labelColor)
        {
            Debug.Log(
                $"<color=#{ColorUtility.ToHtmlStringRGB(labelColor)}><b>{label}: </b></color> <color=#{ColorUtility.ToHtmlStringRGB(color)}>{obj}</color>");
        }

        public static void printError(object obj)
        {
            Debug.LogError(obj);
        }

        public static void printError(object obj, string label)
        {
            var labelColor =
                ColorUtility.ToHtmlStringRGB(BlenderImporterGlobalSettings.instance.ErrorConsoleLabelColor);
            var textColor = ColorUtility.ToHtmlStringRGB(BlenderImporterGlobalSettings.instance.ErrorConsoleTextColor);
            Debug.LogWarning($"<color=#{labelColor}><b>{label}: </b></color><color=#{textColor}>{obj}</color>");
        }
    }
}