using System;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace BlenderImporter.UI
{
    [CustomEditor(typeof(BlenderImporter.BlendImporter))]
    public class BlendImporterEditor : ScriptedImporterEditor
    {
        private int _toolbarInt = 1;
        private string[] _toolbarStrings = { "Blender","Model","Materials","Animation" };
        private readonly  GUILayoutOption[] _toolBarOptions = { GUILayout.MaxWidth(350), GUILayout.Height(25) };
        
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
        
        public override void OnEnable()
        {
            base.OnEnable();
            var blendSettings = serializedObject.FindProperty("BlendSettings");
            Debug.Log(blendSettings);
            
            // This works.
            Debug.Log(serializedObject.FindProperty("Test").name);
            // This doesn't.
            Debug.Log(serializedObject.FindProperty("BlendSettings").name);
            
            #region Blender Tab Properties
            // _exportVisible = serializedObject.FindProperty("BlendSettings.ExportVisible");
            // _exportObjects = serializedObject.FindProperty("BlendSettings.ExportObjects");
            // _exportCollections = serializedObject.FindProperty("BlendSettings.ExportCollections");
            // _collectionFilterMode = serializedObject.FindProperty("BlendSettings.CollectionFilterMode");
            // _collectionNames = serializedObject.FindProperty("BlendSettings.CollectionNames");
            // _triangulateMesh = serializedObject.FindProperty("BlendSettings.TriangulateMesh");
            // _applyModifiers = serializedObject.FindProperty("BlendSettings.ApplyModifiers");
            // _embedTextures = serializedObject.FindProperty("BlendSettings.EmbedTextures");
            // _bakeAnimation = serializedObject.FindProperty("BlendSettings.BakeAnimation");
            // _bakeAnimationNlaStrips = serializedObject.FindProperty("BlendSettings.BakeAnimationNlaStrips");
            // _bakeAnimationActions = serializedObject.FindProperty("BlendSettings.BakeAnimationActions");
            // _simplifyBakeAnimation = serializedObject.FindProperty("BlendSettings.SimplifyBakeAnimation");
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
                    //DrawModelTab();
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
            // EditorGUILayout.PropertyField(_exportObjects, new GUIContent("Export Objects"));
            // EditorGUILayout.PropertyField(_exportCollections, new GUIContent("Export Collections"));
            // EditorGUILayout.PropertyField(_collectionFilterMode, new GUIContent("Collection Filter Mode"));
            // EditorGUILayout.PropertyField(_collectionNames, new GUIContent("Collection Names"));
            // EditorGUILayout.PropertyField(_triangulateMesh, new GUIContent("Triangulate Mesh"));
            // EditorGUILayout.PropertyField(_applyModifiers, new GUIContent("Apply Modifiers"));
            // EditorGUILayout.PropertyField(_embedTextures, new GUIContent("Embed Textures"));
            // EditorGUILayout.PropertyField(_bakeAnimation, new GUIContent("Bake Animation"));
            // EditorGUILayout.PropertyField(_bakeAnimationNlaStrips, new GUIContent("Bake Animation NLA Strips"));
            // EditorGUILayout.PropertyField(_bakeAnimationActions, new GUIContent("Bake Animation Actions"));
            // EditorGUILayout.PropertyField(_simplifyBakeAnimation, new GUIContent("Simplify Bake Animation"));
        }
    }

}