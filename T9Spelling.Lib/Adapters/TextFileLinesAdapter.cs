using System.IO;
using T9Spelling.Lib.Domain.AdatersInterfaces;

namespace T9Spelling.Lib.Adapters
{
    public class TextFileLinesAdapter : ILinesAdapter
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

    public class TextLinesAdapterFactory : ILinesAdapterFactory
    {
        public ILinesAdapter Create(string inPath, string outPath)
        {
            return new TextFileLinesAdapter(inPath, outPath);
        }
    }
}