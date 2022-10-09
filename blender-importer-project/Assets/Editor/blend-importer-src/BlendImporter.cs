using System;
using System.Security.Principal;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Rendering;

namespace BlenderImporter
{
    [ScriptedImporter(1, new[] { "made_by_simon" }, new[] { "blend" })]
    public class BlendImporter : ScriptedImporter
    {
        

        [Tooltip("Advanced Import Mode: Allows you to export multiple assets from the same blender file.")]
        public bool advancedImportMode;
        
        public ModelImporterSettings modelImporterSettings;

        private AssetImportContext _ctx;
        public override void OnImportAsset(AssetImportContext ctx)
        {
            _ctx = ctx;
            // TODO: Start a Blender Process
            // TODO: Export everything in the Blender File.
            // TODO: Mark when it's completed.
            // To begin with, let's just export the file by opening a blender process.
            Debug.Log("Starting Custom Blender Importer");
            BlenderProcessHandler.OnBlenderProcessFinished onBlenderProcessFinished = BlendProcessFinished;
            
            // Gather blender process arguments.
            var blenderExecutable = BlendDefaultApplicationFinder.GetExecFileAssociatedToExtension(".blend");
            var pythonScript = @"E:\repos\blender-importer\blender-importer-project\Assets\Editor\blend-importer-src\blend-exporter.py";
            var blendFilePath = ctx.assetPath;
            var args = "";
            
            BlenderProcessHandler.RunBlender(blenderExecutable, pythonScript, blendFilePath, args, onBlenderProcessFinished);
        }

        private void BlendProcessFinished(string outputPath, bool success)
        {
            AssetDatabase.Refresh();

            // get the fbx.
            var fbxPath = outputPath + ".fbx";
            Debug.Log("FBX Path: " + fbxPath);
            
            
            var fbxObject = AssetDatabase.LoadAssetAtPath<GameObject>(fbxPath);
            Debug.Log("FBX Object: " + fbxObject);
            
            var mesh = fbxObject.GetComponent<MeshFilter>().sharedMesh;
            _ctx.AddObjectToAsset(fbxPath,mesh);
            _ctx.AddObjectToAsset(fbxPath, fbxObject);
            _ctx.SetMainObject(fbxObject);
            
            AssetDatabase.Refresh();
            
            AssetDatabase.DeleteAsset(fbxPath);
            
            AssetDatabase.Refresh();

            Debug.Log("Blender Process Finished");
        }
    }
    
    [Serializable]
    public class ModelImporterSettings
    {
        [Header("Scene")]
        public float scaleFactor = 1.0f;
        public bool convertUnits = true;
        public bool bakeAxisConversion = false;
        public bool importBlendShapes = true;
        public bool importVisibility = true;
        public bool importCameras = true;
        public bool importLights = true;
        public bool preserveHierarchy = false;
        public bool sortHierarchyByName = true;
        
        [Header("Meshes")]
        public ModelImporterMeshCompression meshCompression = ModelImporterMeshCompression.Off;
        public bool isReadable = false;
        public MeshOptimizationFlags optimizeMesh = MeshOptimizationFlags.Everything;
        public bool generateColliders = false;

        [Header("Geometry")] public bool keepQuads = false;
        public bool weldVertices = true;
        public ModelImporterIndexFormat indexFormat = ModelImporterIndexFormat.Auto;
        public bool legacyBlendShapeNormals = false;
        public ModelImporterNormals normals = ModelImporterNormals.Import;
        public ModelImporterNormals blendShapeNormals = ModelImporterNormals.Calculate;
        public ModelImporterNormalCalculationMode normalsMode = ModelImporterNormalCalculationMode.AreaAndAngleWeighted;
        public int smoothingAngle = 60;
        public ModelImporterTangents tangents = ModelImporterTangents.CalculateMikk;
        public bool swapUvs = false;
        public bool generateLightmapUvs = false;
        
    }
}