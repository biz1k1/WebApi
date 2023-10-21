using CashbackApi.Data;
using CashbackApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CashbackApi.Controllers {
    //[Authorize]
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
            return Ok(await _context.CardInfo.Include(x => x.Category).ToListAsync());
        }
        [HttpGet]
        [Route(template: "GetById")]
        public async Task<IActionResult> GetId(int id) {
            var cashback = await _context.CardInfo.Include(x=>x.Category).FirstOrDefaultAsync(x => x.CardId == id);
            if (cashback == null) return NotFound();
            return Ok(cashback);
        }
        [HttpPost]
        [Route(template: "AddCard")]
        public async Task<IActionResult> AddCashback(CardInfos card) {
            var ExistBankCard = await _context.CardInfo.FirstOrDefaultAsync(x => x.BankCard== card.BankCard);
            if (ExistBankCard?.BankCard != null) return Ok("Duplicate BankCard");
            _context.CardInfo.AddRange(card);
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
        public async Task<IActionResult> UpdateCashback(int id,CardInfos card) {
            var ExistCard = await _context.CardInfo.FirstOrDefaultAsync(x => x.CardId == id);
            if (ExistCard == null) return NotFound();
            ExistCard.CardName = card.CardName;
            ExistCard.BankType = card.BankType;
            ExistCard.BankCard = card.BankCard;
            ExistCard.LastUpdate = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}

