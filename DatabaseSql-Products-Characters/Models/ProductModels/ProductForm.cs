using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DatabaseSql_Products_Characters.Models
{
    public class ProductForm
    {
        public string ArticleNumber { get; set; } = null!;

        public string Artist { get; set; } = null!;

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }  

        public string Genre { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string? About { get; set; }

        public string? ReleseDate { get; set; }

        public string? RecordedDate { get; set; }

        public string? Country { get; set; }

        public string? Label { get; set; }
    }
}
