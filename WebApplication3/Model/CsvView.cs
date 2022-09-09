namespace WebApplication3.Model
{
    public class CsvView
    {
        public int ItemId { get; set; }
        public int SubcategoryId { get; set; }
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public string SubName { get; set; }
        public string CategoryName { get; set; }
        public bool Archived { get; set; }
    }
}
