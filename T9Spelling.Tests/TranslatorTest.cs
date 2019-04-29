using System.Collections.Generic;
using Moq;
using T9Spelling.Lib.Domain;
using T9Spelling.Lib.Domain.AdatersInterfaces;
using Xunit;

namespace T9SPelling.tests
{
    public class TranslatorTest
    {
        private readonly IEncoderFactory _encoderFactory;
        private readonly ILinesAdapterFactory _linesAdapterFactory;
        private readonly List<string> _outLines;
        
        public TranslatorTest()
        {
            var counter = 0;
           _outLines = new List<string>();
            var lines = new Mock<ILinesAdapter>();
                lines.Setup(l => l.LinesAreOver).Returns( () =>
                {
                    return counter > 2;
                });
                lines.Setup(l => l.WriteLine(It.IsAny<string>())).Callback<string>(s => _outLines.Add(s));
                lines.Setup(l => l.ReadLine()).Returns(() =>
                {
                    counter++;
                    return "test";
                });
            var encoder = new Mock<IEncoder>();
                encoder.Setup(e=> e.Encode(It.IsAny<string>()))
                .Returns("test");

            var linesFactory = new Mock<ILinesAdapterFactory>();
                linesFactory
                .Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(lines.Object);
            var encoderFactory = new Mock<IEncoderFactory>();
            encoderFactory.Setup(e => e.Create()).Returns(encoder.Object);
            _encoderFactory = encoderFactory.Object;
            _linesAdapterFactory = linesFactory.Object;
        }

        [Fact]
        public void Translate()
        {
            var translator = new T9Translator(_linesAdapterFactory,_encoderFactory);
            translator.Translate("in","out");
            Assert.Equal(_outLines, new []{"test","test"});
        }
    }
}