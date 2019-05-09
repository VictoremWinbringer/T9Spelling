namespace T9Spelling.Lib.Domain
{
    public class Line
    {
        public string Value { get; }

        public Line(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new LineIsNullOrEmptyException();
            Value = value;
        }
    }
}