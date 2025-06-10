namespace Library
{
    public class Book : LibraryItem
    {
        public string Author { get; set; }
        public string Genre { get; set; }

        public override string GetDetails()
        {
            return $"{base.GetDetails()}, Author: {Author}, Genre: {Genre}";
        }
    }
}