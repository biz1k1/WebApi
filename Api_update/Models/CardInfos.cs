    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CashbackApi.Models {
    public class CardInfos {
        [Key]
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string BankType { get; set; }
        public string BankCard{ get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<Categories> Category { get; set; } = new List<Categories>();
    }
}
