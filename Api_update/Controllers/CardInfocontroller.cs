using CashbackApi.Data;
using CashbackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CashbackApi.Controllers {
    [ApiController]
    [Route(template: "[controller]")]
    public class CardInfocontroller : ControllerBase {

        private readonly DataContext _context;
        public CardInfocontroller(DataContext context) {
            _context = context;
        }
        [HttpGet]
        [Route(template:"GetAll")]
        public async Task<IActionResult> Get() {
            return Ok(await _context.CardInfo.ToListAsync());
        }
        [HttpGet]
        [Route(template: "GetById")]
        public async Task<IActionResult> GetId(int id) {
            var cashback = await _context.CardInfo.FirstOrDefaultAsync(x => x.CardId == id);
            if (cashback == null) return NotFound();
            return Ok(cashback);
        }
        [HttpPost]
        [Route(template: "AddCard")]
        public async Task<IActionResult> AddCashback(string cardName,string bankType,string bankCard) {
            var NewCard = new CardInfos {
                CardName = cardName,
                BankType = bankType,
                BankCard = bankCard,
                LastUpdate = DateTime.Now,
            };
            _context.CardInfo.Add(NewCard);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route(template: "DeleteCard")]
        public async Task<IActionResult> DeleteCashback(int id) {
            var card = await _context.CardInfo.FirstOrDefaultAsync(x => x.CardId == id);
            if (card == null) return NotFound();

            _context.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch]
        [Route(template: "UpdateCard")]
        public async Task<IActionResult> UpdateCashback(int Id,string cardName, string bankType, string bankCard) {
            var ExistCard = await _context.CardInfo.FirstOrDefaultAsync(x => x.CardId == Id);
            if (ExistCard == null) return NotFound();
            ExistCard.CardName = cardName;
            ExistCard.BankType = bankType;
            ExistCard.BankCard = bankCard;
            ExistCard.LastUpdate = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
