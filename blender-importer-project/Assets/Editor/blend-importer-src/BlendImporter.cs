using System;
using System.Collections.Generic;
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
        public static Dictionary<string, BlendImporter> Importers = new Dictionary<string, BlendImporter>();

        [Tooltip("Advanced Import Mode: Allows you to export multiple assets from the same blender file.")]
        public bool advancedImportMode;

        public ModelImporterSettings modelImporterSettings;

        private AssetImportContext _ctx;

        public void OnValidate()
        {
            // check if the importers dictionary contains this importer
            if (!Importers.ContainsKey(assetPath))
            {
                Importers.Add(assetPath, this);
            }
        }

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
            var pythonScript =
                @"E:\repos\blender-importer\blender-importer-project\Assets\Editor\blend-importer-src\blend-exporter.py";
            var blendFilePath = ctx.assetPath;
            var args = "";

            BlenderProcessHandler.RunBlender(blenderExecutable, pythonScript, blendFilePath, args,
                onBlenderProcessFinished);
        }

        private void BlendProcessFinished(string outputPath, bool success)
        {
            AssetDatabase.Refresh();

            // get the fbx.
            var fbxPath = outputPath + ".fbx";
            Debug.Log("FBX Path: " + fbxPath);


            var fbxObject = AssetDatabase.LoadAssetAtPath<GameObject>(fbxPath);
            Debug.Log("FBX Object: " + fbxObject);

            // var mesh = fbxObject.GetComponent<MeshFilter>().sharedMesh;
            // _ctx.AddObjectToAsset(fbxPath,mesh);
            // _ctx.AddObjectToAsset(fbxPath, fbxObject);
            // _ctx.SetMainObject(fbxObject);

            AssetDatabase.Refresh();

            //AssetDatabase.DeleteAsset(fbxPath);

            AssetDatabase.Refresh();

            Debug.Log("Blender Process Finished");
        }
    }

    [Serializable]
    public class ModelImporterSettings
    {
        // Model
        [Header("Scene")] public float globalScale = 1.0f; // Scale Factor
        public bool useFileUnits = true; // Convert Units
        public bool bakeAxisConversion = false;
        public bool importBlendShapes = true;
        public bool importVisibility = true;
        public bool importCameras = true;
        public bool importLights = true;
        public bool preserveHierarchy = false;
        public bool sortHierarchyByName = true;

        [Header("Meshes")] public ModelImporterMeshCompression meshCompression = ModelImporterMeshCompression.Off;
        public bool isReadable = false; // Read/Write Enabled
        public MeshOptimizationFlags meshOptimizationFlags = MeshOptimizationFlags.Everything; // Optimize Mesh
        public bool addCollider = false; // Generate Colliders

        [Header("Geometry")] public bool keepQuads = false;
        public bool weldVertices = true;
        public ModelImporterIndexFormat indexFormat = ModelImporterIndexFormat.Auto;
        public bool legacyBlendShapeNormals = false;
        public ModelImporterNormals importerNormals = ModelImporterNormals.Import;
        public ModelImporterNormals importBlendShapeNormals = ModelImporterNormals.Calculate;

        public ModelImporterNormalCalculationMode normalCalculationMode =
            ModelImporterNormalCalculationMode.AreaAndAngleWeighted;

        public ModelImporterNormalSmoothingSource normalSmoothingSource =
            ModelImporterNormalSmoothingSource.PreferSmoothingGroups;

        public int normalSmoothingAngle = 60;
        public ModelImporterTangents importTangents = ModelImporterTangents.CalculateMikk;
        public bool swapUVChannels = false;
        public bool generateSecondaryUV = false;

        // Geometry Lightmap Settings
        
        public float secondaryUVHardAngle = 88.0f;
        public float secondaryUVAngleDistortion = 8.0f;
        public float secondaryUVAreaDistortion = 15.0f;
        public ModelImporterSecondaryUVMarginMethod secondaryUVMarginMethod =
            ModelImporterSecondaryUVMarginMethod.Calculate;
        public int secondaryUVMinLightmapResolution = 40;
        public float secondaryUVMinObjectScale = 1.0f;

        // Rigs not supported.

        // Animation 
        public bool importConstraints = false;
        public bool importAnimation = true;
        public bool resampleCurves = true;

        public ModelImporterAnimationCompression animationCompression =
            ModelImporterAnimationCompression.KeyframeReductionAndCompression;

        public float rotationError = 0.5f;
        public float positionError = 0.5f;
        public float scaleError = 0.5f;
        public bool animatedCustomProperties = false;
        public ModelImporterClipAnimation clipAnimations;

    }

    [Serializable]
    public class LightMapSettings
    {
        
    }

}