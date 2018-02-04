using System.Collections.Generic;

namespace StockArt.Domain
{
    public class ImageSet
    {
        public string Name { get; set; }
        public int ImageCount { get; set; }
        public List<ImageSetSubject> ImageSetSubjects { get; set; } = new List<ImageSetSubject>();
    }
}
