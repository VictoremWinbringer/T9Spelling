namespace T9Spelling.Lib.UseCase
{
    public class EncoderService : IEncoderService
    {
        private readonly ILinesRepositoryFactory _linesRepositoryFactory;
        private readonly IEncoderRepository _encoderRepository;

        public EncoderService(ILinesRepositoryFactory linesRepositoryFactory,
            IEncoderRepository encoderRepository)
        {
            _linesRepositoryFactory = linesRepositoryFactory;
            _encoderRepository = encoderRepository;
        }

        public void Translate(string inPath, string outPath)
        {
            var encoder = _encoderRepository.Get();
            using (var file = _linesRepositoryFactory.Create(inPath, outPath))
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