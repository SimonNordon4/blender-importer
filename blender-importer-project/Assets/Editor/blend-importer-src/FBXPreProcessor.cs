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
            
            var blendImporter = BlendImporter.Importers[blendPath];
            
            ModelImporter modelImporter = assetImporter as ModelImporter;
            if (modelImporter != null)
            {
                // Model - Scene
                modelImporter.globalScale = blendImporter.modelImporterSettings.globalScale;
                modelImporter.useFileUnits = blendImporter.modelImporterSettings.useFileUnits;
                modelImporter.bakeAxisConversion = blendImporter.modelImporterSettings.bakeAxisConversion;
                modelImporter.importBlendShapes = blendImporter.modelImporterSettings.importBlendShapes;
                modelImporter.importVisibility = blendImporter.modelImporterSettings.importVisibility;
                modelImporter.importCameras = blendImporter.modelImporterSettings.importCameras;
                modelImporter.importLights = blendImporter.modelImporterSettings.importLights;
                modelImporter.preserveHierarchy = blendImporter.modelImporterSettings.preserveHierarchy;
                modelImporter.sortHierarchyByName = blendImporter.modelImporterSettings.sortHierarchyByName;
                
                // Model - Meshes
                modelImporter.meshCompression = blendImporter.modelImporterSettings.meshCompression;
                modelImporter.isReadable = blendImporter.modelImporterSettings.isReadable;
                modelImporter.meshOptimizationFlags = blendImporter.modelImporterSettings.meshOptimizationFlags;
                modelImporter.addCollider = blendImporter.modelImporterSettings.addCollider;  
                
                // Model - Geometry
                modelImporter.keepQuads = blendImporter.modelImporterSettings.keepQuads;
                modelImporter.weldVertices = blendImporter.modelImporterSettings.weldVertices;
                modelImporter.indexFormat = blendImporter.modelImporterSettings.indexFormat;
                // TODO: Add legacy Blend Shape Normals
                modelImporter.importNormals = blendImporter.modelImporterSettings.importerNormals;
                modelImporter.importBlendShapeNormals = blendImporter.modelImporterSettings.importBlendShapeNormals;
                modelImporter.normalCalculationMode = blendImporter.modelImporterSettings.normalCalculationMode;
                modelImporter.normalSmoothingSource = blendImporter.modelImporterSettings.normalSmoothingSource;
                modelImporter.normalSmoothingAngle = blendImporter.modelImporterSettings.normalSmoothingAngle;
                modelImporter.importTangents = blendImporter.modelImporterSettings.importTangents;
                modelImporter.swapUVChannels = blendImporter.modelImporterSettings.swapUVChannels;
                modelImporter.generateSecondaryUV = blendImporter.modelImporterSettings.generateSecondaryUV;
                
                // Model - Lightmap Settings
                modelImporter.secondaryUVHardAngle =
                    blendImporter.modelImporterSettings.lightMapSettings.secondaryUVHardAngle;
                modelImporter.secondaryUVAngleDistortion =
                    blendImporter.modelImporterSettings.lightMapSettings.secondaryUVAngleDistortion;
                modelImporter.secondaryUVAreaDistortion =
                    blendImporter.modelImporterSettings.lightMapSettings.secondaryUVAreaDistortion;
                modelImporter.secondaryUVMarginMethod = 
                    blendImporter.modelImporterSettings.lightMapSettings.secondaryUVMarginMethod;
                modelImporter.secondaryUVMinLightmapResolution =
                    blendImporter.modelImporterSettings.lightMapSettings.secondaryUVMinLightmapResolution;
                modelImporter.secondaryUVMinObjectScale =
                    blendImporter.modelImporterSettings.lightMapSettings.secondaryUVMinObjectScale;
            }
        }
    }
}