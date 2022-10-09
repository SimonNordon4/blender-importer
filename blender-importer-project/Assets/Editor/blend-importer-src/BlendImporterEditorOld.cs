using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BlenderImporter
{
  
    public class BlendImporterEditorOld : ScriptedImporterEditor
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
        private SerializedProperty _rotationError;
        private SerializedProperty _positionError;
        private SerializedProperty _scaleError;
        private SerializedProperty _animatedCustomProperties;
        private SerializedProperty _clipAnimations;
        private SerializedProperty _lightMapSettings;

        #endregion

        public override VisualElement CreateInspectorGUI()
        {
            serializedObject.Update();
            var root = new VisualElement();
            root.Add(new IMGUIContainer(() =>
            {
                _toolbarInt = GUILayout.Toolbar(_toolbarInt, _toolbarStrings, _toolBarOptions);
                switch (_toolbarInt)
                {
                    case 0:
                        //DrawBlenderTab();
                        break;
                    case 1:
                        DrawModelTab();
                        break;
                    case 2:
                        DrawAnimationTab();
                        break;
                    case 3:
                        //DrawMaterialsTab();
                        break;
                }
            }));
            return root;
        }
        public override void OnEnable()
        {
            base.OnEnable();
            
            #region Blender Tab Properties
            
            #endregion
            
            #region Model Tab Properties
            // Scene
            _scaleFactor = serializedObject.FindProperty("ms.globalScale");
            _convertUnits = serializedObject.FindProperty("ms.useFileUnits");
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
            _optimizeMesh = serializedObject.FindProperty("ms.meshOptimizationFlags");
            _generateColliders = serializedObject.FindProperty("ms.addCollider");
            // Geometry
            _keepQuads = serializedObject.FindProperty("ms.keepQuads");
            _weldVertices = serializedObject.FindProperty("ms.weldVertices");
            _indexFormat = serializedObject.FindProperty("ms.indexFormat");
            _legacyBlendShapeNormals = serializedObject.FindProperty("ms.legacyBlendShapeNormals");
            _normals = serializedObject.FindProperty("ms.importerNormals");
            _blendShapeNormals = serializedObject.FindProperty("ms.importBlendShapeNormals");
            _normalsMode = serializedObject.FindProperty("ms.normalCalculationMode");
            _smoothingAngle = serializedObject.FindProperty("ms.normalSmoothingAngle");
            _normalSmoothingSource = serializedObject.FindProperty("ms.normalSmoothingSource");
            _tangents = serializedObject.FindProperty("ms.importTangents");
            _swapUvs = serializedObject.FindProperty("ms.swapUVChannels");
            _generateLightmapUvs = serializedObject.FindProperty("ms.generateSecondaryUV");
            // Geometry Lightmaps
            _secondaryUVHardAngle = serializedObject.FindProperty("ms.secondaryUVHardAngle");
            _secondaryUVAngleDistortion = serializedObject.FindProperty("ms.secondaryUVAngleDistortion");
            _secondaryUVAreaDistortion = serializedObject.FindProperty("ms.secondaryUVAreaDistortion");
            _secondaryUVMarginMethod = serializedObject.FindProperty("ms.secondaryUVMarginMethod");
            _secondaryUVMinLightmapResolution = serializedObject.FindProperty("ms.secondaryUVMinLightmapResolution");
            _secondaryUVMinObjectScale = serializedObject.FindProperty("ms.secondaryUVMinObjectScale");
            
            
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
            var root = new VisualElement();
            root.Add(new PropertyField(_scaleFactor));
            // EditorGUILayout.PropertyField(_scaleFactor);
            // EditorGUILayout.PropertyField(_convertUnits);
            // EditorGUILayout.PropertyField(_bakeAxisConversion);
            // EditorGUILayout.PropertyField(_importBlendShapes);
            // EditorGUILayout.PropertyField(_importVisibility);
            // EditorGUILayout.PropertyField(_importCameras);
            // EditorGUILayout.PropertyField(_importLights);
            // EditorGUILayout.PropertyField(_preserveHierarchy);
            // EditorGUILayout.PropertyField(_sortHierarchyByName);
            // // Meshes
            // EditorGUILayout.PropertyField(_meshCompression);
            // EditorGUILayout.PropertyField(_isReadable);
            // EditorGUILayout.PropertyField(_optimizeMesh);
            // EditorGUILayout.PropertyField(_generateColliders);
            // // Geometry
            // EditorGUILayout.PropertyField(_keepQuads);
            // EditorGUILayout.PropertyField(_weldVertices);
            // EditorGUILayout.PropertyField(_indexFormat);
            // EditorGUILayout.PropertyField(_legacyBlendShapeNormals);
            // EditorGUILayout.PropertyField(_normals);
            // EditorGUILayout.PropertyField(_blendShapeNormals);
            // EditorGUILayout.PropertyField(_normalsMode);
            // EditorGUILayout.PropertyField(_normalSmoothingSource);
            // EditorGUILayout.PropertyField(_smoothingAngle);
            // EditorGUILayout.PropertyField(_tangents);
            // EditorGUILayout.PropertyField(_swapUvs);
            // EditorGUILayout.PropertyField(_generateLightmapUvs);
            // Geometry - Lightmaps

            // create a foldout for the lightmap settings

            // _isLightmapExpanded = EditorGUILayout.Foldout(_isLightmapExpanded, "Lightmap Settings");
            //
            // if (_isLightmapExpanded)
            // {
            //     EditorGUI.indentLevel++;
            //     EditorGUILayout.PropertyField(_secondaryUVHardAngle);
            //     EditorGUILayout.PropertyField(_secondaryUVAngleDistortion);
            //     EditorGUILayout.PropertyField(_secondaryUVAreaDistortion);
            //     EditorGUILayout.PropertyField(_secondaryUVMarginMethod);
            //     EditorGUILayout.PropertyField(_secondaryUVMinLightmapResolution);
            //     EditorGUILayout.PropertyField(_secondaryUVMinObjectScale);
            //     EditorGUI.indentLevel--;
            // }
            

            // Apply the changes so Undo/Redo is working
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAnimationTab()
        {
            
        }
    }
}