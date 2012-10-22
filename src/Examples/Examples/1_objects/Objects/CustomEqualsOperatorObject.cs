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
            // Do NOT check a == null here...
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
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