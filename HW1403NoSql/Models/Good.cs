using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HW1403NoSql.Models
{
    public class Good
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public virtual Category Category { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, title: {Title}, price:{Price}";
        }

    }
}
