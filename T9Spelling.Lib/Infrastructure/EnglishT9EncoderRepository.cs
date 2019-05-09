using T9Spelling.Lib.Domain;
using T9Spelling.Lib.UseCase;

namespace T9Spelling.Lib.Infrastructure
{
    public class EnglishT9EncoderRepository : IEncoderRepository
    {
        public IEncoder Get() => new EnglishT9Encoder();
    }
}