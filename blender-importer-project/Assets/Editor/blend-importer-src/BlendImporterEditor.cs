using System;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BlenderImporter
{
    [CustomEditor(typeof(BlendImporter))]
    public class BlendImporterEditor : ScriptedImporterEditor
    {
        // Tool Bar
        private int _toolbarInt = 1;
        private readonly string[] _toolbarStrings = {"Blender", "Model", "Animation", "Materials"};
        private readonly GUILayoutOption[] _toolBarOptions = { GUILayout.MaxWidth(350), GUILayout.Height(25) };

        #region Model Tab Properties
        // Scene
        private SerializedProperty _globalScale;
        private SerializedProperty _useFileUnits;
        private SerializedProperty _bakeAxisConversion;
        private SerializedProperty _importBlendShapes;
        private SerializedProperty _importVisibility;
        private SerializedProperty _importCameras;
        private SerializedProperty _importLights;
        private SerializedProperty _preserveHierarchy;
        private SerializedProperty _sortHierarchyByName;
        // Meshes
        private SerializedProperty _meshCompression;
        private SerializedProperty _isReadable;
        private SerializedProperty _meshOptimizationFlags;
        private SerializedProperty _addColliders;
        // Geometry
        private SerializedProperty _keepQuads;
        private SerializedProperty _weldVertices;
        private SerializedProperty _indexFormat;
        private SerializedProperty _legacyBlendShapeNormals;
        private SerializedProperty _importerNormals;
        private SerializedProperty _importBlendShapeNormals;
        private SerializedProperty _normalsCalculationMode;
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
        
        #region Animation Tab Properties
        private SerializedProperty _importConstraints;
        private SerializedProperty _importAnimation;
        private SerializedProperty _resampleCurves;
        private SerializedProperty _animationCompression;
        private SerializedProperty _animationRotationError;
        private SerializedProperty _animationPositionError;
        private SerializedProperty _animationScaleError;
        private SerializedProperty _importAnimatedCustomProperties;

        #endregion

        
        public override void OnEnable()
        {
            base.OnEnable();
            
            #region Blender Tab Properties
            
            #endregion
            
            #region Model Tab Properties
            // Scene
            _globalScale = serializedObject.FindProperty("ms.globalScale");
            _useFileUnits = serializedObject.FindProperty("ms.useFileUnits");
            _bakeAxisConversion = serializedObject.FindProperty("ms.bakeAxisConversion");
            _importBlendShapes = serializedObject.FindProperty("ms.importBlendShapes");
            _importVisibility = serializedObject.FindProperty("ms.importVisibility");
            _importCameras = serializedObject.FindProperty("ms.importCameras");
            _importLights = serializedObject.FindProperty("ms.importLights");
            _preserveHierarchy = serializedObject.FindProperty("ms.preserveHierarchy");
            _sortHierarchyByName = serializedObject.FindProperty("ms.sortHierarchyByName");
            // Meshes
            _meshCompression = serializedObject.FindProperty("ms.meshCompression");
            _isReadable = serializedObject.FindProperty("ms.isReadable");
            _meshOptimizationFlags = serializedObject.FindProperty("ms.meshOptimizationFlags");
            _addColliders = serializedObject.FindProperty("ms.addCollider");
            // Geometry
            _keepQuads = serializedObject.FindProperty("ms.keepQuads");
            _weldVertices = serializedObject.FindProperty("ms.weldVertices");
            _indexFormat = serializedObject.FindProperty("ms.indexFormat");
            _legacyBlendShapeNormals = serializedObject.FindProperty("ms.legacyBlendShapeNormals");
            _importerNormals = serializedObject.FindProperty("ms.importerNormals");
            _importBlendShapeNormals = serializedObject.FindProperty("ms.importBlendShapeNormals");
            _normalsCalculationMode = serializedObject.FindProperty("ms.normalCalculationMode");
            _normalSmoothingAngle = serializedObject.FindProperty("ms.normalSmoothingAngle");
            _normalSmoothingSource = serializedObject.FindProperty("ms.normalSmoothingSource");
            _importTangents = serializedObject.FindProperty("ms.importTangents");
            _swapUVChannels = serializedObject.FindProperty("ms.swapUVChannels");
            _generateSecondaryUV = serializedObject.FindProperty("ms.generateSecondaryUV");
            // Geometry Lightmaps
            _secondaryUVHardAngle = serializedObject.FindProperty("ms.secondaryUVHardAngle");
            _secondaryUVAngleDistortion = serializedObject.FindProperty("ms.secondaryUVAngleDistortion");
            _secondaryUVAreaDistortion = serializedObject.FindProperty("ms.secondaryUVAreaDistortion");
            _secondaryUVMarginMethod = serializedObject.FindProperty("ms.secondaryUVMarginMethod");
            _secondaryUVMinLightmapResolution = serializedObject.FindProperty("ms.secondaryUVMinLightmapResolution");
            _secondaryUVMinObjectScale = serializedObject.FindProperty("ms.secondaryUVMinObjectScale");
            #endregion

            #region AnimationTabProperties
            _importConstraints = serializedObject.FindProperty("ms.importConstraints");
            _importAnimation = serializedObject.FindProperty("ms.importAnimation");
            _resampleCurves = serializedObject.FindProperty("ms.resampleCurves");
            _animationCompression = serializedObject.FindProperty("ms.animationCompression");
            _animationRotationError = serializedObject.FindProperty("ms.animationRotationError");
            _animationPositionError = serializedObject.FindProperty("ms.animationPositionError");
            _animationScaleError = serializedObject.FindProperty("ms.animationScaleError");
            _importAnimatedCustomProperties = serializedObject.FindProperty("ms.importAnimatedCustomProperties");
            #endregion
        }
        
        public override void OnInspectorGUI()
        {            
            serializedObject.Update();


            GUILayout.BeginArea(new Rect(Screen.width/2 - 120,5,300,30));
            _toolbarInt = GUILayout.Toolbar(_toolbarInt, _toolbarStrings, null , GUI.ToolbarButtonSize.FitToContents, _toolBarOptions);
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
                    DrawAnimationTab();
                    break;
                case 3:
                    DrawMaterialTab();
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
            throw new NotImplementedException();
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
            EditorGUILayout.PropertyField(_addColliders, new GUIContent("Generate Colliders"));
            // Geometry
            EditorGUILayout.PropertyField(_keepQuads);
            EditorGUILayout.PropertyField(_weldVertices);
            EditorGUILayout.PropertyField(_indexFormat);
            EditorGUILayout.PropertyField(_legacyBlendShapeNormals);
            EditorGUILayout.PropertyField(_importerNormals, new GUIContent("Normals"));
            EditorGUILayout.PropertyField(_importBlendShapeNormals, new GUIContent("Blend Shape Normals"));
            EditorGUILayout.PropertyField(_normalsCalculationMode, new GUIContent("Normals Mode"));
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
                EditorGUILayout.PropertyField(_secondaryUVMinLightmapResolution, new GUIContent("Min Lightmap Resolution"));
                EditorGUILayout.PropertyField(_secondaryUVMinObjectScale, new GUIContent("Min Object Scale"));
                EditorGUI.indentLevel--;
            }
        }

        private void DrawAnimationTab()
        {
            EditorGUILayout.PropertyField(_importConstraints, new GUIContent("Import Constraints"));
            EditorGUILayout.PropertyField(_importAnimation, new GUIContent("Import Animation"));
            
            if (_importAnimation.boolValue)
            {
                EditorGUILayout.PropertyField(_resampleCurves, new GUIContent("Resample Curves"));
                EditorGUILayout.PropertyField(_animationCompression, new GUIContent("Compression"));
                EditorGUILayout.PropertyField(_animationRotationError, new GUIContent("Rotation Error"));
                EditorGUILayout.PropertyField(_animationPositionError, new GUIContent("Position Error"));
                EditorGUILayout.PropertyField(_animationScaleError, new GUIContent("Scale Error"));
                EditorGUILayout.PropertyField(_importAnimatedCustomProperties, new GUIContent("Import Animated Custom Properties"));
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void DrawMaterialTab()
        {
            // Button Options
            // TODO: Add half width buttons
            // Extract Textures
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Textures");
            if (GUILayout.Button("Extract Textures..."))
            {
                Debug.LogWarning("EXTRACT TEXTURES TODO");
            }
            EditorGUILayout.EndHorizontal();
            
            // Extract Materials
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Materials");
            if(GUILayout.Button("Extract Materials..."))
            {
                Debug.LogWarning("EXTRACT MATERIALS TODO");
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}