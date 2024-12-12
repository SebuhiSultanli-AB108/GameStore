using GameStore.DataAccess;
using GameStore.Models;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController(GameStoreDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index() { return View(await _context.Games.ToListAsync()); }
        public IActionResult Create() { return View(); }
        public IActionResult Update() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Create(GameVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            if (!vm.Image.ContentType.StartsWith("image"))
            {
                ModelState.AddModelError("File", "Format type must be an image.");
                return View(vm);
            }
            if (vm.Image.Length > 50 * 1024 * 1024)
            {
                ModelState.AddModelError("File", "File size must be less than 2 MB.");
                return View(vm);
            }
            Random random = new();
            long randInt = random.Next(1000000, 9999999);
            string fileName = Path.GetFileNameWithoutExtension(vm.Image.FileName) + $"_{randInt}" + Path.GetExtension(vm.Image.FileName);
            using (Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "images", "games", fileName)))
            {
                await vm.Image.CopyToAsync(stream);
            }
            Game game = new Game
            {
                ImgPath = fileName,
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                GameId = vm.GameId,
            };
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Update(GameVM vm, int id)
        {
            if (!ModelState.IsValid) return View(vm);
            Game game = await _context.Games.FindAsync(id);
            game.Name = vm.Name;
            game.Price = vm.Price;
            game.Description = vm.Description;
            game.GameId = vm.GameId;
            game.ImgPath = await vm.Image!.UploadAsync(_env.WebRootPath, "imgs", "sliders");

            _context.Games.Update(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Game target = await _context.Games.FindAsync(id) ?? throw new Exception("Cant find the game!");
            _context.Games.Remove(target);
            string filePath = Path.Combine(Path.Combine(_env.WebRootPath, "images", "games", target.ImgPath));
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception)
            {
                throw (new Exception($"Cant find the image({filePath})"));
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("");
        }
        public async Task<IActionResult> ShowHide(int id)
        {
            Game target = await _context.Games.FindAsync(id) ?? throw new Exception("Cant find the game!");
            target.IsDeleted = target.IsDeleted ? false : true;
            _context.Update(target);
            await _context.SaveChangesAsync();
            return RedirectToAction(string.Empty);
        }
    }
}
