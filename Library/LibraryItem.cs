namespace Library
{
    public class LibraryItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }

        public virtual string GetDetails()
        {
            return $"ID: {ID}, Title: {Title}, Year: {YearPublished}";
        }
    }
}