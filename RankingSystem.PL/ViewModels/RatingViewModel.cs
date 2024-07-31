using RankingSystem.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RankingSystem.PL.ViewModels
{
    public class RatingViewModel
    {
        //public int Id { get; set; }
        [Range(1,5,ErrorMessage ="The Range Must Be From 1 To 5")]
        public int StarsNum { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }
        [InverseProperty("Ratings")]
        public Item? Item { get; set; }
    }
}
