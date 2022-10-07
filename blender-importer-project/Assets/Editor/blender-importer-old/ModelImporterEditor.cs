// using UnityEngine;
// using UnityEditor;
// using System;
// using UnityEditor.AssetImporters;

//  namespace BlenderImporter{
// [CustomEditor(typeof(BlendImporter))]
// public class ModelImporterEditor : AssetImporterEditor
// {
//     Editor defaultEditor;

//     public override void OnEnable()
//     {
//         defaultEditor = Editor.CreateEditor(targets, Type.GetType("UnityEditor.ModelImporterEditor, UnityEditor"));
//         defaultEditor.CreateInspectorGUI();
//         base.OnEnable();
//     }

//     public override void OnDisable()
//     {
//         DestroyImmediate(defaultEditor);
//         base.OnDisable();
//     }

//     public override void OnInspectorGUI()
//     {
//         serializedObject.Update();

//         defaultEditor.OnInspectorGUI();

//         ApplyRevertGUI();

//         if (GUILayout.Button("CREATE LODs"))
//         {
//             //Do the LOD creation
//         }

//         serializedObject.ApplyModifiedProperties();
//     }
// }}

