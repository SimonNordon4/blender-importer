using AssetStoreTools.Uploader;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace BlenderImporter
{
    [CustomEditor(typeof(BlendImporter))]
    public class BlendImporterEditor : ScriptedImporterEditor
    {
        private VisualElement root { get; set; }
        
        private SerializedProperty _scaleFactor;
        
        public override VisualElement CreateInspectorGUI()
        {
            FindProperties();
            InitializeEditor();
            ApplyRevertGUI();
            return root;
        }
        public void onEnable()
        {
            FindProperties();
            InitializeEditor();
        }
        public override void OnInspectorGUI()
        {
            ApplyRevertGUI();
        }

        private void FindProperties()
        {
            _scaleFactor = serializedObject.FindProperty("ms.scaleFactor");
        }

        private void InitializeEditor()
        {
            root = new VisualElement();
            var scaleFactorField = new PropertyField(_scaleFactor, "Scale Factor");
            root.Add(scaleFactorField);
        }
    }
}