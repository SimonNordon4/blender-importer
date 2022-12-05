using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace BlenderImporter.Processors
{
    public class FBXProcessor : AssetPostprocessor
    {
        private void OnPreprocessAsset()
        {
            var path = assetPath;
            // remove the fbx file extension
            var blendPath = assetPath.Replace(".fbx", "");

            Debug.Log("OnPreprocessAsset: " + path);
            // Check if the fbx is associated with a blend file
            if (!File.Exists(blendPath)) return;

            // Check if the fbx has a blend Importer
            if (!BlendImporter.BlendImporters.ContainsKey(blendPath)) return;

            var blendImporter = BlendImporter.BlendImporters[blendPath];

            ModelImporter modelImporter = assetImporter as ModelImporter;
            if (modelImporter != null)
            {
                var ms = blendImporter.fbxSettings;
                // Model - Scene
                modelImporter.globalScale = ms.globalScale;
                modelImporter.useFileUnits = ms.useFileUnits;
                modelImporter.bakeAxisConversion = ms.bakeAxisConversion;
                modelImporter.importBlendShapes = ms.importBlendShapes;
                modelImporter.importVisibility = ms.importVisibility;
                modelImporter.importCameras = ms.importCameras;
                modelImporter.importLights = ms.importLights;
                modelImporter.preserveHierarchy = ms.preserveHierarchy;
                modelImporter.sortHierarchyByName = ms.sortHierarchyByName;

                // Model - Meshes
                modelImporter.meshCompression = ms.meshCompression;
                modelImporter.isReadable = ms.isReadable;
                modelImporter.meshOptimizationFlags = ms.meshOptimizationFlags;
                modelImporter.addCollider = ms.addCollider;

                // Model - Geometry
                modelImporter.keepQuads = ms.keepQuads;
                modelImporter.weldVertices = ms.weldVertices;
                modelImporter.indexFormat = ms.indexFormat;
                // TODO: Add legacy Blend Shape Normals
                modelImporter.importNormals = ms.importerNormals;
                modelImporter.importBlendShapeNormals = ms.importBlendShapeNormals;
                modelImporter.normalCalculationMode = ms.normalCalculationMode;
                modelImporter.normalSmoothingSource = ms.normalSmoothingSource;
                modelImporter.normalSmoothingAngle = ms.normalSmoothingAngle;
                modelImporter.importTangents = ms.importTangents;
                modelImporter.swapUVChannels = ms.swapUVChannels;
                modelImporter.generateSecondaryUV = ms.generateSecondaryUV;

                // Model - Lightmap Settings
                modelImporter.secondaryUVHardAngle = ms.secondaryUVHardAngle;
                modelImporter.secondaryUVAngleDistortion = ms.secondaryUVAngleDistortion;
                modelImporter.secondaryUVAreaDistortion = ms.secondaryUVAreaDistortion;
                modelImporter.secondaryUVMarginMethod = ms.secondaryUVMarginMethod;
                modelImporter.secondaryUVMinLightmapResolution = ms.secondaryUVMinLightmapResolution;
                modelImporter.secondaryUVMinObjectScale = ms.secondaryUVMinObjectScale;
            }
        }
    }
}