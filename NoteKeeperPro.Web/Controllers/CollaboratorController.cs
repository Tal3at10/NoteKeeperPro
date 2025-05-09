using Microsoft.AspNetCore.Mvc;
using NoteKeeperPro.Application.Dtos.Collaborators;
using NoteKeeperPro.Application.Services.Collaborators;
using NoteKeeperPro.Web.ViewModels.Collaborators;

namespace NoteKeeperPro.Web.Controllers
{
    public class CollaboratorController : Controller
    {
        #region Services
        private readonly ICollaboratorService _collaboratorService;
        private readonly ILogger<CollaboratorController> _logger;
        private readonly IWebHostEnvironment _env;

        public CollaboratorController(ICollaboratorService collaboratorService, ILogger<CollaboratorController> logger, IWebHostEnvironment env)
        {
            _collaboratorService = collaboratorService;
            _logger = logger;
            _env = env;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var collaborators = _collaboratorService.GetAllCollaborators();
            return View(collaborators);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            // Send Notes from action to view (if necessary, to choose the note to collaborate on)
            // ViewData["Notes"] = _collaboratorService.GetAllNotes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CollaboratorViewModel collaboratorVM)
        {
            if (!ModelState.IsValid)
                return View(collaboratorVM);
            var message = string.Empty;

            try
            {
                var result = _collaboratorService.CreateCollaborator(new CollaboratorToCreateDto()
                {
                    NoteId = collaboratorVM.NoteId,
                    UserId = collaboratorVM.UserId,
                   // PermissionType = collaboratorVM.PermissionType,
                });

                if (result > 0)
                {
                    TempData["Message"] = "New Collaborator Created Successfully";
                }
                else
                {
                    message = "Collaborator Cannot be Created";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(collaboratorVM);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message);

                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(collaboratorVM);
                }
                else
                {
                    message = "Collaborator Cannot be Created";
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

            var collaborator = _collaboratorService.GetCollaboratorById(id.Value);
            if (collaborator == null)
                return NotFound(); // error 404

            return View(collaborator);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest(); // error 400

            var collaborator = _collaboratorService.GetCollaboratorById(id.Value);
            if (collaborator == null)
                return NotFound(); // error 404

            return View(new CollaboratorViewModel
            {
                NoteId = collaborator.NoteId,
                UserId = collaborator.UserId,
             //  PermissionType = collaborator.PermissionType,

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CollaboratorViewModel collaboratorVM)
        {
            if (!ModelState.IsValid)
                return View(collaboratorVM);
            var message = string.Empty;

            try
            {
                var result = _collaboratorService.UpdateCollaborator(new CollaboratorToUpdateDto()
                {
                    Id = id,
                   // PermissionType = collaboratorVM.PermissionType,
                });

                if (result > 0)
                {
                    TempData["Message"] = "Collaborator Updated Successfully";
                }
                else
                {
                    message = "Collaborator Cannot Be Updated";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Collaborator Cannot Be Updated";
            }
            return View(collaboratorVM);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var collaborator = _collaboratorService.GetCollaboratorById(id.Value);

            if (collaborator == null) return NotFound();
            return View(collaborator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = _collaboratorService.DeleteCollaborator(id);
            var message = string.Empty;

            try
            {
                if (result)
                {
                    TempData["Message"] = "Collaborator Deleted Successfully";
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
