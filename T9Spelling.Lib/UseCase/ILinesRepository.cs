using System;
using T9Spelling.Lib.Domain;

namespace T9Spelling.Lib.UseCase
{
    public interface ILinesRepository : IDisposable
    {
        Line ReadLine();
        void WriteLine(Line line);
        bool LinesAreOver { get; }
    }

    public interface ILinesRepositoryFactory
    {
        ILinesRepository Create(string inPath, string outPath);
    }
}