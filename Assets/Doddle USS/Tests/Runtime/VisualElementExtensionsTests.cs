using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss.tests
{
    public class VisualElementExtensionsTests
    {
        [Test, Order(1)]
        public void DisplayFlex_WhenNone_SetsElementToFlex()
        {
            //Arrange
            VisualElement element = new VisualElement();
            element.style.display = DisplayStyle.None;
            StyleEnum<DisplayStyle> expectedStyle = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

            //Act
            element.DisplayFlex();

            //Assert
            Assert.AreEqual(expectedStyle, element.style.display);
        }

        [Test, Order(2)]
        public void DisplayNone_WhenFlex_SetsElementToNone()
        {
            //Arrange
            VisualElement element = new VisualElement();
            StyleEnum<DisplayStyle> expectedStyle = new StyleEnum<DisplayStyle>(DisplayStyle.None);

            //Act
            element.DisplayNone();

            //Assert
            Assert.AreEqual(expectedStyle, element.style.display);
        }

        [Test, Order(3)]
        public void SetSprite_AsFalse_SetsElementToFlex()
        {
            //Arrange
            VisualElement element = new VisualElement();
            StyleEnum<DisplayStyle> expectedStyle = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Doddle USS/Sprites/Arrows/Arrow_Down_Sprite.png");

            //Act
            element.SetSprite(sprite);

            //Assert
            Assert.AreEqual(expectedStyle, element.style.display);
        }

        [Test, Order(4)]
        public void SetSprite_AsTrue_SetsElementToNone()
        {
            //Arrange
            VisualElement element = new VisualElement();
            StyleEnum<DisplayStyle> expectedStyle = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            Sprite sprite = null;

            //Act
            element.SetSprite(sprite, true);

            //Assert
            Assert.AreEqual(expectedStyle, element.style.display);
        }

        [Test, Order(5)]
        public void SetBackgroundColour_SetsBackgroundColour()
        {
            //Arrange
            VisualElement element = new VisualElement();
            StyleColor expectedColour = new StyleColor(new Color(0, 0, 0, 0.7f));

            //Act
            element.SetBackgroundColour(new Color(0, 0, 0, 0.7f));

            //Assert
            Assert.AreEqual(expectedColour, element.style.backgroundColor);
        }

        [Test, Order(6)]
        public void SetBorderColour_SetsAllBorderColour()
        {
            //Arrange
            VisualElement element = new VisualElement();
            StyleColor expectedColour = new StyleColor(new Color(255, 120, 120, 1));

            //Act
            element.SetBorderColour(new Color(255, 120, 120, 1));

            //Assert
            Assert.AreEqual(expectedColour, element.style.borderBottomColor);
            Assert.AreEqual(expectedColour, element.style.borderLeftColor);
            Assert.AreEqual(expectedColour, element.style.borderTopColor);
            Assert.AreEqual(expectedColour, element.style.borderRightColor);
        }

        [Test, Order(7)]
        public void DisableAllChildren_DisablesAllChildren()
        {
            //Arrange
            bool expectedEnabledState = false;

            VisualElement parent = new VisualElement();
            VisualElement child1 = new VisualElement();
            Button child2 = new Button();

            parent.Add(child1);
            parent.Add(child2);

            //Act
            parent.DisableAllChildren();

            //Assert
            Assert.AreEqual(expectedEnabledState, child1.enabledSelf);
            Assert.AreEqual(expectedEnabledState, child2.enabledSelf);
        }

        [Test, Order(8)]
        public void DisableAllButtons_OnlyDisablesButtons()
        {
            //Arrange
            VisualElement parent = new VisualElement();
            VisualElement child1 = new VisualElement();
            Button child2 = new Button();

            parent.Add(child1);
            parent.Add(child2);

            //Act
            parent.DisableAllButtons();

            //Assert
            Assert.IsTrue(child1.enabledSelf);
            Assert.IsFalse(child2.enabledSelf);
        }

        [Test, Order(9)]
        public void EnableAllButtons_OnlyEnablesButtons()
        {
            //Arrange
            VisualElement parent = new VisualElement();
            VisualElement child1 = new VisualElement();
            Button child2 = new Button();
            child1.SetEnabled(false);

            parent.Add(child1);
            parent.Add(child2);

            //Act
            parent.EnableAllButtons();

            //Assert
            Assert.IsFalse(child1.enabledSelf);
            Assert.IsTrue(child2.enabledSelf);
        }

        [Test, Order(10)]
        public void GetChildren_ReturnsAllChildren()
        {
            //Arrange
            int expectedCount = 2;

            VisualElement parent = new VisualElement();
            VisualElement child1 = new VisualElement();
            Button child2 = new Button();

            parent.Add(child1);
            parent.Add(child2);

            //Act
            List<VisualElement> children = parent.GetChildren();

            //Assert
            Assert.AreEqual(expectedCount, children.ToList().Count);
        }

        [Test, Order(11)]
        public void CreateChild_WithNoClasses_CreatesDefaultChild()
        {
            //Arrange
            int expectedCount = 0;
            VisualElement parent = new VisualElement();

            //Act
            parent.CreateChild(null);
            List<VisualElement> children = parent.GetChildren();

            //Assert
            Assert.AreEqual(expectedCount, children.First().GetClasses().Count());
        }

        [Test, Order(12)]
        public void CreateChild_WithClasses_CreatesChildWithClasses()
        {
            //Arrange
            VisualElement parent = new VisualElement();

            //Act
            parent.CreateChild("grow", "flex-row");
            List<VisualElement> children = parent.GetChildren();

            //Assert
            Assert.IsTrue(children.First().ClassListContains("grow"));
            Assert.IsTrue(children.First().ClassListContains("flex-row"));
        }

        [Test, Order(13)]
        public void CreateChild_WithClasses_AppliesCorrectNumberOfClasses()
        {
            //Arrange
            int expectedCount = 2;
            VisualElement parent = new VisualElement();

            //Act
            parent.CreateChild("grow", "flex-row");
            List<VisualElement> children = parent.GetChildren();

            //Assert
            Assert.AreEqual(expectedCount, children.First().GetClasses().Count());
        }

        [Test, Order(16)]
        public void AddClasses_WithTwoClasses_AddsAllClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            element.AddClasses("grow", "flex-row");

            //Assert
            Assert.IsTrue(element.ClassListContains("grow"));
            Assert.IsTrue(element.ClassListContains("flex-row"));
        }

        [Test, Order(17)]
        public void AddClasses_WithTwoClasses_AddsCorrectNumberOfClasses()
        {
            //Arrange
            int expectedCount = 2;
            VisualElement element = new VisualElement();

            //Act
            element.AddClasses("grow", "flex-row");

            //Assert
            Assert.AreEqual(expectedCount, element.GetClasses().Count());
        }

        [Test, Order(18)]
        public void RemoveClasses_WithAllClasses_RemovesAllClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();
            element.AddClasses("grow", "flex-row");

            //Act
            element.RemoveClasses("grow", "flex-row");

            //Assert
            Assert.IsFalse(element.ClassListContains("grow"));
            Assert.IsFalse(element.ClassListContains("flex-row"));
        }

        [Test, Order(19)]
        public void RemoveClasses_WithAllClasses_RemovesCorrectNumberOfClasses()
        {
            //Arrange
            int expectedCount = 0;
            VisualElement element = new VisualElement();
            element.AddClasses("grow", "flex-row");

            //Act
            element.RemoveClasses("grow", "flex-row");

            //Assert
            Assert.AreEqual(expectedCount, element.GetClasses().Count());
        }

        [Test, Order(20)]
        public void RemoveClasses_WithOneClass_RemovesOnlyThatClass()
        {
            //Arrange
            VisualElement element = new VisualElement();
            element.AddClasses("grow", "flex-row");

            //Act
            element.RemoveClasses("flex-row");

            //Assert
            Assert.IsTrue(element.ClassListContains("grow"));
            Assert.IsFalse(element.ClassListContains("flex-row"));
        }

        [Test, Order(21)]
        public void QueryFor_ReturnsAllElementsWithMatchingTooltips()
        {
            //Arrange
            int expectedCount = 2;
            string expectedTooltip = "Test Tooltip";

            VisualElement parent = new VisualElement();
            VisualElement child1 = new VisualElement();
            VisualElement child2 = new VisualElement();
            VisualElement child3 = new VisualElement();

            parent.Add(child1);
            parent.Add(child2);
            parent.Add(child3);

            child1.tooltip = "Test Tooltip";
            child2.tooltip = "Test Tooltip";
            child3.tooltip = "New Tooltip";

            //Act
            List<VisualElement> machingElements = parent.QueryFor("Test Tooltip");

            //Assert
            Assert.AreEqual(expectedCount, machingElements.Count);
            machingElements.ForEach(e => Assert.AreEqual(expectedTooltip, e.tooltip));
        }

        [Test, Order(22)]
        public void QueryContians_ReturnsAllElementsWithTooltipsContainingString()
        {
            //Arrange
            int expectedCount = 3;
            string expectedTooltip = "Tooltip";

            VisualElement parent = new VisualElement();
            VisualElement child1 = new VisualElement();
            VisualElement child2 = new VisualElement();
            VisualElement child3 = new VisualElement();

            parent.Add(child1);
            parent.Add(child2);
            parent.Add(child3);

            child1.tooltip = "Test Tooltip";
            child2.tooltip = "Test Tooltip";
            child3.tooltip = "New Tooltip";

            //Act
            List<VisualElement> machingElements = parent.QueryContains("Tooltip");

            //Assert
            Assert.AreEqual(expectedCount, machingElements.Count);
            machingElements.ForEach(e => Assert.IsTrue(e.tooltip.Contains(expectedTooltip)));
        }
    }
}