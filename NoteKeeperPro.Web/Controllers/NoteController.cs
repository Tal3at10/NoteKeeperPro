using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteKeeperPro.Application.Dtos.Collaborators;
using NoteKeeperPro.Application.Dtos.Notes;
using NoteKeeperPro.Application.Dtos.Tags;
using NoteKeeperPro.Application.Services.Notes;
using NoteKeeperPro.Web.ViewModels.Collaborators;
using NoteKeeperPro.Web.ViewModels.Notes;

namespace NoteKeeperPro.Web.Controllers
{
    [Authorize]
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
        public IActionResult Index(string SearchValue)
        {
            var notes = _noteService.GetAllNotes(SearchValue);
            return View(notes);
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Notes"] = _noteService.GetAllNotes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoteViewModel noteVM)
        {
            if (!ModelState.IsValid)
                return View(noteVM);

            string message = string.Empty;

            try
            {
                var result = _noteService.CreateNote(new NoteToCreateDto()
                {
                    Title = noteVM.Title,
                    Content = noteVM.Content,
                    Tags = noteVM.TagNames
                        .Select(tag => new TagDetailsToReturnDto { Name = tag })
                        .ToList(),

                    Collaborators = noteVM.Collaborators
                        .Select(c => new CollaboratorDetailsToReturnDto
                        {
                            Id = c.Id,
                            UserName = c.UserName
                        })
                        .ToList()
                });

                if (result > 0)
                {
                    TempData["Message"] = "New Note Created Successfully";
                    return RedirectToAction(nameof(Index));
                }

                message = "Note Cannot be Created";
                TempData["Message"] = message;
                ModelState.AddModelError(string.Empty, message);
                return View(noteVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "Note Cannot be Created";
                return View("Error", message);
            }
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var note = _noteService.GetNoteById(id.Value);
            if (note == null)
                return NotFound();

            return View(note);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var note = _noteService.GetNoteById(id.Value);
            if (note == null)
                return NotFound();

            return View(new NoteViewModel
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                TagNames = note.Tags?.Select(t => t.Name).ToList() ?? new(),
                Collaborators = note.Collaborators?
                    .Select(c => new CollaboratorViewModel
                    {
                        Id = c.Id,
                        UserName = c.UserName
                    }).ToList() ?? new()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NoteViewModel noteVM)
        {
            if (!ModelState.IsValid)
                return View(noteVM);

            string message = string.Empty;

            try
            {
                var result = _noteService.UpdateNote(new NoteToUpdateDto()
                {
                    Id = id,
                    Title = noteVM.Title,
                    Content = noteVM.Content,
                    Tags = noteVM.TagNames
                        .Select(tag => new TagDetailsToReturnDto { Name = tag })
                        .ToList(),

                    Collaborators = noteVM.Collaborators
                        .Select(c => new CollaboratorDetailsToReturnDto
                        {
                            Id = c.Id,
                            UserName = c.UserName
                        })
                        .ToList()
                });

                if (result > 0)
                {
                    TempData["Message"] = "Note Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }

                message = "Note Cannot Be Updated";
                TempData["Message"] = message;
                ModelState.AddModelError(string.Empty, message);
                return View(noteVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "Note Cannot Be Updated";
                TempData["Message"] = message;
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
            if (note == null)
                return NotFound();

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            string message = string.Empty;

            try
            {
                var result = _noteService.DeleteNote(id);
                if (result)
                {
                    TempData["Message"] = "Note Deleted Successfully";
                    return RedirectToAction(nameof(Index));
                }

                message = "An error occurred while deleting the note.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "An error occurred while deleting the note.";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));
        }
        #endregion
    }
}
