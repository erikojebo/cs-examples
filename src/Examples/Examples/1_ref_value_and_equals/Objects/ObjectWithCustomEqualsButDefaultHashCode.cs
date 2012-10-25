using System;
using System.Collections;
using System.Collections.Generic;

namespace Examples
{
    public class ObjectWithCustomEqualsButDefaultHashCode
    {
        private readonly string _value;

        public ObjectWithCustomEqualsButDefaultHashCode(string value)
        {
            _value = value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ObjectWithCustomEqualsButDefaultHashCode;

            if (other == null)
                return false;

            return _value == other._value;
        }

        // No GetHashCode implementation here, so default implementation is used
        // which means that different instances get different hash codes even though
        // they are equal according to the custom Equals implementation
    }
}