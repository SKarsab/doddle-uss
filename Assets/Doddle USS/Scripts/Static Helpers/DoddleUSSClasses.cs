using System.Collections.Generic;

namespace doddle.uss
{
    public static class DoddleUSSClasses
    {
        //-----------------------------------------------------------------------
        //Font Classes
        //-----------------------------------------------------------------------
        public static readonly string FontBlackClass = "fw-900";
        public static readonly string FontBlackItalicClass = "fw-900-italic";
        public static readonly string FontBoldClass = "fw-700";
        public static readonly string FontBoldItalicClass = "fw-700-italic";
        public static readonly string FontRegularClass = "fw-400";
        public static readonly string FontRegularItalicClass = "fw-400-italic";
        public static readonly string FontLightClass = "fw-300";
        public static readonly string FontLightItalicClass = "fw-300-italic";
        public static readonly string FontThinClass = "fw-100";
        public static readonly string FontThinItalicClass = "fw-100-italic";

        public static readonly List<string> FontWeights = new List<string> { FontBlackClass, FontBlackItalicClass, FontBoldClass, FontBoldItalicClass, FontRegularClass, FontRegularItalicClass, FontLightClass, FontLightItalicClass, FontThinClass, FontThinItalicClass };



        //-----------------------------------------------------------------------
        //Transition Hover Classes
        //-----------------------------------------------------------------------
        public static readonly string GrowXYHoverClass = "grow-xy--hover";
        public static readonly string GrowXHoverClass = "grow-x--hover";
        public static readonly string GrowYHoverClass = "grow-y--hover";

        public static readonly string TranslateLeftHoverClass = "translate-left--hover";
        public static readonly string TranslateRightHoverClass = "translate-right--hoverr";
        public static readonly string TranslateUpHoverClass = "translate-up--hover";
        public static readonly string TranslateDownHoverClass = "translate-down--hover";

        public static readonly string OpacityDownHoverClass = "opacity-down--hover";
        public static readonly string BGColourHoverClass = "bg-colour--hover";
        public static readonly string BColourHoverClass = "b-colour--hover";
        public static readonly string ColourHoverClass = "colour--hover";
        public static readonly string AllColourHoverClass = "all-colour--hover";



        //-----------------------------------------------------------------------
        //Transition Loop Classes
        //-----------------------------------------------------------------------
        public static readonly string BreatheLoopReqClass = "breathe--req";
        public static readonly string BreatheLoopRestClass = "breathe--rest";
        public static readonly string BreatheLoopAnimClass = "breathe--anim";

        public static readonly string BreatheRotateCWLoopReqClass = "breathe-rotate-cw--req";
        public static readonly string BreatheRotateCWLoopRestClass = "breathe-rotate-cw--rest";
        public static readonly string BreatheRotateCWLoopAnimClass = "breathe-rotate-cw--anim";
        
        public static readonly string BreatheRotateCCWLoopReqClass = "breathe-rotate-ccw--req";
        public static readonly string BreatheRotateCCWLoopRestClass = "breathe-rotate-ccw--rest";
        public static readonly string BreatheRotateCCWLoopAnimClass = "breathe-rotate-ccw--anim";



        //-----------------------------------------------------------------------
        //Transition Enter/Exit Classes
        //-----------------------------------------------------------------------
        public static readonly string FadeReqClass = "fade--req";
        public static readonly string FadeRestClass = "fade--rest";
        public static readonly string FadeAnimClass = "fade--anim";

        public static readonly string FadeTranslateBigReqClass = "fade-translate-big--req";
        public static readonly string FadeTranslateBigRestClass = "fade-translate-big--rest";
        public static readonly string FadeTranslateBigLeftClass = "fade-translate-big--left";
        public static readonly string FadeTranslateBigRightClass = "fade-translate-big--right";
        public static readonly string FadeTranslateBigUpClass = "fade-translate-big--up";
        public static readonly string FadeTranslateBigDownClass = "fade-translate-big--down";

        public static readonly string FadeTranslateReqClass = "fade-translate--req";
        public static readonly string FadeTranslateRestClass = "fade-translate--rest";
        public static readonly string FadeTranslateLeftClass = "fade-translate--left";
        public static readonly string FadeTranslateRightClass = "fade-translate--right";
        public static readonly string FadeTranslateUpClass = "fade-translate--up";
        public static readonly string FadeTranslateDownClass = "fade-translate--down";

        public static readonly string FadeTranslateScaleReqClass = "fade-scale-translate--req";
        public static readonly string FadeTranslateScaleRestClass = "fade-scale-translate--rest";
        public static readonly string FadeTranslateScaleLeftClass = "fade-scale-translate--left";
        public static readonly string FadeTranslateScaleRightClass = "fade-scale-translate--right";
        public static readonly string FadeTranslateScaleUpClass = "fade-scale-translate--up";
        public static readonly string FadeTranslateScaleDownClass = "fade-scale-translate--down";

        public static readonly string BounceReqClass = "bounce--req";
        public static readonly string BounceRestClass = "bounce--rest";
        public static readonly string BounceLeftClass = "bounce--left";
        public static readonly string BounceRightClass = "bounce--right";
        public static readonly string BounceUpClass = "bounce--up";
        public static readonly string BounceDownClass = "bounce--down";
    }
}