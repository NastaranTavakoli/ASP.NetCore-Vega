using System.IO;
using System.Linq;

namespace Application.Types
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string fileName)
        {
            return (AcceptedFileTypes.Any(t => t == Path.GetExtension(fileName).ToLower()));
        }
    }
}