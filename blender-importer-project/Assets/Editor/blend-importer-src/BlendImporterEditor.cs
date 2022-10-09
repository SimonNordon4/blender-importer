using System;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace BlenderImporter
{
    [CustomEditor(typeof(BlendImporter))]
    public class BlendImporterEditor : ScriptedImporterEditor
    {
        // Tool Bar
        private int _toolbarInt = 0;
        private readonly string[] _toolbarStrings = {"Blender", "Model", "Animation", "Materials"};
        private readonly GUILayoutOption[] _toolBarOptions = { GUILayout.MaxWidth(350), GUILayout.Height(25)};

        #region Model Tab Properties
        // Scene
        private SerializedProperty _scaleFactor;
        private SerializedProperty _convertUnits;
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
        private SerializedProperty _optimizeMesh;
        private SerializedProperty _generateColliders;
        // Geometry
        private SerializedProperty _keepQuads;
        private SerializedProperty _weldVertices;
        private SerializedProperty _indexFormat;
        private SerializedProperty _legacyBlendShapeNormals;
        private SerializedProperty _normals;
        private SerializedProperty _blendShapeNormals;
        private SerializedProperty _normalsMode;
        private SerializedProperty _normalSmoothingSource;
        private SerializedProperty _smoothingAngle;
        private SerializedProperty _tangents;
        private SerializedProperty _swapUvs;
        private SerializedProperty _generateLightmapUvs;
        #endregion
        
        #region Animation Tab Properties
        private SerializedProperty _importConstraints;
        private SerializedProperty _importAnimation;
        private SerializedProperty _resampleCurves;
        private SerializedProperty _animationCompression;
        private SerializedProperty _rotationError;
        private SerializedProperty _positionError;
        private SerializedProperty _scaleError;
        private SerializedProperty _animatedCustomProperties;
        private SerializedProperty _clipAnimations;


        #endregion
   
        public override void OnEnable()
        {
            base.OnEnable();
            
            #region Blender Tab Properties
            
            #endregion
            
            #region Model Tab Properties
            // Scene
            _scaleFactor = serializedObject.FindProperty("modelImporterSettings.globalScale");
            _convertUnits = serializedObject.FindProperty("modelImporterSettings.useFileUnits");
            _bakeAxisConversion = serializedObject.FindProperty("modelImporterSettings.bakeAxisConversion");
            _importBlendShapes = serializedObject.FindProperty("modelImporterSettings.importBlendShapes");
            _importVisibility = serializedObject.FindProperty("modelImporterSettings.importVisibility");
            _importCameras = serializedObject.FindProperty("modelImporterSettings.importCameras");
            _importLights = serializedObject.FindProperty("modelImporterSettings.importLights");
            _preserveHierarchy = serializedObject.FindProperty("modelImporterSettings.preserveHierarchy");
            _sortHierarchyByName = serializedObject.FindProperty("modelImporterSettings.sortHierarchyByName");
            // Meshes
            _meshCompression = serializedObject.FindProperty("modelImporterSettings.meshCompression");
            _isReadable = serializedObject.FindProperty("modelImporterSettings.isReadable");
            _optimizeMesh = serializedObject.FindProperty("modelImporterSettings.meshOptimizationFlags");
            _generateColliders = serializedObject.FindProperty("modelImporterSettings.addCollider");
            // Geometry
            _keepQuads = serializedObject.FindProperty("modelImporterSettings.keepQuads");
            _weldVertices = serializedObject.FindProperty("modelImporterSettings.weldVertices");
            _indexFormat = serializedObject.FindProperty("modelImporterSettings.indexFormat");
            _legacyBlendShapeNormals = serializedObject.FindProperty("modelImporterSettings.legacyBlendShapeNormals");
            _normals = serializedObject.FindProperty("modelImporterSettings.importerNormals");
            _blendShapeNormals = serializedObject.FindProperty("modelImporterSettings.importBlendShapeNormals");
            _normalsMode = serializedObject.FindProperty("modelImporterSettings.normalCalculationMode");
            _smoothingAngle = serializedObject.FindProperty("modelImporterSettings.normalSmoothingAngle");
            _normalSmoothingSource = serializedObject.FindProperty("modelImporterSettings.normalSmoothingSource");
            _tangents = serializedObject.FindProperty("modelImporterSettings.importTangents");
            _swapUvs = serializedObject.FindProperty("modelImporterSettings.swapUVChannels");
            _generateLightmapUvs = serializedObject.FindProperty("modelImporterSettings.generateSecondaryUV");
            
            #endregion
        }
        
        public override void OnInspectorGUI()
        {
            _toolbarInt = GUILayout.Toolbar(_toolbarInt, _toolbarStrings, null , GUI.ToolbarButtonSize.FitToContents, _toolBarOptions);

            if (_toolbarInt == 1)
            {
                DrawModelTab();
            }

            // Call ApplyRevertGUI to show Apply and Revert buttons.
            ApplyRevertGUI();
        }
        
        private void DrawModelTab()
        {
            // Update the serializedObject in case it has been changed outside the Inspector.
            serializedObject.Update();

            // Scene.
            EditorGUILayout.PropertyField(_scaleFactor);
            EditorGUILayout.PropertyField(_convertUnits);
            EditorGUILayout.PropertyField(_bakeAxisConversion);
            EditorGUILayout.PropertyField(_importBlendShapes);
            EditorGUILayout.PropertyField(_importVisibility);
            EditorGUILayout.PropertyField(_importCameras);
            EditorGUILayout.PropertyField(_importLights);
            EditorGUILayout.PropertyField(_preserveHierarchy);
            EditorGUILayout.PropertyField(_sortHierarchyByName);
            // Meshes
            EditorGUILayout.PropertyField(_meshCompression);
            EditorGUILayout.PropertyField(_isReadable);
            EditorGUILayout.PropertyField(_optimizeMesh);
            EditorGUILayout.PropertyField(_generateColliders);
            // Geometry
            EditorGUILayout.PropertyField(_keepQuads);
            EditorGUILayout.PropertyField(_weldVertices);
            EditorGUILayout.PropertyField(_indexFormat);
            EditorGUILayout.PropertyField(_legacyBlendShapeNormals);
            EditorGUILayout.PropertyField(_normals);
            EditorGUILayout.PropertyField(_blendShapeNormals);
            EditorGUILayout.PropertyField(_normalsMode);
            EditorGUILayout.PropertyField(_normalSmoothingSource);
            EditorGUILayout.PropertyField(_smoothingAngle);
            EditorGUILayout.PropertyField(_tangents);
            EditorGUILayout.PropertyField(_swapUvs);
            EditorGUILayout.PropertyField(_generateLightmapUvs);
            

            // Apply the changes so Undo/Redo is working
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAnimationTab()
        {
            
        }
    }
}