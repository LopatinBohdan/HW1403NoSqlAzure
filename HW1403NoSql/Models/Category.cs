namespace HW1403NoSql.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public  ICollection<Good> goods { get; set; }

        public Category() { goods = new List<Good>(); }
    }
}
