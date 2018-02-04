namespace StockArt.Domain
{
    public class ImageSetSubject
    {
        public string ImageSetName { get; set; }
        public ImageSet ImageSet { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }
}
