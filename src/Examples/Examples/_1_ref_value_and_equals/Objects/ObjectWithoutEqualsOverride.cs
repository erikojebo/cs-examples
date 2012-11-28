using System.Collections.Generic;

namespace Examples
{
    public class ObjectWithoutEqualsOverride
    {
        public int Value { get; set; }

        public ObjectWithoutEqualsOverride(int value)
        {
            Value = value;
        }
    }

    public class ObjectWithoutEqualsOverrideComparer : IEqualityComparer<ObjectWithoutEqualsOverride>
    {
        public bool Equals(ObjectWithoutEqualsOverride x, ObjectWithoutEqualsOverride y)
        {
            return x.Value == y.Value;
        }

        public int GetHashCode(ObjectWithoutEqualsOverride obj)
        {
            return obj.Value;
        }
    }
}