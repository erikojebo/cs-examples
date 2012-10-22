namespace Examples
{
    public class CustomEqualsOperatorObject
    {
        public CustomEqualsOperatorObject(int value)
        {
            Value = value;
        }

        protected int Value { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CustomEqualsOperatorObject;

            return this == other;
        }

        public static bool operator ==(CustomEqualsOperatorObject a, CustomEqualsOperatorObject b)
        {
            if (a == null && b == null)
                return true;
            if (a == null)
                return false;
            if (b == null)
                return false;

            return a.Value == b.Value;
        }

        public static bool operator !=(CustomEqualsOperatorObject a, CustomEqualsOperatorObject b)
        {
            return !(a == b);
        }

        public bool Equals(CustomEqualsOperatorObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}