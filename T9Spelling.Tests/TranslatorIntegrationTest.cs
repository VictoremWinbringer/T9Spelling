
using System.IO;
using T9Spelling.Lib.Adapters;
using T9Spelling.Lib.Domain;
using Xunit;

namespace T9SPelling.tests
{
    public class TranslatorIntegrationTest
    {
        
        [Fact]
        public void Translate()
        {
            var translator =
                new T9Translator(new TextLinesAdapterFactory(), new EncoderFactory(new EnglishCodeAdapter()));
            var inFile = "./in.txt";
            var outFile = "./out.txt";
            
            var expected = new[]
            {
                "Case #1: 44 444",
                "Case #2: 999337777",
                "Case #3: 333666 6660 022 2777",
                "Case #4: 4433555 555666096667775553"
            };
            
            File.WriteAllLines(inFile, new []
            {
                "4",
                "hi",
                "yes",
                "foo  bar",
                "hello world"
            });
            
            translator.Translate(inFile,outFile);
            
             //Act
            var result = File.ReadAllLines(outFile);
            
            //Assert
            Assert.Equal(expected,result);
        }
    }
}