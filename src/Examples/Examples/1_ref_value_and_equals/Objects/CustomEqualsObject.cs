namespace Examples
{
    public class CustomEqualsObject
    {
        public int Value { get; set; }

        public CustomEqualsObject(int value)
        {
            Value = value;
        }

        // Exceptions should never be thrown inside an Equals implementation
        public override bool Equals(object obj)
        {
            var other = obj as CustomEqualsObject;

            if (other == null)
                return false;

            return Value == other.Value;
        }
    }
}