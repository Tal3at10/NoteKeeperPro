using Microsoft.AspNetCore.Mvc;
using NoteKeeperPro.Application.Dtos.Notes;
using NoteKeeperPro.Application.Services.Notes;
using NoteKeeperPro.Web.ViewModels.Notes;

namespace NoteKeeperPro.Web.Controllers
{
    public class NoteController : Controller
    {
        #region Services
        private readonly INoteService _noteService;
        private readonly ILogger<NoteController> _logger;
        private readonly IWebHostEnvironment _env;

        public NoteController(INoteService noteService, ILogger<NoteController> logger, IWebHostEnvironment env)
        {
            _noteService = noteService;
            _logger = logger;
            _env = env;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var notes = _noteService.GetAllNotes();
            return View(notes);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            // If you need any extra data, like collaborators or tags, pass them to the view here
            // ViewData["Collaborators"] = _noteService.GetAllCollaborators();
            // ViewData["Tags"] = _noteService.GetAllTags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoteViewModel noteVM)
        {
            if (!ModelState.IsValid)
                return View(noteVM);

            var message = string.Empty;

            try
            {
                var result = _noteService.CreateNote(new NoteToCreateDto()
                {
                    Title = noteVM.Title,
                    Content = noteVM.Content,
                    //OwnerId = noteVM.OwnerId,
                    // Add other properties if necessary (e.g., collaborators, tags)
                });

                if (result > 0)
                {
                    TempData["Message"] = "New Note Created Successfully";
                }
                else
                {
                    message = "Note Cannot be Created";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(noteVM);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message);

                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(noteVM);
                }
                else
                {
                    message = "Note Cannot be Created";
                    return View("Error", message);
                }
            }
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest(); // error 400

            var note = _noteService.GetNoteById(id.Value);
            if (note == null)
                return NotFound(); // error 404

            return View(note);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest(); // error 400

            var note = _noteService.GetNoteById(id.Value);
            if (note == null)
                return NotFound(); // error 404

            return View(new NoteViewModel
            {
                Title = note.Title,
                Content = note.Content,
                //OwnerName = note.OwnerName,
                // Map other properties (e.g., collaborators, tags) if necessary
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NoteViewModel noteVM)
        {
            if (!ModelState.IsValid)
                return View(noteVM);

            var message = string.Empty;

            try
            {
                var result = _noteService.UpdateNote(new NoteToUpdateDto()
                {
                    Id = id,
                    Title = noteVM.Title,
                    Content = noteVM.Content,
                    //OwnerId = noteVM.OwnerId,
                    // Add other properties if necessary
                });

                if (result > 0)
                {
                    TempData["Message"] = "Note Updated Successfully";
                }
                else
                {
                    message = "Note Cannot Be Updated";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Note Cannot Be Updated";
                return View(noteVM);
            }
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var note = _noteService.GetNoteById(id.Value);

            if (note == null) return NotFound();
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = _noteService.DeleteNote(id);
            var message = string.Empty;

            try
            {
                if (result)
                {
                    TempData["Message"] = "Note Deleted Successfully";
                    return RedirectToAction(nameof(Index));
                }
                message = "An Error Happened while Deleting";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "An Error Happened while Deleting";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }
        #endregion
    }

}
