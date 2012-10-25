namespace Examples
{
    public class CustomToStringObject
    {
        private readonly string _value;

        public CustomToStringObject(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return string.Format("Value: {0}", _value);
        }
    }
}