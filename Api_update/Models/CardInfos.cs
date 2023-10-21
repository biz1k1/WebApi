using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Newtonsoft.Json;

namespace CashbackApi.Models {
    public class CardInfos {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string BankType { get; set; }
        public string BankCard{ get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime LastUpdate { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Categories> Category { get; set; } = new List<Categories>();
    }
}
