using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CashbackApi.Models {
    public class Categories {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double CategoryValue { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public ICollection<CardInfos> Cardinfo { get; set; } = new List<CardInfos>();
    }
}
