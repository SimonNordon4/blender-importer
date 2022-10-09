using System;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace BlenderImporter
{
    [CustomEditor(typeof(BlendImporter))]
    public class BlendImporterEditor : ScriptedImporterEditor
    {
        SerializedProperty m_advancedImportMode;
        SerializedProperty m_modelImpoterSettings;
        
        int toolbarInt = 0;
        string[] toolbarStrings = {"Blender", "Model", "Rig", "Animation", "Materials"};
        GUILayoutOption[] toolBarOptions = { GUILayout.MaxWidth(350), GUILayout.Height(25)};

   
        public override void OnEnable()
        {
            base.OnEnable();
            // Once in OnEnable, retrieve the serializedObject property and store it.
            m_advancedImportMode = serializedObject.FindProperty("advancedImportMode");
            
            // add the model importer gui
            m_modelImpoterSettings = serializedObject.FindProperty("modelImporterSettings");
        }
        
        public override void OnInspectorGUI()
        {
            toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings, null , GUI.ToolbarButtonSize.FitToContents, toolBarOptions);

            if (toolbarInt == 0)
            {
                // Update the serializedObject in case it has been changed outside the Inspector.
                serializedObject.Update();

                // Draw the boolean property.
                EditorGUILayout.PropertyField(m_advancedImportMode);
                EditorGUILayout.PropertyField(m_modelImpoterSettings);

                // Apply the changes so Undo/Redo is working
                serializedObject.ApplyModifiedProperties();
            }

            // Call ApplyRevertGUI to show Apply and Revert buttons.
            ApplyRevertGUI();
        }
    }
    
    [CustomEditor(typeof(ModelImporterSettings))]
    public class ModelImporterSettingsEditor : Editor
    {
        private void OnEnable()
        {

        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}