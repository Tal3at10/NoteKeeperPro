using System.ComponentModel.DataAnnotations;
using NoteKeeperPro.Web.ViewModels.Collaborators;

namespace NoteKeeperPro.Web.ViewModels.Notes
{
    public class NoteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "CreatedAt is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "UpdatedAt is required.")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "OwnerId is required.")]
        public string OwnerId { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string NoteInfo { get; set; } = string.Empty;

        public List<CollaboratorViewModel> Collaborators { get; set; } = new();

        public List<string> TagNames { get; set; } = new();
    }
}
