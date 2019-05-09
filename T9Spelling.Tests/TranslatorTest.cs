using System.Collections.Generic;
using Moq;
using T9Spelling.Lib.Domain;
using T9Spelling.Lib.UseCase;
using Xunit;

namespace T9SPelling.tests
{
    public class TranslatorTest
    {
        private readonly IEncoderRepository _encoderFactory;
        private readonly ILinesRepositoryFactory _linesRepositoryFactory;
        private readonly List<string> _outLines;
        
        public TranslatorTest()
        {
            var counter = 0;
            
           _outLines = new List<string>();
           
            var lines = new Mock<ILinesRepository>();
                lines.Setup(l => l.LinesAreOver).Returns( () =>
                {
                    return counter > 2;
                });
                lines.Setup(l => l.WriteLine(It.IsAny<Line>())).Callback<Line>(s => _outLines.Add(s.Value));
                lines.Setup(l => l.ReadLine()).Returns(() =>
                {
                    counter++;
                    return new Line( "test");
                });
                
            var encoder = new Mock<IEncoder>();
                encoder.Setup(e=> e.Encode(It.IsAny<Line>()))
                .Returns( new Line("test"));

            var linesFactory = new Mock<ILinesRepositoryFactory>();
                linesFactory
                .Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(lines.Object);
                
            var encoderFactory = new Mock<IEncoderRepository>();
            encoderFactory.Setup(e => e.Get()).Returns(encoder.Object);
            
            _encoderFactory = encoderFactory.Object;
            _linesRepositoryFactory = linesFactory.Object;
        }

        [Fact]
        public void Translate()
        {
            var translator = new EncoderService(_linesRepositoryFactory,_encoderFactory);
            translator.Translate("in","out");
            Assert.Equal(_outLines, new []{"test","test"});
        }
    }
}