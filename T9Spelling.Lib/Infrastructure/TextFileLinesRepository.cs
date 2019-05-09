using System.IO;
using T9Spelling.Lib.Domain;
using T9Spelling.Lib.UseCase;

namespace T9Spelling.Lib.Infrastructure
{
    public class TextFileLinesRepository : ILinesRepository
    {
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        public TextFileLinesRepository(string inPath, string outPath)
        {
            _reader = new StreamReader(File.Open(inPath, FileMode.Open, FileAccess.Read));
            _writer = new StreamWriter(File.Open(outPath, FileMode.Create, FileAccess.Write));
        }

        public void Dispose()
        {
            _reader.Dispose();
            _writer.Dispose();
        }

        public Line ReadLine()
        {
            return new Line( _reader.ReadLine());
        }

        public void WriteLine(Line line)
        {
            _writer.WriteLine(line);
        }

        public bool LinesAreOver => _reader.EndOfStream;
    }

    public class TextLinesRepositoryFactory : ILinesRepositoryFactory
    {
        public ILinesRepository Create(string inPath, string outPath)
        {
            return new TextFileLinesRepository(inPath, outPath);
        }
    }
}