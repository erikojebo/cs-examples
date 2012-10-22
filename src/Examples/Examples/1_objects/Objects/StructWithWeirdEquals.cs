namespace Examples
{
    public struct StructWithWeirdEquals
    {
        public StructWithWeirdEquals(int x, int y)
        {
            X = x;
            Y = y;
        }

        public readonly int X;
        public readonly int Y;

        public override bool Equals(object obj)
        {
            return obj is StructWithWeirdEquals && ((StructWithWeirdEquals)obj).X == X;
        }

        public override int GetHashCode()
        {
            return X;
        }
    }
}