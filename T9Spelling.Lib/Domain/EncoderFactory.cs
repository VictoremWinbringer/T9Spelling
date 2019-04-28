using T9Spelling.Lib.Domain.AdatersInterfaces;

namespace T9Spelling.Lib.Domain
{
    public interface IEncoderFactory
    {
        IEncoder Create();
    }

    public class EncoderFactory : IEncoderFactory
    {
        private readonly ICodeAdapter _codeAdapter;

        public EncoderFactory(ICodeAdapter codeAdapter)
        {
            _codeAdapter = codeAdapter;
        }

        public IEncoder Create()
        {
            return new Encoder(_codeAdapter);
        }
    }
}