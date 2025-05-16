using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace doddle.uss
{
    //Yield return nulls are to wait for resolvedStyle the frame after. Otherwise final transition property could be applied right away
    //with no transition effect
    public static class LoopTransitionHelper
    {
        private static readonly float loopTime = 2.8f;

        /// <summary>
        /// This is so MonoBehaviours can call StartCoroutine(TransitionHelper.[ApplyEnterTransition/ApplyExitTransition]) to automatically start or 
        /// end an transition based on the LoopTransitionType. Any element should be able to use this. This applies REQ, ANIM, and REST USS classes
        /// </summary>
        public static readonly Dictionary<LoopTransitionType, TransitionClassSet> ClassMap = new Dictionary<LoopTransitionType, TransitionClassSet>
        {
            { LoopTransitionType.Breathe, new TransitionClassSet(DoddleUSSClasses.BreatheLoopReqClass, DoddleUSSClasses.BreatheLoopRestClass, DoddleUSSClasses.BreatheLoopAnimClass) },
            { LoopTransitionType.BreatheRotateCW, new TransitionClassSet(DoddleUSSClasses.BreatheRotateCWLoopReqClass, DoddleUSSClasses.BreatheRotateCWLoopRestClass, DoddleUSSClasses.BreatheRotateCWLoopAnimClass) },
            { LoopTransitionType.BreatheRotateCCW, new TransitionClassSet(DoddleUSSClasses.BreatheRotateCCWLoopReqClass, DoddleUSSClasses.BreatheRotateCCWLoopRestClass, DoddleUSSClasses.BreatheRotateCCWLoopAnimClass) }
        };

        /// <summary>
        /// Adds the REQ and REST classes for the incoming LoopTransitionType. Waits a frame before applying
        /// ANIM class for resolvedStyle to be applied. Once transition is complete, ANIM class is continuously
        /// toggled to loop the transition
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IEnumerator ApplyLoopTransition(VisualElement element, LoopTransitionType transitionType)
        {
            TransitionClassSet set = ClassMap[transitionType];

            element.AddClasses(set.ReqClass, set.RestClass);
            yield return null;
            element.AddToClassList(set.AnimClass);

            //TODO: Make OnTransitionEnded events work with rotate and scale properties together. Animation was getting stuck on ANIM state
            while (true)
            {
                yield return new WaitForSecondsRealtime(loopTime);
                element.ToggleInClassList(set.AnimClass);
            }
        }
    }
}