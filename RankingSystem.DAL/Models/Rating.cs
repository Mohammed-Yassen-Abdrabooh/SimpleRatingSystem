using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingSystem.DAL.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int StarsNum { get; set; }

        [ForeignKey("Item")]
        public int? ItemId { get; set; }
        [InverseProperty("Ratings")]
        public Item Item { get; set; }

    }
}
