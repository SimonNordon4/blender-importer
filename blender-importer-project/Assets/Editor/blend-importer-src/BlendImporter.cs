using System;
using System.Collections.Generic;
using System.Security.Principal;
using NUnit.Framework;
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

        /// <summary> Blend Importer Settings </summary>
        public BlendImporterSettings bs;
        
        /// <summary> Model Importer Settings </summary>
        public ModelImporterSettings ms;

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
            AssetDatabase.StartAssetEditing();
            _ctx = ctx;
            // To begin with, let's just export the file by opening a blender process.
            Debug.Log("Starting Custom Blender Importer");
            BlenderProcessHandler.OnBlenderProcessFinished onBlenderProcessFinished = BlendProcessFinished;

            // Gather blender process arguments.
            var blenderExecutable = BlendDefaultApplicationFinder.GetExecFileAssociatedToExtension(".blend");
            var pythonScript =
                @"E:\repos\blender-importer\blender-importer-project\Assets\Editor\blend-importer-src\blend-exporter.py";
            var blendFilePath = ctx.assetPath;
            var args = "";
            
            // Write blender settings to a json.
            var jsonString = EditorJsonUtility.ToJson(bs);
            var jsonPath = blendFilePath + ".json";
            System.IO.File.WriteAllText(jsonPath, jsonString);
            

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



            //AssetDatabase.DeleteAsset(fbxPath);
            AssetDatabase.StopAssetEditing();


            Debug.Log("Blender Process Finished");
        }
    }
    
    [Serializable]
    public class BlendImporterSettings
    {
        public enum ExportVisibleMode { Visible, All }
        [Header("General")]
        public ExportVisibleMode exportVisible = ExportVisibleMode.Visible;
        
        [Flags]
        public enum ExportTypes { EMPTY = 1, CAMERA = 2, LIGHT = 4, ARMATURE = 8, MESH = 16, OTHER = 32 }
        public ExportTypes exportObjects = ExportTypes.EMPTY | ExportTypes.CAMERA | ExportTypes.LIGHT | ExportTypes.ARMATURE | ExportTypes.MESH | ExportTypes.OTHER;
        
        [Header("Collection Settings")]
        public bool exportCollections = true;
        public enum CollectionExportMode { Include, Exclude }
        public CollectionExportMode collectionFilterMode = CollectionExportMode.Exclude;
        public List<string> collectionNames = new List<string>();

        [Header("Mesh Settings")]
        public bool triangulateMesh = false;
        public bool applyModifiers = true;
        public bool embedTextures = true;

        [Header("Animation Settings")]
        public bool bakeAnimation = true;
        public bool bakeAnimationNlaStrips = true;
        public bool bakeAnimationActions = true;
        [UnityEngine.Range(0,100)]
        public int simplifyBakeAnimation = 1;
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

        [UnityEngine.Range(0,180)]
        public int normalSmoothingAngle = 60;
        public ModelImporterTangents importTangents = ModelImporterTangents.CalculateMikk;
        public bool swapUVChannels = false;
        public bool generateSecondaryUV = false;

        // Geometry Lightmap Settings
        
        [UnityEngine.Range(0,180)]
        public float secondaryUVHardAngle = 88.0f;
        [UnityEngine.Range(1,75)]
        public float secondaryUVAngleDistortion = 8.0f;
        [UnityEngine.Range(1,75)]
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

        public float animationRotationError = 0.5f;
        public float animationPositionError = 0.5f;
        public float animationScaleError = 0.5f;
        public bool importAnimatedCustomProperties = false;
        
        // TODO: Add animation clips
        // public string[] referencedClips = Array.Empty<string>();
    }
}