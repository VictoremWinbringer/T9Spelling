using Moq;
using T9Spelling.Lib.Domain.AdatersInterfaces;

namespace T9SPelling.tests
{
    public class TranslatorTest
    {
        public TranslatorTest()
        {

            var lines = new Mock<ILinesAdapter>().Setup(l => l.LinesAreOver).Returns();
        }  
    }
}