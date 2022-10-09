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
                modelImporter.globalScale = b.modelImporterSettings.globalScale;
                modelImporter.useFileUnits = b.modelImporterSettings.useFileUnits;
                modelImporter.bakeAxisConversion = b.modelImporterSettings.bakeAxisConversion;
                modelImporter.importBlendShapes = b.modelImporterSettings.importBlendShapes;
                modelImporter.importVisibility = b.modelImporterSettings.importVisibility;
                modelImporter.importCameras = b.modelImporterSettings.importCameras;
                modelImporter.importLights = b.modelImporterSettings.importLights;
                modelImporter.preserveHierarchy = b.modelImporterSettings.preserveHierarchy;
                modelImporter.sortHierarchyByName = b.modelImporterSettings.sortHierarchyByName;
                
                // Model - Meshes
                modelImporter.meshCompression = b.modelImporterSettings.meshCompression;
                modelImporter.isReadable = b.modelImporterSettings.isReadable;
                modelImporter.meshOptimizationFlags = b.modelImporterSettings.meshOptimizationFlags;
                modelImporter.addCollider = b.modelImporterSettings.addCollider;  
                
                // Model - Geometry
                modelImporter.keepQuads = b.modelImporterSettings.keepQuads;
                modelImporter.weldVertices = b.modelImporterSettings.weldVertices;
                modelImporter.indexFormat = b.modelImporterSettings.indexFormat;
                // TODO: Add legacy Blend Shape Normals
                modelImporter.importNormals = b.modelImporterSettings.importerNormals;
                modelImporter.importBlendShapeNormals = b.modelImporterSettings.importBlendShapeNormals;
                modelImporter.normalCalculationMode = b.modelImporterSettings.normalCalculationMode;
                modelImporter.normalSmoothingSource = b.modelImporterSettings.normalSmoothingSource;
                modelImporter.normalSmoothingAngle = b.modelImporterSettings.normalSmoothingAngle;
                modelImporter.importTangents = b.modelImporterSettings.importTangents;
                modelImporter.swapUVChannels = b.modelImporterSettings.swapUVChannels;
                modelImporter.generateSecondaryUV = b.modelImporterSettings.generateSecondaryUV;
                
                // Model - Lightmap Settings
                modelImporter.secondaryUVHardAngle = b.modelImporterSettings.secondaryUVHardAngle;
                modelImporter.secondaryUVAngleDistortion = b.modelImporterSettings.secondaryUVAngleDistortion;
                modelImporter.secondaryUVAreaDistortion = b.modelImporterSettings.secondaryUVAreaDistortion;
                modelImporter.secondaryUVMarginMethod =  b.modelImporterSettings.secondaryUVMarginMethod;
                modelImporter.secondaryUVMinLightmapResolution = b.modelImporterSettings.secondaryUVMinLightmapResolution;
                modelImporter.secondaryUVMinObjectScale = b.modelImporterSettings.secondaryUVMinObjectScale;
            }
        }
    }
}