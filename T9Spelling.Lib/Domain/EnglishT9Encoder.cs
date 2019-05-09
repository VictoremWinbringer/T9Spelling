using System.Collections.Generic;
using System.Text;

namespace T9Spelling.Lib.Domain
{
    public class EnglishT9Encoder : IEncoder
    {
        private readonly Dictionary<char, string> _dictionary;
        private readonly StringBuilder _sb;
        private int _lineNumber;

        public EnglishT9Encoder()
        {
            _dictionary = new Dictionary<char, string>()
            {
                [' '] = "0",
                ['a'] = "2",
                ['b'] = "22",
                ['c'] = "222",
                ['d'] = "3",
                ['e'] = "33",
                ['f'] = "333",
                ['g'] = "4",
                ['h'] = "44",
                ['i'] = "444",
                ['j'] = "5",
                ['k'] = "55",
                ['l'] = "555",
                ['m'] = "6",
                ['n'] = "66",
                ['o'] = "666",
                ['p'] = "7",
                ['q'] = "77",
                ['r'] = "777",
                ['s'] = "7777",
                ['t'] = "8",
                ['u'] = "88",
                ['v'] = "888",
                ['w'] = "9",
                ['x'] = "99",
                ['y'] = "999",
                ['z'] = "9999",
            };
            _sb = new StringBuilder();
            _lineNumber = 1;
        }

        public Line Encode(Line line)
        {
            _sb.Clear();
            _sb.Append($"Case #{_lineNumber.ToString()}: ");
            _lineNumber++;
            foreach (var c in line.Value)
            {
                var code = _dictionary[c];
                if (_sb[_sb.Length - 1] == code[0])
                    _sb.Append(" ");
                _sb.Append(code);
            }

            return new Line(_sb.ToString());
        }
    }
}