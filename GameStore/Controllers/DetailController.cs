using GameStore.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class DetailController(GameStoreDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.Games.FindAsync(id));
        }
    }
}
