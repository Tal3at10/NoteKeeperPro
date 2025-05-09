using Microsoft.AspNetCore.Mvc;
using NoteKeeperPro.Application.Dtos.NotesInfo;
using NoteKeeperPro.Application.Services.NotesInfo;
using NoteKeeperPro.Web.ViewModels.NotesInfo;

namespace NoteKeeperPro.Web.Controllers
{
    public class NoteInfoController : Controller
    {
        #region Services
        private readonly INoteInfoService _noteInfoService;
        private readonly ILogger<NoteInfoController> _logger;
        private readonly IWebHostEnvironment _env;

        public NoteInfoController(INoteInfoService noteInfoService, ILogger<NoteInfoController> logger, IWebHostEnvironment env)
        {
            _noteInfoService = noteInfoService;
            _logger = logger;
            _env = env;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var noteInfos = _noteInfoService.GetAllNoteInfos();
            return View(noteInfos);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            // If you need any extra data, like notes, pass them to the view here
            // ViewData["Notes"] = _noteInfoService.GetAllNotes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoteInfoViewModel noteInfoVM)
        {
            if (!ModelState.IsValid)
                return View(noteInfoVM);

            var message = string.Empty;

            try
            {
                var result = _noteInfoService.CreateNoteInfo(new NoteInfoToCreateDto()
                {
                    //NoteId = noteInfoVM.NoteId,
                    CreatedAt = noteInfoVM.CreatedAt,
                    LastModifiedAt = noteInfoVM.LastModifiedAt,
                    WordCount = noteInfoVM.WordCount,
                    CharchterCount = noteInfoVM.CharacterCount,
                    // IsDeleted can be set to false by default, depending on your logic
                });

                if (result > 0)
                {
                    TempData["Message"] = "New Note Info Created Successfully";
                }
                else
                {
                    message = "Note Info Cannot be Created";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(noteInfoVM);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message);

                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(noteInfoVM);
                }
                else
                {
                    message = "Note Info Cannot be Created";
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

            var noteInfo = _noteInfoService.GetNoteInfoById(id.Value);
            if (noteInfo == null)
                return NotFound(); // error 404

            return View(noteInfo);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest(); // error 400

            var noteInfo = _noteInfoService.GetNoteInfoById(id.Value);
            if (noteInfo == null)
                return NotFound(); // error 404

            return View(new NoteInfoViewModel
            {
                NoteId = noteInfo.NoteId,
                CreatedAt = noteInfo.CreatedAt,
                LastModifiedAt = noteInfo.LastModifiedAt,
                WordCount = noteInfo.WordCount,
                CharacterCount = noteInfo.CharchterCount,
                // IsDeleted is handled in service by default if needed
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NoteInfoViewModel noteInfoVM)
        {
            if (!ModelState.IsValid)
                return View(noteInfoVM);

            var message = string.Empty;

            try
            {
                var result = _noteInfoService.UpdateNoteInfo(new NoteInfoToUpdateDto()
                {
                    Id = id,
                    NoteId = noteInfoVM.NoteId,
                    CreatedAt = noteInfoVM.CreatedAt,
                    LastModifiedAt = noteInfoVM.LastModifiedAt,
                    WordCount = noteInfoVM.WordCount,
                    CharchterCount = noteInfoVM.CharacterCount,
                    // IsDeleted can be handled in the service, too
                });

                if (result > 0)
                {
                    TempData["Message"] = "Note Info Updated Successfully";
                }
                else
                {
                    message = "Note Info Cannot Be Updated";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Note Info Cannot Be Updated";
                return View(noteInfoVM);
            }
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var noteInfo = _noteInfoService.GetNoteInfoById(id.Value);

            if (noteInfo == null) return NotFound();
            return View(noteInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = _noteInfoService.DeleteNoteInfo(id);
            var message = string.Empty;

            try
            {
                if (result)
                {
                    TempData["Message"] = "Note Info Deleted Successfully";
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
