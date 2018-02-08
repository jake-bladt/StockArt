using System;
using System.Collections.Generic;
using io = System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using StockArt.Data;
using StockArt.Domain;

namespace StockArtExplorer.Controllers
{
    public class YearbookImagesController : Controller
    {
        protected StockArtDBContext _context;

        public YearbookImagesController(StockArtDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(string id)
        {
            var imageSet = _context.CanonicalImageSet(id);
            var subject = imageSet.ImageSetSubjects.FirstOrDefault()?.Subject;

            if(null == subject || String.IsNullOrEmpty(subject.CatalogImagePath) || !io.File.Exists(subject.CatalogImagePath))
            {
                return NotFound();
            }
            else
            {
                var stream = new io.FileStream(subject.CatalogImagePath, io.FileMode.Open);
                return File(stream, "image/jpeg");
            }
        }
    }
}
