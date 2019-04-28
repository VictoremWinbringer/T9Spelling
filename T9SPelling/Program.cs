using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using T9Spelling.Lib.Adapters;
using T9Spelling.Lib.Domain;

namespace T9SPelling
{
    class Program
    {
        static void Main(string[] args)
        {
            var inPath = args[0];
            var outPath = args[1];
            var translator =
                new T9Translator(new TextLinesAdapterFactory(), new EncoderFactory(new EnglishCodeAdapter()));
            translator.Translate(inPath, outPath);
        }
    }
}