using System.Text;
using T9Spelling.Lib.Domain.AdatersInterfaces;

namespace T9Spelling.Lib.Domain
{
    public interface IEncoder
    {
        string Encode(string line);
    }

    public class Encoder : IEncoder
    {
        private readonly ICodeAdapter _codeAdapter;
        private readonly StringBuilder _sb;
        private int _lineNumber;

        public Encoder(ICodeAdapter codeAdapter)
        {
            _codeAdapter = codeAdapter;
            _sb = new StringBuilder();
            _lineNumber = 1;
        }

        public string Encode(string line)
        {
            _sb.Clear();
            _sb.Append($"Case #{_lineNumber.ToString()}: ");
            _lineNumber++;
            foreach (var c in line)
            {
                var code = _codeAdapter.Get(c);
                if (_sb[_sb.Length - 1] == code[0])
                    _sb.Append(" ");
                _sb.Append(code);
            }

            return _sb.ToString();
        }
    }
}