using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//Domain -----------------------------------------------------------
interface IT9Translator
{
    void Translate(string inPath, string outPath);
}

class T9Traslator : IT9Translator
{
    private readonly ILinesAdapterFactory _linesAdapterFactory;
    private readonly IEncoderFactory _encoderFactory;

    public T9Traslator(ILinesAdapterFactory linesAdapterFactory, IEncoderFactory encoderFactory)
    {
        _linesAdapterFactory = linesAdapterFactory;
        _encoderFactory = encoderFactory;
    }

    public void Translate(string inPath, string outPath)
    {
        var encoder = _encoderFactory.Create();
        using (var file = _linesAdapterFactory.Create(inPath, outPath))
        {
            var _ = file.ReadLine(); // skip line with count
            while (!file.LinesAreOver)
            {
                var line = file.ReadLine();
                file.WriteLine(encoder.Encode(line));
            }
        }
    }
}

interface IEncoder
{
    string Encode(string line);
}

class Encoder : IEncoder
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
        _sb.Append($"Case #{_lineNumber}: ");
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

interface IEncoderFactory
{
    IEncoder Create();
}

class EncoderFactory : IEncoderFactory
{
    private readonly ICodeAdapter _codeAdapter;

    public EncoderFactory(ICodeAdapter codeAdapter)
    {
        _codeAdapter = codeAdapter;
    }

    public IEncoder Create()
    {
        return new Encoder(_codeAdapter);
    }
}

interface ILinesAdapter : IDisposable
{
    string ReadLine();
    void WriteLine(string line);
    bool LinesAreOver { get; }
}

interface ILinesAdapterFactory
{
    ILinesAdapter Create(string inPath, string outPath);
}

interface ICodeAdapter
{
    string Get(char key);
}

//Adapters ----------------------------------------------------------
class EnglishCodeAdapter : ICodeAdapter
{
    private readonly Dictionary<char, string> _dictionary;

    public EnglishCodeAdapter()
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
    }

    public string Get(char key)
    {
        return _dictionary[key];
    }
}

class TextFileLinesAdapter : ILinesAdapter
{
    private readonly StreamReader _reader;
    private readonly StreamWriter _writer;

    public TextFileLinesAdapter(string inPath, string outPath)
    {
        _reader = new StreamReader(File.Open(inPath, FileMode.Open, FileAccess.Read));
        _writer = new StreamWriter(File.Open(outPath, FileMode.Create, FileAccess.Write));
    }

    public void Dispose()
    {
        _reader.Dispose();
        _writer.Dispose();
    }

    public string ReadLine()
    {
        return _reader.ReadLine();
    }

    public void WriteLine(string line)
    {
        _writer.WriteLine(line);
    }

    public bool LinesAreOver => _reader.EndOfStream;
}

class TextLinesAdapterFactory : ILinesAdapterFactory
{
    public ILinesAdapter Create(string inPath, string outPath)
    {
        return new TextFileLinesAdapter(inPath, outPath);
    }
}

//Ports ----------------------------------------------------------------------------------------------------

namespace T9SPelling
{
    class Program
    {
        static void Main(string[] args)
        {
            var inPath = args[0];
            var outPath = args[1];
            var translator =
                new T9Traslator(new TextLinesAdapterFactory(), new EncoderFactory(new EnglishCodeAdapter()));
            translator.Translate(inPath, outPath);
        }
    }
}