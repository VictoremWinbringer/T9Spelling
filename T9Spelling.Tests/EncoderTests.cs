using System;
using Moq;
using T9Spelling.Lib.Domain;
using T9Spelling.Lib.Domain.AdatersInterfaces;
using Xunit;

namespace T9SPelling.tests
{
    public class EncoderTests
    {
        private readonly Encoder _encoder;

        public EncoderTests()
        {
            var adapter = new Mock<ICodeAdapter>();
            adapter.Setup(a => a.Get('a')).Returns("11");
            adapter.Setup(a => a.Get('b')).Returns("22");
            _encoder = new Encoder(adapter.Object);
        }

        [Fact]
        public void Encode()
        {
            var values = new[]
            {
                ("b", "Case #1: 22"),
                ("bb", "Case #2: 22 22"),
                ("ab", "Case #3: 1122")
            };
            
            foreach (var (input, output) in values)
            {
                //Act
                var result = _encoder.Encode(input);
                //Assert
                Assert.Equal(result, output);
            }
        }

        [Fact]
        public void EncodeNull()
        {
            Assert.Throws<NullReferenceException>(()=>_encoder.Encode(null));
        }
    }
}