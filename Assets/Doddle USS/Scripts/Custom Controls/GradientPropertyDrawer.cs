using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss
{
    [CustomPropertyDrawer(typeof(GradientAttribute))]
    public class GradientPropertyDrawer : PropertyDrawer
    {
        private VisualElement propertyVisualElement;
        private Label propertyLabel;
        private ColorField propertyColor;
        private Button propertyCopy;
        private Button propertyPaste;

        private const float CHANNEL_MIN = 0.0f;
        private const float CHANNEL_MAX = 1.0f;
        private const string TOKEN = ",";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            //Create UI elements for custom drawer
            propertyVisualElement = new VisualElement { style = { flexDirection = FlexDirection.Row, marginTop = 4} };
            propertyLabel = new Label { text = property.name, style = { minWidth = 130} };
            propertyColor = new ColorField { style = { flexGrow = 1 } };
            propertyCopy = new Button { text = "C" };
            propertyPaste = new Button { text = "P" };

            //Add UI elements to base container
            propertyVisualElement.Add(propertyLabel);
            propertyVisualElement.Add(propertyColor);
            propertyVisualElement.Add(propertyCopy);
            propertyVisualElement.Add(propertyPaste);

            //Get the parent property
            string parentPropertyPath = property.propertyPath.Substring(0, property.propertyPath.LastIndexOf('.'));
            SerializedProperty parent = property.serializedObject.FindProperty(parentPropertyPath);

            //Bind the property for change
            var colorProp = parent.FindPropertyRelative(property.name);
            propertyColor.BindProperty(colorProp);

            //Store current property RGBA colour value in system clipboard separated by ","
            propertyCopy.clicked += () =>
            {
                EditorGUIUtility.systemCopyBuffer = $"{propertyColor.value.r}{TOKEN}{propertyColor.value.g}{TOKEN}{propertyColor.value.b}{TOKEN}{propertyColor.value.a}";
            };

            //Paste system clipboard contents into RGBA colour value
            propertyPaste.clicked += () =>
            {
                if (string.IsNullOrWhiteSpace(EditorGUIUtility.systemCopyBuffer)) { return; }
                if (!EditorGUIUtility.systemCopyBuffer.Contains(TOKEN)) { return; }

                string[] splitCopyBuffer = EditorGUIUtility.systemCopyBuffer.Split(TOKEN);
                if (!float.TryParse(splitCopyBuffer[0], out float r)) { return; }
                if (!float.TryParse(splitCopyBuffer[1], out float g)) { return; }
                if (!float.TryParse(splitCopyBuffer[2], out float b)) { return; }
                if (!float.TryParse(splitCopyBuffer[3], out float a)) { return; }

                propertyColor.value = new Color(Mathf.Clamp(r, CHANNEL_MIN, CHANNEL_MAX), Mathf.Clamp(g, CHANNEL_MIN, CHANNEL_MAX), Mathf.Clamp(b, CHANNEL_MIN, CHANNEL_MAX), Mathf.Clamp(a, CHANNEL_MIN, CHANNEL_MAX));
                propertyVisualElement.MarkDirtyRepaint();
            };

            return propertyVisualElement;
        }
    }
}