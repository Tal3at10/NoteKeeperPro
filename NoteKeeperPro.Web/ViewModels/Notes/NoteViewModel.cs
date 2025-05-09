using NoteKeeperPro.Web.ViewModels.Collaborators;

namespace NoteKeeperPro.Web.ViewModels.Notes
{
    public class NoteViewModel
    {
        public int Id { get; set; }

        // Title of the note
        public string Title { get; set; } = string.Empty;

        // Content of the note
        public string Content { get; set; } = string.Empty;

        // Timestamp of note creation
        public DateTime CreatedAt { get; set; }

        // Timestamp of last update to the note
        public DateTime UpdatedAt { get; set; }

        // Soft delete flag
        public bool IsDeleted { get; set; }

        // The owner of the note
        public string OwnerId { get; set; } = string.Empty;

        // Navigation to the owner user (for display purposes)
        public string OwnerName { get; set; } = string.Empty;

        // Navigation to metadata info (e.g., word count)
        public string NoteInfo { get; set; } = string.Empty; // Assuming you want to display some string info here

        // List of collaborators on the note
        public List<CollaboratorViewModel> Collaborators { get; set; } = new List<CollaboratorViewModel>();

        // List of tags associated with the note
        public List<string> TagNames { get; set; } = new List<string>(); // Only tag names, assuming the view just needs names
    }
}
