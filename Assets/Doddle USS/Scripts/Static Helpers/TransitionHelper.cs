using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace doddle.uss
{
    //Yield return nulls are to wait for resolvedStyle the frame after. Otherwise final transition property could be applied right away
    //with no transition effect
    public static class TransitionHelper
    {
        /// <summary>
        /// This is so MonoBehaviours can call StartCoroutine(TransitionHelper.[ApplyEnterTransition/ApplyExitTransition]) to automatically start or 
        /// end an transition based on the TransitionType. Any element should be able to use this. This applies REQ, ANIM, and REST USS classes
        /// </summary>
        public static readonly Dictionary<TransitionType, TransitionClassSet> ClassMap = new Dictionary<TransitionType, TransitionClassSet>
        {
            { TransitionType.Fade, new TransitionClassSet(DoddleUSSClasses.FadeReqClass, DoddleUSSClasses.FadeRestClass, DoddleUSSClasses.FadeAnimClass) },
            { TransitionType.FadeTranslateRight, new TransitionClassSet(DoddleUSSClasses.FadeTranslateReqClass, DoddleUSSClasses.FadeTranslateRestClass, DoddleUSSClasses.FadeTranslateRightClass) },
            { TransitionType.FadeTranslateLeft, new TransitionClassSet(DoddleUSSClasses.FadeTranslateReqClass, DoddleUSSClasses.FadeTranslateRestClass, DoddleUSSClasses.FadeTranslateLeftClass) },
            { TransitionType.FadeTranslateDown, new TransitionClassSet(DoddleUSSClasses.FadeTranslateReqClass, DoddleUSSClasses.FadeTranslateRestClass, DoddleUSSClasses.FadeTranslateDownClass) },
            { TransitionType.FadeTranslateUp, new TransitionClassSet(DoddleUSSClasses.FadeTranslateReqClass, DoddleUSSClasses.FadeTranslateRestClass, DoddleUSSClasses.FadeTranslateUpClass) },
            { TransitionType.FadeTranslateBigRight, new TransitionClassSet(DoddleUSSClasses.FadeTranslateBigReqClass, DoddleUSSClasses.FadeTranslateBigRestClass, DoddleUSSClasses.FadeTranslateBigRightClass) },
            { TransitionType.FadeTranslateBigLeft, new TransitionClassSet(DoddleUSSClasses.FadeTranslateBigReqClass, DoddleUSSClasses.FadeTranslateBigRestClass, DoddleUSSClasses.FadeTranslateBigLeftClass) },
            { TransitionType.FadeTranslateBigDown, new TransitionClassSet(DoddleUSSClasses.FadeTranslateBigReqClass, DoddleUSSClasses.FadeTranslateBigRestClass, DoddleUSSClasses.FadeTranslateBigDownClass) },
            { TransitionType.FadeTranslateBigUp, new TransitionClassSet(DoddleUSSClasses.FadeTranslateBigReqClass, DoddleUSSClasses.FadeTranslateBigRestClass, DoddleUSSClasses.FadeTranslateBigUpClass) },
            { TransitionType.FadeTranslateScaleRight, new TransitionClassSet(DoddleUSSClasses.FadeTranslateScaleReqClass, DoddleUSSClasses.FadeTranslateScaleRestClass, DoddleUSSClasses.FadeTranslateScaleRightClass) },
            { TransitionType.FadeTranslateScaleLeft, new TransitionClassSet(DoddleUSSClasses.FadeTranslateScaleReqClass, DoddleUSSClasses.FadeTranslateScaleRestClass, DoddleUSSClasses.FadeTranslateScaleLeftClass) },
            { TransitionType.FadeTranslateScaleDown, new TransitionClassSet(DoddleUSSClasses.FadeTranslateScaleReqClass, DoddleUSSClasses.FadeTranslateScaleRestClass, DoddleUSSClasses.FadeTranslateScaleDownClass) },
            { TransitionType.FadeTranslateScaleUp, new TransitionClassSet(DoddleUSSClasses.FadeTranslateScaleReqClass, DoddleUSSClasses.FadeTranslateScaleRestClass, DoddleUSSClasses.FadeTranslateScaleUpClass) },
            { TransitionType.BounceRight, new TransitionClassSet(DoddleUSSClasses.BounceReqClass, DoddleUSSClasses.BounceRestClass, DoddleUSSClasses.BounceRightClass) },
            { TransitionType.BounceLeft, new TransitionClassSet(DoddleUSSClasses.BounceReqClass, DoddleUSSClasses.BounceRestClass, DoddleUSSClasses.BounceLeftClass) },
            { TransitionType.BounceDown, new TransitionClassSet(DoddleUSSClasses.BounceReqClass, DoddleUSSClasses.BounceRestClass, DoddleUSSClasses.BounceDownClass) },
            { TransitionType.BounceUp, new TransitionClassSet(DoddleUSSClasses.BounceReqClass, DoddleUSSClasses.BounceRestClass, DoddleUSSClasses.BounceUpClass) }
        };

        /// <summary>
        /// Adds the REQ and ANIM classes for the incoming TransitionType. Waits a frame before applying
        /// REST class, and removing ANIM class for resolvedStyle to be applied.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="transitionType"></param>
        /// <returns></returns>
        public static IEnumerator ApplyEnterTransition(VisualElement element, TransitionType transitionType)
        {
            TransitionClassSet set = ClassMap[transitionType];

            element.AddClasses(set.ReqClass, set.AnimClass);
            yield return null;
            element.RemoveFromClassList(set.AnimClass);
            element.AddToClassList(set.RestClass);
            yield return null;
        }

        /// <summary>
        /// Adds the REQ and REST classes for the incoming TransitionType. Waits a frame before applying
        /// ANIM class, and removing REST class for resolvedStyle to be applied.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="transitionType"></param>
        /// <returns></returns>
        public static IEnumerator ApplyExitTransition(VisualElement element, TransitionType transitionType)
        {
            TransitionClassSet set = ClassMap[transitionType];

            element.AddClasses(set.ReqClass, set.RestClass);
            yield return null;
            element.RemoveFromClassList(set.RestClass);
            element.AddToClassList(set.AnimClass);
            yield return null;
        }
    }
}