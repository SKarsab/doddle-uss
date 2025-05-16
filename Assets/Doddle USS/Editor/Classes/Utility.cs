using System;
using System.Collections.Generic;
using UnityEngine;

namespace doddle.uss.editor
{
    [Serializable]
    public class Utility
    {
        [Tooltip("Name of the class. Will be appended with -[value] suffix is instance > 1")]
        public string ClassName = "";

        [ Tooltip("# of instances to increment the class")]
        public int Instances = 1;

        [Tooltip("List of properties defined in each class increment")]
        public List<Property> Properties;

        public Utility(string className, int instances)
        {
            ClassName = className;
            Instances = instances;
            Properties = new List<Property>();
        }
    }
}