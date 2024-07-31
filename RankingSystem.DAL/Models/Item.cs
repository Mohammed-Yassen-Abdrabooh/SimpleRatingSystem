using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingSystem.DAL.Models
{
    public class Item
    {
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }

        [InverseProperty("Item")]
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();

    }
}
