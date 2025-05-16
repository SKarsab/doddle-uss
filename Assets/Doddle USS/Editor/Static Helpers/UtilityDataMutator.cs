using System.Collections.Generic;
using System.Text;
using UnityEngine.UIElements;

namespace doddle.uss.editor
{
    public static class UtilityDataMutator
    {
        /// <summary>
        /// Creates a new List<Utility> from the altered config in the editor window
        /// </summary>
        /// <param name="scrollView"></param>
        public static List<Utility> GetUtilityDataFromFoldouts(ScrollView scrollView)
        {
            List<Utility> newUtilityData = new List<Utility>();

            foreach (Foldout utilityFoldout in scrollView.Query<Foldout>(className: DoddleUtilityEditor.UtilityClass).ToList())
            {
                //Get references to the text fields and int fields in the foldout
                TextField className = utilityFoldout.Q<TextField>(DoddleUtilityEditor.UtilityName);
                IntegerField instances = utilityFoldout.Q<IntegerField>(DoddleUtilityEditor.UtilityInstance);

                Utility newUtility = new Utility(className.value, instances.value);

                foreach (Foldout propertyFoldout in utilityFoldout.Query<Foldout>(className: DoddleUtilityEditor.PropertyClass).ToList())
                {
                    //Get references to the text fields in the foldout
                    TextField name = propertyFoldout.Q<TextField>(DoddleUtilityEditor.PropertyName);
                    TextField value = propertyFoldout.Q<TextField>(DoddleUtilityEditor.PropertyValue);
                    TextField unit = propertyFoldout.Q<TextField>(DoddleUtilityEditor.PropertyUnit);

                    Property newProperty = new Property(name.value, value.value, unit.value);
                    newUtility.Properties.Add(newProperty);
                }

                newUtilityData.Add(newUtility);
            }

            return newUtilityData;
        }

        /// <summary>
        /// Builds a the contents of the new doddle-utility.uss file. Return a string which can be written to file
        /// </summary>
        /// <param name="utilityData"></param>
        /// <returns></returns>
        public static string BuildUtilityContents(List<Utility> utilityData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Utility utility in utilityData)
            {
                //Only if number classes. Things like flex-row wouldn't enter here
                if (utility.Instances > 1)
                {
                    for (int i = 0; i < utility.Instances; i++)
                    {
                        //Class name
                        string resolvedSuffix = $"{(i * (float.Parse(utility.Properties[0].Value)))}";
                        stringBuilder.AppendLine($".{utility.ClassName}-{resolvedSuffix} {{");

                        //Class properties
                        foreach (Property property in utility.Properties)
                        {
                            string resolvedValue = $"{(i * (float.Parse(property.Value)))}";
                            stringBuilder.AppendLine($"\t{property.Name}: {resolvedValue}{property.Unit};");
                        }

                        //Class closing
                        stringBuilder.AppendLine("}");
                        stringBuilder.AppendLine("");
                    }
                }
                //This is where classes like flex-row would enter
                else
                {
                    //Class name
                    stringBuilder.AppendLine($".{utility.ClassName} {{");

                    //Class properties
                    foreach (Property property in utility.Properties)
                    {
                        stringBuilder.AppendLine($"\t{property.Name}: {property.Value}{property.Unit};");
                    }

                    //Class closing
                    stringBuilder.AppendLine("}");
                    stringBuilder.AppendLine("");
                }
            }

            return stringBuilder.ToString();
        }
    }
}