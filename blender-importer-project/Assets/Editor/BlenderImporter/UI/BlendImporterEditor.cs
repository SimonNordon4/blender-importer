using System;
using BlenderImporter.Data;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace BlenderImporter.UI
{
    [CustomEditor(typeof(BlenderImporter.BlendImporter))]
    public class BlendImporterEditor : ScriptedImporterEditor
    {
        private int _toolbarInt = 1;
        private string[] _toolbarStrings = { "Blender", "Model", "Materials", "Animation" };
        private readonly GUILayoutOption[] _toolBarOptions = { GUILayout.MaxWidth(350), GUILayout.Height(25) };

        #region Blender Tab Properties

        private SerializedProperty _exportVisible;
        private SerializedProperty _exportObjects;
        private SerializedProperty _exportCollections;
        private SerializedProperty _collectionFilterMode;
        private SerializedProperty _collectionNames;
        private SerializedProperty _triangulateMesh;
        private SerializedProperty _applyModifiers;
        private SerializedProperty _embedTextures;
        private SerializedProperty _bakeAnimation;
        private SerializedProperty _bakeAnimationNlaStrips;
        private SerializedProperty _bakeAnimationActions;
        private SerializedProperty _simplifyBakeAnimation;

        #endregion

        #region Model Tab Properties

        private SerializedProperty _globalScale;
        private SerializedProperty _useFileUnits;
        private SerializedProperty _bakeAxisConversion;
        private SerializedProperty _importBlendShapes;
        private SerializedProperty _importVisibility;
        private SerializedProperty _importCameras;
        private SerializedProperty _importLights;
        private SerializedProperty _preserveHierarchy;
        private SerializedProperty _sortHierarchyByName;

        private SerializedProperty _meshCompression;
        private SerializedProperty _isReadable;
        private SerializedProperty _meshOptimizationFlags;
        private SerializedProperty _addCollider;

        private SerializedProperty _keepQuads;
        private SerializedProperty _weldVertices;
        private SerializedProperty _indexFormat;
        private SerializedProperty _legacyBlendShapeNormals;
        private SerializedProperty _importerNormals;
        private SerializedProperty _importBlendShapeNormals;
        private SerializedProperty _normalCalculationMode;

        private SerializedProperty _normalSmoothingSource;
        private SerializedProperty _normalSmoothingAngle;
        private SerializedProperty _importTangents;
        private SerializedProperty _swapUVChannels;

        private SerializedProperty _generateSecondaryUV;

        // Geometry Lightmaps
        private bool _isLightmapExpanded = false;

        private SerializedProperty _secondaryUVHardAngle;
        private SerializedProperty _secondaryUVAngleDistortion;
        private SerializedProperty _secondaryUVAreaDistortion;
        private SerializedProperty _secondaryUVMarginMethod;
        private SerializedProperty _secondaryUVMinLightmapResolution;
        private SerializedProperty _secondaryUVMinObjectScale;

        #endregion

        public override void OnEnable()
        {
            base.OnEnable();

            #region Blender Tab Properties

            _exportObjects = serializedObject.FindProperty("blendSettings.exportObjects");
            _exportCollections = serializedObject.FindProperty("blendSettings.exportCollections");
            _exportVisible = serializedObject.FindProperty("blendSettings.exportVisible");
            _collectionFilterMode = serializedObject.FindProperty("blendSettings.collectionFilterMode");
            _collectionNames = serializedObject.FindProperty("blendSettings.collectionNames");
            _triangulateMesh = serializedObject.FindProperty("blendSettings.triangulateMesh");
            _applyModifiers = serializedObject.FindProperty("blendSettings.applyModifiers");
            _embedTextures = serializedObject.FindProperty("blendSettings.embedTextures");
            _bakeAnimation = serializedObject.FindProperty("blendSettings.bakeAnimation");
            _bakeAnimationNlaStrips = serializedObject.FindProperty("blendSettings.bakeAnimationNlaStrips");
            _bakeAnimationActions = serializedObject.FindProperty("blendSettings.bakeAnimationActions");
            _simplifyBakeAnimation = serializedObject.FindProperty("blendSettings.simplifyBakeAnimation");

            #endregion

            #region Model Tab Properties

            _globalScale = serializedObject.FindProperty("fbxSettings.globalScale");
            _useFileUnits = serializedObject.FindProperty("fbxSettings.useFileUnits");
            _bakeAxisConversion = serializedObject.FindProperty("fbxSettings.bakeAxisConversion");
            _importBlendShapes = serializedObject.FindProperty("fbxSettings.importBlendShapes");
            _importVisibility = serializedObject.FindProperty("fbxSettings.importVisibility");
            _importCameras = serializedObject.FindProperty("fbxSettings.importCameras");
            _importLights = serializedObject.FindProperty("fbxSettings.importLights");
            _preserveHierarchy = serializedObject.FindProperty("fbxSettings.preserveHierarchy");
            _sortHierarchyByName = serializedObject.FindProperty("fbxSettings.sortHierarchyByName");
            // Meshes
            _meshCompression = serializedObject.FindProperty("fbxSettings.meshCompression");
            _isReadable = serializedObject.FindProperty("fbxSettings.isReadable");
            _meshOptimizationFlags = serializedObject.FindProperty("fbxSettings.meshOptimizationFlags");
            _addCollider = serializedObject.FindProperty("fbxSettings.addCollider");
            // Geometry
            _keepQuads = serializedObject.FindProperty("fbxSettings.keepQuads");
            _weldVertices = serializedObject.FindProperty("fbxSettings.weldVertices");
            _indexFormat = serializedObject.FindProperty("fbxSettings.indexFormat");
            _legacyBlendShapeNormals = serializedObject.FindProperty("fbxSettings.legacyBlendShapeNormals");
            _importerNormals = serializedObject.FindProperty("fbxSettings.importerNormals");
            _importBlendShapeNormals = serializedObject.FindProperty("fbxSettings.importBlendShapeNormals");
            _normalCalculationMode = serializedObject.FindProperty("fbxSettings.normalCalculationMode");
            _normalSmoothingAngle = serializedObject.FindProperty("fbxSettings.normalSmoothingAngle");
            _normalSmoothingSource = serializedObject.FindProperty("fbxSettings.normalSmoothingSource");
            _importTangents = serializedObject.FindProperty("fbxSettings.importTangents");
            _swapUVChannels = serializedObject.FindProperty("fbxSettings.swapUVChannels");
            _generateSecondaryUV = serializedObject.FindProperty("fbxSettings.generateSecondaryUV");
            // Geometry Lightmaps
            _secondaryUVHardAngle = serializedObject.FindProperty("fbxSettings.secondaryUVHardAngle");
            _secondaryUVAngleDistortion = serializedObject.FindProperty("fbxSettings.secondaryUVAngleDistortion");
            _secondaryUVAreaDistortion = serializedObject.FindProperty("fbxSettings.secondaryUVAreaDistortion");
            _secondaryUVMarginMethod = serializedObject.FindProperty("fbxSettings.secondaryUVMarginMethod");
            _secondaryUVMinLightmapResolution = serializedObject.FindProperty("fbxSettings.secondaryUVMinLightmapResolution");
            _secondaryUVMinObjectScale = serializedObject.FindProperty("fbxSettings.secondaryUVMinObjectScale");

            #endregion
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginArea(new Rect(Screen.width / 2 - 120, 5, 300, 30));
            _toolbarInt = GUILayout.Toolbar(_toolbarInt, _toolbarStrings, null, GUI.ToolbarButtonSize.FitToContents,
                _toolBarOptions);
            GUILayout.EndArea();
            GUILayout.Space(30);
            switch (_toolbarInt)
            {
                case 0:
                    DrawBlenderTab();
                    break;
                case 1:
                    DrawModelTab();
                    break;
                case 2:
                    //DrawAnimationTab();
                    break;
                case 3:
                    //DrawMaterialTab();
                    break;
                default:
                    throw new Exception("Invalid Tab Index");
            }

            serializedObject.ApplyModifiedProperties();
            // Call ApplyRevertGUI to show Apply and Revert buttons.
            ApplyRevertGUI();
        }

        private void DrawBlenderTab()
        {
            EditorGUILayout.PropertyField(_exportVisible, new GUIContent("Export Visible"));
            EditorGUILayout.PropertyField(_exportObjects, new GUIContent("Export Objects"));
            EditorGUILayout.PropertyField(_exportCollections, new GUIContent("Export Collections"));
            EditorGUILayout.PropertyField(_collectionFilterMode, new GUIContent("Collection Filter Mode"));
            EditorGUILayout.PropertyField(_collectionNames, new GUIContent("Collection Names"));
            EditorGUILayout.PropertyField(_triangulateMesh, new GUIContent("Triangulate Mesh"));
            EditorGUILayout.PropertyField(_applyModifiers, new GUIContent("Apply Modifiers"));
            EditorGUILayout.PropertyField(_embedTextures, new GUIContent("Embed Textures"));
            EditorGUILayout.PropertyField(_bakeAnimation, new GUIContent("Bake Animation"));
            EditorGUILayout.PropertyField(_bakeAnimationNlaStrips, new GUIContent("Bake Animation NLA Strips"));
            EditorGUILayout.PropertyField(_bakeAnimationActions, new GUIContent("Bake Animation Actions"));
            EditorGUILayout.PropertyField(_simplifyBakeAnimation, new GUIContent("Simplify Bake Animation"));
        }

        private void DrawModelTab()
        {
            // Scene.
            EditorGUILayout.PropertyField(_globalScale, new GUIContent("Scale Factor"));
            EditorGUILayout.PropertyField(_useFileUnits, new GUIContent("Convert Units"));
            EditorGUILayout.PropertyField(_bakeAxisConversion, new GUIContent("Bake Axis Conversion"));
            EditorGUILayout.PropertyField(_importBlendShapes, new GUIContent("Import BlendShapes"));
            EditorGUILayout.PropertyField(_importVisibility, new GUIContent("Import Visibility"));
            EditorGUILayout.PropertyField(_importCameras, new GUIContent("Import Cameras"));
            EditorGUILayout.PropertyField(_importLights, new GUIContent("Import Lights"));
            EditorGUILayout.PropertyField(_preserveHierarchy, new GUIContent("Preserve Hierarchy"));
            EditorGUILayout.PropertyField(_sortHierarchyByName, new GUIContent("Sort Hierarchy By Name"));
            // Meshes
            EditorGUILayout.PropertyField(_meshCompression, new GUIContent("Mesh Compression"));
            EditorGUILayout.PropertyField(_isReadable, new GUIContent("Read/Write Enabled"));
            EditorGUILayout.PropertyField(_meshOptimizationFlags, new GUIContent("Optimize Mesh"));
            EditorGUILayout.PropertyField(_addCollider, new GUIContent("Generate Colliders"));
            // Geometry
            EditorGUILayout.PropertyField(_keepQuads);
            EditorGUILayout.PropertyField(_weldVertices);
            EditorGUILayout.PropertyField(_indexFormat);
            EditorGUILayout.PropertyField(_legacyBlendShapeNormals);
            EditorGUILayout.PropertyField(_importerNormals, new GUIContent("Normals"));
            EditorGUILayout.PropertyField(_importBlendShapeNormals, new GUIContent("Blend Shape Normals"));
            EditorGUILayout.PropertyField(_normalCalculationMode, new GUIContent("Normals Mode"));
            EditorGUILayout.PropertyField(_normalSmoothingSource, new GUIContent("Smoothing Source"));
            EditorGUILayout.PropertyField(_normalSmoothingAngle, new GUIContent("Smoothing Angle"));
            EditorGUILayout.PropertyField(_importTangents, new GUIContent("Tangents"));
            EditorGUILayout.PropertyField(_swapUVChannels, new GUIContent("Swap UVs"));
            EditorGUILayout.PropertyField(_generateSecondaryUV, new GUIContent("Generate Lightmap UVs"));
            // Geometry - Lightmaps
            // create a foldout for the lightmap settings
            _isLightmapExpanded = EditorGUILayout.Foldout(_isLightmapExpanded, "Lightmap Settings");
            if (_isLightmapExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_secondaryUVHardAngle, new GUIContent("Hard Angle"));
                EditorGUILayout.PropertyField(_secondaryUVAngleDistortion, new GUIContent("Angle Error"));
                EditorGUILayout.PropertyField(_secondaryUVAreaDistortion, new GUIContent("Area Error"));
                EditorGUILayout.PropertyField(_secondaryUVMarginMethod, new GUIContent("Margin Method"));
                EditorGUILayout.PropertyField(_secondaryUVMinLightmapResolution,
                    new GUIContent("Min Lightmap Resolution"));
                EditorGUILayout.PropertyField(_secondaryUVMinObjectScale, new GUIContent("Min Object Scale"));
                EditorGUI.indentLevel--;
            }
        }
    }
}