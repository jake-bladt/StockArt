using System.Collections.Generic;

namespace StockArt.Domain
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string DisplayName { get; set; }
        public string CatalogImageType { get; set; }
        public string CatalogImagePath { get; set; }
        public List<ImageSetSubject> ImageSetSubjects { get; set; } = new List<ImageSetSubject>();
    }
}
