using UnityEngine.UIElements;

namespace doddle.uss
{
    public static class TextElementExtensions
    {
        /// <summary>
        /// Sets the textElement.text property of the incoming TextElement. If text is null,
        /// the TextElement will be set DisplayStyle.None depending on setNoneIfNull
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <param name="setNoneIfNull"></param>
        public static TextElement SetText(this TextElement element, string text, bool setNoneIfNull = false)
        {
            if (string.IsNullOrWhiteSpace(text) && setNoneIfNull)
            {
                element.DisplayNone();
            }
            else
            {
                element.text = text;
                element.DisplayFlex();
            }

            return element;
        }

        /// <summary>
        /// Clears all text of the incoming TextElement
        /// </summary>
        /// <param name="element"></param>
        public static TextElement ClearText(this TextElement element)
        {
            element.text = string.Empty;
            return element;
        }

        /// <summary>
        /// Sets the style.color property of the incoming TextElement.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="colour"></param>
        public static TextElement SetTextColour(this TextElement element, StyleColor colour)
        {
            element.style.color = colour;
            return element;
        }

        /// <summary>
        /// Removes all font weight classes from the incoming TextElement and applies the 
        /// incoming fontWeightClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="fontWeight"></param>
        public static TextElement SetFontWeightByClass(this TextElement element, string desiredFontWeight)
        {
            DoddleUSSClasses.FontWeights.ForEach(fontWeight =>
            {
                if (desiredFontWeight.Equals(fontWeight))
                {
                    element.AddToClassList(fontWeight);
                }
                else
                {
                    element.RemoveFromClassList(fontWeight);
                }
            });

            return element;
        }

        /// <summary>
        /// Removes all font weight classes from the incoming TextElement and applies the 
        /// incoming fontWeightClass
        /// </summary>
        /// <param name="element"></param>
        /// <param name="fontWeight"></param>
        public static TextElement SetFontWeightByEnum(this TextElement element, FontWeight desiredFontWeight)
        {
            DoddleUSSClasses.FontWeights.ForEach(fontWeight => element.RemoveFromClassList(fontWeight));
            element.AddToClassList(DoddleUSSClasses.FontWeights[(int)desiredFontWeight]);
            return element;
        }
    }
}