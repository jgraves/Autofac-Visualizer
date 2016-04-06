using System.Collections.Generic;
using NGenerics.DataStructures.Trees;

namespace AutofacVisualizer.ConsoleTest {

    public class MakesStrings {
        public MakesStrings(IGiveString stringGiver, string myString) {}
    }

    public interface IGiveString {
    }

    public class UsesString : IGiveString {

        public UsesString(TreeWrapper wrapper, string gimme) { }

    }

    public class TreeWrapper {
        public TreeWrapper(ITree<int> tree, IEnumerable<string> strings){}
    }

    public class UsesInt : IGiveString {
        private readonly int gimme;

        public UsesInt(int gimme) { }

    }
}