using System.Collections.Generic;
using UnityEngine;

namespace doddle.uss.editor
{
    [CreateAssetMenu(fileName = "DoddleUtilityData", menuName = "Doddle USS/Doddle Utility Data")]
    public class DoddleUtilityData : ScriptableObject
    {
        public List<Utility> Utilities;
    }
}