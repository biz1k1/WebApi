using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashbackApi.Models {
    public class Categories {
        [Key]
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public double CashBackCategory { get; set; }
        public ICollection<CardInfos> Cardinfo { get; set; } = new List<CardInfos>();
    }
}
