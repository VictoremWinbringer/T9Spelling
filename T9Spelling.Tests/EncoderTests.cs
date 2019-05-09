using System;
using System.Text;
using Moq;
using T9Spelling.Lib.Domain;
using Xunit;

namespace T9SPelling.tests
{
    public class EncoderTests
    {
        private readonly EnglishT9Encoder _encoder;

        public EncoderTests()
        {
            _encoder = new EnglishT9Encoder();
        }

        [Fact]
        public void Encode()
        {
            var values = new[]
            {
                ("b", "Case #1: 22"),
                ("bb", "Case #2: 22 22"),
                ("db", "Case #3: 322")
            };
            
            foreach (var (input, output) in values)
            {
                //Act
                var result = _encoder.Encode(new Line( input));
                //Assert
                Assert.Equal(result.Value, output);
            }
        }

        [Fact]
        public void EncodeNull()
        {
            Assert.Throws<NullReferenceException>(()=>_encoder.Encode(null));
        }
    }
}