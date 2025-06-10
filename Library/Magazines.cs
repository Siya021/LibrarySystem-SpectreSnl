using Library;

namespace Library
{
    public class Magazine : LibraryItem
    {
        public int IssueNumber { get; set; }
        public MonthsEnum MonthsEnum { get; set; }

        public override string GetDetails()
        {
            string monthName = Enum.GetName(typeof(MonthsEnum), MonthsEnum) ?? "Unknown";
            return $"{base.GetDetails()}, Issue: {IssueNumber}, Month: {monthName}";
        }
    }
}