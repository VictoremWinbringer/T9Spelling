using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using T9Spelling.Lib.Domain;
using T9Spelling.Lib.Infrastructure;
using T9Spelling.Lib.UseCase;

namespace T9SPelling
{
    class Program
    {
        static void Main(string[] args)
        {
            var inPath = args[0];
            var outPath = args[1];
            var translator =
                new EncoderService(new TextLinesRepositoryFactory(), new EnglishT9EncoderRepository());
            translator.Translate(inPath, outPath);
        }
    }
}