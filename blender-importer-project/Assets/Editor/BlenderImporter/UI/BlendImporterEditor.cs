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
    }

}