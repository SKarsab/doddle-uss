using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss.tests
{
    public class TextElementExtensionsTests
    {
        [Test, Order(1)]
        public void SetText_AsFalse_SetsTextProperty()
        {
            //Arrange
            string expectedText = "Test Text";
            TextElement element = new TextElement();

            //Act
            element.SetText("Test Text");

            //Assert
            Assert.AreEqual(expectedText, element.text);
        }

        [Test, Order(2)]
        public void SetText_AsFalse_SetsElementToFlex()
        {
            //Arrange
            TextElement element = new TextElement();
            StyleEnum<DisplayStyle> expectedStyle = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

            //Act
            element.SetText("Test Text");

            //Assert
            Assert.AreEqual(expectedStyle, element.style.display);
        }

        [Test, Order(3)]
        public void SetText_AsTrue_SetsElementToNone()
        {
            //Arrange
            TextElement element = new TextElement();
            StyleEnum<DisplayStyle> expectedStyle = new StyleEnum<DisplayStyle>(DisplayStyle.None);

            //Act
            element.SetText("", true);

            //Assert
            Assert.AreEqual(expectedStyle, element.style.display);
        }

        [Test, Order(4)]
        public void ClearText_ClearsAllText()
        {
            //Arrange
            TextElement element = new TextElement();
            element.SetText("Test Tooltip");

            //Act
            element.ClearText();

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(element.text));
        }

        [Test, Order(5)]
        public void SetTextColour_SetsTextColour()
        {
            //Arrange
            TextElement element = new TextElement();
            StyleColor expectedColour = new StyleColor(new Color(255, 120, 120, 1));

            //Act
            element.SetTextColour(new Color(255, 120, 120, 1));

            //Assert
            Assert.AreEqual(expectedColour, element.style.color);
        }

        [Test, Order(6)]
        public void SetFontWeightByClass_AddsDesiredClassButRemovesOthers()
        {
            //Arrange
            TextElement element = new TextElement();
            element.AddToClassList(DoddleUSSClasses.FontRegularClass);

            //Act
            element.SetFontWeightByClass(DoddleUSSClasses.FontBoldClass);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FontBoldClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FontRegularClass));
        }

        [Test, Order(7)]
        public void SetFontWeightByEnum_AddsDesiredClassButRemovesOthers()
        {
            //Arrange
            TextElement element = new TextElement();
            element.AddToClassList(DoddleUSSClasses.FontRegularClass);

            //Act
            element.SetFontWeightByEnum(FontWeight.Bold);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FontBoldClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FontRegularClass));
        }
    }
}