using System.IO;
using UnityEditor;
using UnityEngine;

namespace doddle.uss.editor
{
    public static class FileReader
    {
        /// <summary>
        /// Reads contents of file located at incoming path.
        /// </summary>
        /// <remarks>
        /// Files are referenced in editor window script, so files will always exist as long as referenced
        /// </remarks>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFromFile(string path)
        {
            string contents;

            using (StreamReader streaReader = new StreamReader(path))
            {
                contents = streaReader.ReadToEnd();
            }

            return contents;
        }

        /// <summary>
        /// Tries to get the object's file path from th incoming obj. If the abject is null, warning will be logged instead.
        /// Used in order to get file paths for JSON/TSS/USS files since they are stored as references instead of paths
        /// </summary>
        /// <param name="obj"></param>
        public static string GetFilePath(Object obj)
        {
            if (!obj)
            {
                Debug.LogWarning("FileReader.cs - Incoming object reference null. Check DoddleUtilityEditor.cs/DoddleThemeEditor.cs references");
                return null;
            }

            return AssetDatabase.GetAssetPath(obj);
        }
    }
}