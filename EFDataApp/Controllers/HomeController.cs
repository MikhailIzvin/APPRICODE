using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFDataApp.Models; 

namespace EFDataApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.VideoGames.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(VideoGame videoGame)
        {
            db.VideoGames.Add(videoGame);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                VideoGame videoGame = await db.VideoGames.FirstOrDefaultAsync(p => p.ID == id);
                if (videoGame != null)
                    return View(videoGame);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                VideoGame videoGame = await db.VideoGames.FirstOrDefaultAsync(p => p.ID == id);
                if (videoGame != null)
                    return View(videoGame);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(VideoGame videoGame)
        {
            db.VideoGames.Update(videoGame);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                VideoGame videoGame = await db.VideoGames.FirstOrDefaultAsync(p => p.ID == id);
                if (videoGame != null)
                    return View(videoGame);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                VideoGame videoGame = new VideoGame { ID = id.Value };
                db.Entry(videoGame).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}