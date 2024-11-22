using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class MoviesController : MvcController
    {
        // Service injections:
        private readonly IMoviesService _movieService;
        private readonly IDirectorService _directorService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public MoviesController(
			IMoviesService movieService
            , IDirectorService directorService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _movieService = movieService;
            _directorService = directorService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Movies
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _movieService.Query().ToList();
            return View(list);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            //ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Record.Id", "Name");
            ViewData["DirectorId"] = new SelectList(_directorService.Query().Select(director => new { Id = director.Record.Id, FullName = director.Record.Name + " " + director.Record.Surname}).ToList(), "Id", "FullName");
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MoviesModel movie)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _movieService.Create(movie.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movie.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Movies/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MoviesModel movie)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _movieService.Update(movie.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movie.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _movieService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
