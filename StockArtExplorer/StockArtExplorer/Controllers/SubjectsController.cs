using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using StockArt.Data;

namespace StockArtExplorer.Controllers
{
    public class SubjectsController : Controller
    {
        protected StockArtDBContext _context;

        public SubjectsController(StockArtDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(string id)
        {
            var canonicalImageSet = _context.ImageSets
                .Include(img => img.ImageSetSubjects)
                .ThenInclude(iss => iss.Subject)
                .FirstOrDefault(ims => ims.Name == id);

            // If there is no canonical set with that name, return 404.
            // If there are zero or 2+ subjects in the set, it is not a canonical set. Return 404. 
            if (null == canonicalImageSet || canonicalImageSet.ImageSetSubjects.Count != 1) return NotFound();
            return View(canonicalImageSet);
        }
    }
}
