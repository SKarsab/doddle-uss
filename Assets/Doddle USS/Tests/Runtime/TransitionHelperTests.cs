using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace doddle.uss.tests
{
    public class TransitionHelperTests
    {
        [UnityTest, Order(1)]
        public IEnumerator ApplyEnterTransition_Fade_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.Fade);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeAnimClass));
        }

        [UnityTest, Order(2)]
        public IEnumerator ApplyExitTransition_Fade_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.Fade);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeAnimClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeRestClass));
        }

        [UnityTest, Order(3)]
        public IEnumerator ApplyEnterTransition_FadeTranslateRight_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateRight);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateRightClass));
        }

        [UnityTest, Order(4)]
        public IEnumerator ApplyExitTransition_FadeTranslateRight_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateRight);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateRightClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateRestClass));
        }

        [UnityTest, Order(5)]
        public IEnumerator ApplyEnterTransition_FadeTranslateLeft_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateLeft);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateLeftClass));
        }

        [UnityTest, Order(6)]
        public IEnumerator ApplyExitTransition_FadeTranslateLeft_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateLeft);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateLeftClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateRestClass));
        }

        [UnityTest, Order(7)]
        public IEnumerator ApplyEnterTransition_FadeTranslateDown_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateDown);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateDownClass));
        }

        [UnityTest, Order(8)]
        public IEnumerator ApplyExitTransition_FadeTranslateDown_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateDown);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateDownClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateRestClass));
        }

        [UnityTest, Order(9)]
        public IEnumerator ApplyEnterTransition_FadeTranslateUp_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateUp);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateUpClass));
        }

        [UnityTest, Order(10)]
        public IEnumerator ApplyExitTransition_FadeTranslateUp_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateUp);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateUpClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateRestClass));
        }

        [UnityTest, Order(11)]
        public IEnumerator ApplyEnterTransition_FadeTranslateBigRight_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateBigRight);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRightClass));
        }

        [UnityTest, Order(12)]
        public IEnumerator ApplyExitTransition_FadeTranslateBigRight_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateBigRight);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRightClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRestClass));
        }

        [UnityTest, Order(13)]
        public IEnumerator ApplyEnterTransition_FadeTranslateBigLeft_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateBigLeft);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigLeftClass));
        }

        [UnityTest, Order(14)]
        public IEnumerator ApplyExitTransition_FadeTranslateBigLeft_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateBigLeft);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigLeftClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRestClass));
        }

        [UnityTest, Order(15)]
        public IEnumerator ApplyEnterTransition_FadeTranslateBigDown_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateBigDown);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigDownClass));
        }

        [UnityTest, Order(16)]
        public IEnumerator ApplyExitTransition_FadeTranslateBigDown_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateBigDown);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigDownClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRestClass));
        }

        [UnityTest, Order(17)]
        public IEnumerator ApplyEnterTransition_FadeTranslateBigUp_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateBigUp);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigUpClass));
        }

        [UnityTest, Order(18)]
        public IEnumerator ApplyExitTransition_FadeTranslateBigUp_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateBigUp);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigUpClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateBigRestClass));
        }

        [UnityTest, Order(19)]
        public IEnumerator ApplyEnterTransition_FadeTranslateScaleRight_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateScaleRight);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRightClass));
        }

        [UnityTest, Order(20)]
        public IEnumerator ApplyExitTransition_FadeTranslateScaleRight_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateScaleRight);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRightClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRestClass));
        }

        [UnityTest, Order(21)]
        public IEnumerator ApplyEnterTransition_FadeTranslateScaleLeft_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateScaleLeft);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleLeftClass));
        }

        [UnityTest, Order(22)]
        public IEnumerator ApplyExitTransition_FadeTranslateScaleLeft_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateScaleLeft);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleLeftClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRestClass));
        }

        [UnityTest, Order(23)]
        public IEnumerator ApplyEnterTransition_FadeTranslateScaleDown_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateScaleDown);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleDownClass));
        }

        [UnityTest, Order(24)]
        public IEnumerator ApplyExitTransition_FadeTranslateScaleDown_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateScaleDown);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleDownClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRestClass));
        }

        [UnityTest, Order(25)]
        public IEnumerator ApplyEnterTransition_FadeTranslateScaleUp_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.FadeTranslateScaleUp);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleUpClass));
        }

        [UnityTest, Order(26)]
        public IEnumerator ApplyExitTransition_FadeTranslateScaleUp_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.FadeTranslateScaleUp);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleUpClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.FadeTranslateScaleRestClass));
        }

        [UnityTest, Order(27)]
        public IEnumerator ApplyEnterTransition_BounceRight_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.BounceRight);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.BounceRightClass));
        }

        [UnityTest, Order(28)]
        public IEnumerator ApplyExitTransition_BounceRight_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.BounceRight);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceRightClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.BounceRestClass));
        }

        [UnityTest, Order(29)]
        public IEnumerator ApplyEnterTransition_BounceLeft_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.BounceLeft);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.BounceLeftClass));
        }

        [UnityTest, Order(30)]
        public IEnumerator ApplyExitTransition_BounceLeft_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.BounceLeft);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceLeftClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.BounceRestClass));
        }

        [UnityTest, Order(31)]
        public IEnumerator ApplyEnterTransition_BounceDown_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.BounceDown);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.BounceDownClass));
        }

        [UnityTest, Order(32)]
        public IEnumerator ApplyExitTransition_BounceDown_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.BounceDown);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceDownClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.BounceRestClass));
        }

        [UnityTest, Order(33)]
        public IEnumerator ApplyEnterTransition_BounceUp_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyEnterTransition(element, TransitionType.BounceUp);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceRestClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.BounceUpClass));
        }

        [UnityTest, Order(34)]
        public IEnumerator ApplyExitTransition_BounceUp_AppliesClasses()
        {
            //Arrange
            VisualElement element = new VisualElement();

            //Act
            yield return TransitionHelper.ApplyExitTransition(element, TransitionType.BounceUp);

            //Assert
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceReqClass));
            Assert.IsTrue(element.ClassListContains(DoddleUSSClasses.BounceUpClass));
            Assert.IsFalse(element.ClassListContains(DoddleUSSClasses.BounceRestClass));
        }
    }
}