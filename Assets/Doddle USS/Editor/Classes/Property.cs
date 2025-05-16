using System;
using UnityEngine;

namespace doddle.uss.editor
{
    [Serializable]
    public class Property
    {
        [Tooltip("Name of the property that is placed INSIDE the class 'margin-left', etc.")]
        public string Name = "";

        [Tooltip("Value for the property. Can be int for px/% or string for flex-row, etc.")]
        public string Value = "";

        [Tooltip("Unit either px, % or left blank")]
        public string Unit = "";

        public Property(string name, string value, string unit)
        {
            Name = name;
            Value = value;
            Unit = unit;
        }
    }
}