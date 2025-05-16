using System.IO;
using UnityEditor;

namespace doddle.uss.editor
{
    public static class FileWriter
    {
        /// <summary>
        /// Writes incoming contents to incoming path. Contents will always be OVERWRITTEN in order to 
        /// maintain USS/TSS/JSON file GUIDs, and references between each other/UXML templates.
        /// </summary>
        /// <remarks>
        /// Files are referenced in editor window script, so files will always exist as long as referenced
        /// </remarks>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void WriteToFile(string path, string contents)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(contents);
            }

            AssetDatabase.Refresh();
        }
    }
}