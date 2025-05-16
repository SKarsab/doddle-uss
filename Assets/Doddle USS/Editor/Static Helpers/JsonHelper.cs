using UnityEditor;
using UnityEngine;

namespace doddle.uss.editor
{
    public static class JsonHelper
    {
        /// <summary>
        /// Imports the Utility DoddleConfigEditorData setup from the JSON file doddle-utility-backup.json. Current 
        /// configuration will be LOST (Overwritten)
        /// </summary>
        /// <param name="textAsset"></param>
        /// <param name="configEditorData"></param>
        public static void RestoreDefaults(TextAsset textAsset, DoddleUtilityData configEditorData)
        {
            OverwriteCurrentUtilityConfig(textAsset, configEditorData);
            Debug.Log("DoddleConfigEditor.cs - doddle-utility-backup.json successfully imported. DoddleUtilityData overwritten");
        }

        /// <summary>
        /// Imports the Utility DoddleConfigEditorData setup from the JSON file doddle-utility-import.json. Current 
        /// configuration will be LOST (Overwritten)
        /// </summary>
        /// <param name="textAsset"></param>
        /// <param name="configEditorData"></param>
        public static void ImportConfig(TextAsset textAsset, DoddleUtilityData configEditorData)
        {
            OverwriteCurrentUtilityConfig(textAsset, configEditorData);
            Debug.Log("DoddleConfigEditor.cs - doddle-utility-import.json successfully imported. DoddleUtilityData overwritten");
        }

        /// <summary>
        /// Exports the current Utility DoddleConfigEditorData setup to the JSON file doddle-utility-export.json
        /// </summary>
        /// <param name="textAsset"></param>
        /// <param name="configEditorData"></param>
        public static void ExportConfig(TextAsset textAsset, DoddleUtilityData configEditorData)
        {
            DoddleUtilityDataWrapper wrapper = new DoddleUtilityDataWrapper();
            wrapper.Utilities = configEditorData.Utilities;

            string json = JsonUtility.ToJson(wrapper, true);
            string filePath = FileReader.GetFilePath(textAsset);

            FileWriter.WriteToFile(filePath, json);
            AssetDatabase.Refresh();

            Debug.Log("DoddleConfigEditor.cs - doddle-utility-export.json successfully exported");
        }

        /// <summary>
        /// Replaces the current DoddleConfigEditorData setup from the incoming JSON file. Current 
        /// configuration will be LOST (Overwritten)
        /// </summary>
        /// <param name="textAsset"></param>
        /// <param name="configEditorData"></param>
        public static void OverwriteCurrentUtilityConfig(TextAsset textAsset, DoddleUtilityData configEditorData)
        {
            DoddleUtilityDataWrapper wrapper = new DoddleUtilityDataWrapper();
            wrapper = JsonUtility.FromJson<DoddleUtilityDataWrapper>(textAsset.text);
            configEditorData.Utilities = wrapper.Utilities;
            AssetDatabase.Refresh();
        }
    }
}