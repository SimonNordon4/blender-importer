using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace BlenderImporter
{
    /// <summary>
    /// Used to apply BlendImporter Settings to any imported FBX files.
    /// </summary>
    public class FBXPreProcessor : AssetPostprocessor
    {
        //https://gist.github.com/TJHeuvel/f74acbbbcfe8e84e59fa41ebff774f35
        private void OnPreprocessAsset()
        {
            var path = assetPath;
            // remove the fbx file extension
            var blendPath = assetPath.Replace(".fbx", "");

            Debug.Log("OnPreprocessAsset: " + path);
            // Check if the fbx is associated with a blend file
            if (!File.Exists(blendPath)) return;

            // Check if the fbx has a blend Importer
            if(!BlendImporter.Importers.ContainsKey(blendPath)) return;
            
            var b = BlendImporter.Importers[blendPath];
            
            ModelImporter modelImporter = assetImporter as ModelImporter;
            if (modelImporter != null)
            {
                // Model - Scene
                modelImporter.globalScale = b.ms.globalScale;
                modelImporter.useFileUnits = b.ms.useFileUnits;
                modelImporter.bakeAxisConversion = b.ms.bakeAxisConversion;
                modelImporter.importBlendShapes = b.ms.importBlendShapes;
                modelImporter.importVisibility = b.ms.importVisibility;
                modelImporter.importCameras = b.ms.importCameras;
                modelImporter.importLights = b.ms.importLights;
                modelImporter.preserveHierarchy = b.ms.preserveHierarchy;
                modelImporter.sortHierarchyByName = b.ms.sortHierarchyByName;
                
                // Model - Meshes
                modelImporter.meshCompression = b.ms.meshCompression;
                modelImporter.isReadable = b.ms.isReadable;
                modelImporter.meshOptimizationFlags = b.ms.meshOptimizationFlags;
                modelImporter.addCollider = b.ms.addCollider;  
                
                // Model - Geometry
                modelImporter.keepQuads = b.ms.keepQuads;
                modelImporter.weldVertices = b.ms.weldVertices;
                modelImporter.indexFormat = b.ms.indexFormat;
                // TODO: Add legacy Blend Shape Normals
                modelImporter.importNormals = b.ms.importerNormals;
                modelImporter.importBlendShapeNormals = b.ms.importBlendShapeNormals;
                modelImporter.normalCalculationMode = b.ms.normalCalculationMode;
                modelImporter.normalSmoothingSource = b.ms.normalSmoothingSource;
                modelImporter.normalSmoothingAngle = b.ms.normalSmoothingAngle;
                modelImporter.importTangents = b.ms.importTangents;
                modelImporter.swapUVChannels = b.ms.swapUVChannels;
                modelImporter.generateSecondaryUV = b.ms.generateSecondaryUV;
                
                // Model - Lightmap Settings
                modelImporter.secondaryUVHardAngle = b.ms.secondaryUVHardAngle;
                modelImporter.secondaryUVAngleDistortion = b.ms.secondaryUVAngleDistortion;
                modelImporter.secondaryUVAreaDistortion = b.ms.secondaryUVAreaDistortion;
                modelImporter.secondaryUVMarginMethod =  b.ms.secondaryUVMarginMethod;
                modelImporter.secondaryUVMinLightmapResolution = b.ms.secondaryUVMinLightmapResolution;
                modelImporter.secondaryUVMinObjectScale = b.ms.secondaryUVMinObjectScale;
            }
        }
    }
}