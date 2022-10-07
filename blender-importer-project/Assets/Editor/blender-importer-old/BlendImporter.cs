using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System;
using System.IO;

namespace BlenderImporterOld
{
    [ScriptedImporter(1, new[] { "made_by_simon" }, new[] { "blend-old" })]
    public class BlendImporter : ScriptedImporter
    {
        [Tooltip(
            "This will create empty gameobjects for collections, and parent all blender objects in that collection to them.")]
        public bool ImportCollectionAsGameObjects = true;

        [Tooltip(
            "Attempt to convert blender materials into unity materials. Currently only works for BDSF & Emission Shaders.")]
        public bool ImportMaterialsAndTextures = true;

        [Tooltip("Will attempt to evalute Blender Shader node into a texture that can be used in Unity.")]
        public bool BakeShaderNodeToTexture = true;

        private AssetImportContext _ctx;

        public override void OnImportAsset(AssetImportContext ctx)
        {
            _ctx = ctx;
            // Gather blender process arguments.
            var blenderExectuablePath = BlenderImporterGlobalSettings.instance.GetBlenderExecutablePath();
            var pythonExectuablePath = BlenderImporterGlobalSettings.instance.GetPythonScriptLocation();
            var blendFilePath = ctx.assetPath;
            var args = ResolvePythonArgs(ctx.assetPath);
            BlenderProcessHandler.OnBlenderProcessFinished onBlenderProcessFinished = BlendProcessFinished;

            // Run Blender Process
            BlenderProcessHandler.RunBlender(blenderExectuablePath, pythonExectuablePath, blendFilePath, args,
                onBlenderProcessFinished);
        }

        /// <summary>
        /// Generate import arguments to be passed to the blender process.
        /// </summary>
        /// <param name="assetPath">assetPath to the asset being imported</param>
        /// <returns>a string in the form of a dictionary of arguments.</returns>
        private string ResolvePythonArgs(string assetPath)
        {
            var args = string.Empty;
            var blend_path = GetBlendPath(assetPath);
            var blend_name = GetBlendName(assetPath);
            var import_collections = ImportCollectionAsGameObjects;
            var import_materials = ImportMaterialsAndTextures;
            var bake_shaders = BakeShaderNodeToTexture;
            args += $" {nameof(blend_path)}={blend_path}";
            args += $" {nameof(blend_name)}={blend_name}";
            args += $" {nameof(import_collections)}={import_collections}";
            args += $" {nameof(import_materials)}={import_materials}";
            args += $" {nameof(bake_shaders)}={bake_shaders}";
            return args;
        }

        /// <summary>
        /// Get the name of the .blend file from it's asset path
        /// </summary>
        private string GetBlendName(string assetPath)
        {
            return Path.GetFileNameWithoutExtension(assetPath);
        }

        /// <summary>
        /// Get the full path of the blend file from it's asset path
        private string GetBlendPath(string assetPath)
        {
            var projectPath = Application.dataPath.Replace("Assets", "");
            return projectPath + assetPath.Replace(Path.GetFileName(assetPath), "");
        }

        /// <summary>
        /// Callback for when the blender process has finished either with error or success.
        /// </summary>
        private void BlendProcessFinished(string outputPath, bool success)
        {
            Debug.Log("finished");
        }
    }
}