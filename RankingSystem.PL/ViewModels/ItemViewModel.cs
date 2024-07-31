using RankingSystem.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RankingSystem.PL.ViewModels
{
    public class ItemViewModel
    {
        public int id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage = "Max Length For Name is 50 Char")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public double AverageRating { get; set; }
        public int RatingCount { get; set; }

        //public List<SelectListItem> selectRating { get; set; }
        [InverseProperty("Item")]
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    }
}
