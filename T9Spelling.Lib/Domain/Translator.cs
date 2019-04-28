using T9Spelling.Lib.Domain.AdatersInterfaces;

namespace T9Spelling.Lib.Domain
{
    public interface ITranslator
    {
        void Translate(string inPath, string outPath);
    }

    public class T9Translator : ITranslator
    {
        private readonly ILinesAdapterFactory _linesAdapterFactory;
        private readonly IEncoderFactory _encoderFactory;

        public T9Translator(ILinesAdapterFactory linesAdapterFactory, IEncoderFactory encoderFactory)
        {
            _linesAdapterFactory = linesAdapterFactory;
            _encoderFactory = encoderFactory;
        }

        public void Translate(string inPath, string outPath)
        {
            var encoder = _encoderFactory.Create();
            using (var file = _linesAdapterFactory.Create(inPath, outPath))
            {
                var _ = file.ReadLine(); // skip line with count
                while (!file.LinesAreOver)
                {
                    var line = file.ReadLine();
                    file.WriteLine(encoder.Encode(line));
                }
            }
        }
    }
}