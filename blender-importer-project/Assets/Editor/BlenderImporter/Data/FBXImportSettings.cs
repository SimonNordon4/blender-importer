using System;
using UnityEditor;
using UnityEngine;

namespace BlenderImporter.Data
{
    [Serializable]
    public class FBXImportSettings
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