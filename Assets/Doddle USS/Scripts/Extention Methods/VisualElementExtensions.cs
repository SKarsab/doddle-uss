using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss
{
    public static class VisualElementExtensions
    {
        /// <summary>
        /// Changes DisplayStyle of incoming VisualElement to Flex
        /// </summary>
        /// <param name="element"></param>
        public static VisualElement DisplayFlex(this VisualElement element)
        {
            element.style.display = DisplayStyle.Flex;
            return element;
        }

        /// <summary>
        /// Changes DisplayStyle of incoming VisualElement to None
        /// </summary>
        /// <param name="element"></param>
        public static VisualElement DisplayNone(this VisualElement element)
        {
            element.style.display = DisplayStyle.None;
            return element;
        }

        /// <summary>
        /// Sets the style.backgroundImage property of the incoming VisualElement. If sprite is null,
        /// the VisualElement be set DisplayStyle.None depending on setNoneIfNull
        /// </summary>
        /// <param name="element"></param>
        /// <param name="sprite"></param>
        /// <param name="setNoneIfNull"></param>
        public static VisualElement SetSprite(this VisualElement element, Sprite sprite, bool setNoneIfNull = false)
        {
            if (sprite == null && setNoneIfNull)
            {
                element.DisplayNone();
            }
            else
            {
                element.style.backgroundImage = new StyleBackground(sprite);
                element.DisplayFlex();
            }

            return element;
        }

        /// <summary>
        /// Sets the style.backgroundColor property of the incoming VisualElement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="backgroundColour"></param>
        public static VisualElement SetBackgroundColour(this VisualElement element, StyleColor backgroundColour)
        {
            element.style.backgroundColor = backgroundColour;
            return element;
        }

        /// <summary>
        /// Sets the style.borderColor property of the incoming VisualElement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="borderColour"></param>
        public static VisualElement SetBorderColour(this VisualElement element, StyleColor borderColour)
        {
            element.style.borderBottomColor = borderColour;
            element.style.borderLeftColor = borderColour;
            element.style.borderTopColor = borderColour;
            element.style.borderRightColor = borderColour;
            return element;
        }

        /// <summary>
        /// Disables all children of the incoming VisualElement
        /// </summary>
        /// <param name="element"></param>
        public static VisualElement DisableAllChildren(this VisualElement element)
        {
            element.Children().ToList().ForEach(child => child.SetEnabled(false));
            return element;
        }

        /// <summary>
        /// Disables all children button of the incoming VisualElement
        /// </summary>
        /// <param name="element"></param>
        public static VisualElement DisableAllButtons(this VisualElement element)
        {
            element.Query<Button>().ToList().ForEach(button => { button.SetEnabled(false); });
            return element;
        }

        /// <summary>
        /// Enables all children button of the incoming VisualElement
        /// </summary>
        /// <param name="element"></param>
        public static VisualElement EnableAllButtons(this VisualElement element)
        {
            element.Query<Button>().ToList().ForEach(button => { button.SetEnabled(true); });
            return element;
        }

        /// <summary>
        /// Gets a list of all children VisualElement
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static List<VisualElement> GetChildren(this VisualElement element)
        {
            UQueryBuilder<VisualElement> elements = element.Query<VisualElement>();
            List<VisualElement> children = elements.ToList();
            children.Remove(elements.First());
            return children;
        }

        /// <summary>
        /// Creates a child VisualElement and applies all USS classes
        /// </summary>
        /// <param name="element"></param>
        /// <param name="classes"></param>
        public static VisualElement CreateChild(this VisualElement element, params string[] classes)
        {
            VisualElement child = new VisualElement();
            child.AddClasses(classes);
            element.Add(child);
            return child;
        }

        /// <summary>
        /// Instantiates the template num times as a child of the incoming VisualElement and applies all USS classes to each template
        /// </summary>
        /// <param name="element"></param>
        /// <param name="template"></param>
        /// <param name="num"></param>
        /// <param name="classes"></param>
        public static VisualElement CreateChildTemplate(this VisualElement element, VisualTreeAsset template, int num, params string[] classes)
        {
            VisualElement newTemp = template.CloneTree();

            for (int i = 0; i < num; i++)
            {
                newTemp.AddClasses(classes);
                element.Add(newTemp);
            }

            return newTemp;
        }

        /// <summary>
        /// Adds USS classes to the incoming VisualElement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="classes"></param>
        public static VisualElement AddClasses(this VisualElement element, params string[] classes)
        {
            if (classes != null) 
            {
                classes.ToList().ForEach(ussClass =>
                {
                    if (!string.IsNullOrWhiteSpace(ussClass))
                    {
                        element.AddToClassList(ussClass);
                    }
                });
            }

            return element;
        }

        /// <summary>
        /// Removes USS classes from class list
        /// </summary>
        /// <param name="element"></param>
        /// <param name="classes"></param>
        public static VisualElement RemoveClasses(this VisualElement element, params string[] classes)
        {
            if (classes != null) 
            {
                classes.ToList().ForEach(ussClass =>
                {
                    if (!string.IsNullOrWhiteSpace(ussClass.TrimEnd()))
                    {
                        element.RemoveFromClassList(ussClass);
                    }
                });
            }

            return element;
        }

        /// <summary>
        /// Queries children element's tooltip property for matching string tooltip
        /// </summary>
        /// <remarks>
        /// Tooltips are a only supported in editor. This can be a handy trick for storing runtime 
        /// information when pushing/popping similar elements in lists
        /// </remarks>
        /// <param name="element">List of elements that have the tooltip</param>
        public static List<VisualElement> QueryFor(this VisualElement element, string tooltip)
        {
            UQueryBuilder<VisualElement> elements = element.Query<VisualElement>().Where(e => e.tooltip == tooltip);
            return elements.ToList();
        }

        /// <summary>
        /// Queries children element's tooltip property if string tooltip is contained in the children tooltip
        /// </summary>
        /// <remarks>
        /// Tooltips are a only supported in editor. This can be a handy trick for storing runtime 
        /// information when pushing/popping similar elements in lists
        /// </remarks>
        /// <param name="element">List of elements that have the tooltip</param>
        public static List<VisualElement> QueryContains(this VisualElement element, string tooltip)
        {
            UQueryBuilder<VisualElement> elements = element.Query<VisualElement>().Where(e => e.tooltip.Contains(tooltip));
            return elements.ToList();
        }
    }
}