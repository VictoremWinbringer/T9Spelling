using System;

namespace T9Spelling.Lib.Domain.AdatersInterfaces
{
    public interface ILinesAdapter : IDisposable
    {
        string ReadLine();
        void WriteLine(string line);
        bool LinesAreOver { get; }
    }

    public interface ILinesAdapterFactory
    {
        ILinesAdapter Create(string inPath, string outPath);
    }
}