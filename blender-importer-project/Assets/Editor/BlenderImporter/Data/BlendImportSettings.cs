using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlenderImporter.Data
{
    /// <summary>
    /// Contains data for the Blender Export.
    /// DO NOT CHANGE variable names, as they're linked to the JSON.
    /// </summary>
    [Serializable]
    public class BlendImportSettings
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
}