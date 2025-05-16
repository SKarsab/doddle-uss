using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss.tests
{
    public static class TestHelper
    {
        private static readonly string panelSettingsAssetPath = "Assets/Doddle USS/Settings/Doddle Panel Settings.asset";
        private static readonly string primaryButtonAssetPath = "Assets/Doddle USS/Templates/Button Primary/Button-Primary-Large.uxml";
        private static readonly string secondaryButtonAssetPath = "Assets/Doddle USS/Templates/Button Secondary/Button-Secondary-Large.uxml";
        private static readonly string destructiveButtonAssetPath = "Assets/Doddle USS/Templates/Button Destructive/Button-Destructive-Large.uxml";

        /// <summary>
        /// Mocks up a base UI game objects in the scene. Adds a UI document, references source 
        /// asset, and panel settings
        /// </summary>
        /// <param name="uxml"></param>
        /// <returns></returns>
        public static GameObject CreateBaseUIObject(VisualTreeAsset uxml)
        {
            //Find panel setting in project files and set up base UI game object
            GameObject obj = new GameObject();
            UIDocument document = obj.AddComponent<UIDocument>();
            PanelSettings panelSettings = AssetDatabase.LoadAssetAtPath<PanelSettings>(panelSettingsAssetPath);
            
            //Reference panel settings and source asset as SerializedFields
            SerializedObject so = new SerializedObject(document);
            so.FindProperty("m_PanelSettings").objectReferenceValue = panelSettings;
            so.FindProperty("sourceAsset").objectReferenceValue = uxml;
            so.ApplyModifiedProperties();

            return obj;
        }

        /// <summary>
        /// References primary, secondary, and destructive button UXML templates in a private serialized list
        /// on the UI monobehaviour class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        public static void ReferenceButtonTemplates(MonoBehaviour uiClass)
        {
            VisualTreeAsset primaryButtonUxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(primaryButtonAssetPath);
            VisualTreeAsset secondaryButtonUxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(secondaryButtonAssetPath);
            VisualTreeAsset destructiveButtonUxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(destructiveButtonAssetPath);

            //References serialized button templates for content to populate
            SerializedObject so2 = new SerializedObject(uiClass);
            SerializedProperty templates = so2.FindProperty("templates");
            templates.InsertArrayElementAtIndex(0);
            templates.GetArrayElementAtIndex(0).objectReferenceValue = primaryButtonUxml;
            templates.InsertArrayElementAtIndex(1);
            templates.GetArrayElementAtIndex(1).objectReferenceValue = secondaryButtonUxml;
            templates.InsertArrayElementAtIndex(2);
            templates.GetArrayElementAtIndex(2).objectReferenceValue = destructiveButtonUxml;
            so2.ApplyModifiedProperties();
        }

        /// <summary>
        /// Method to print a message to console when called. Used to test UnityEvents in 
        /// methods in UI classes
        /// </summary>
        public static void NotifyWhenInvoked()
        {
            Debug.Log("Event invoked!");
        }
    }
}