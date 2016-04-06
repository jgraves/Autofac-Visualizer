using System;
using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {

    [Serializable]
    public class ResolutionTree {
        public ComponentRegistration Built { get; set; }
        public IEnumerable<ResolutionTree> Buildees { get; set; }
    }
}