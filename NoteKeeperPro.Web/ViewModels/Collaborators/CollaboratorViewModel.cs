namespace NoteKeeperPro.Web.ViewModels.Collaborators
{
    

    public class CollaboratorViewModel
    {
       
        public int Id { get; set; }

        // FK to the shared note
        public int NoteId { get; set; }

        // Navigation to the note being shared (for display purposes)
        public string NoteTitle { get; set; } = string.Empty; // Assuming the note has a title for display

        // FK to the collaborating user
        public string UserId { get; set; } = string.Empty;

        // Navigation to the collaborating user (for display purposes)
        public string UserName { get; set; } = string.Empty; // Assuming user has a username or similar property

        // Access level of the collaborator (Read, Write, etc.)
        public string? PermissionType { get; set; }

        // Soft delete flag for collaboration (e.g., revoked access)
        public bool IsDeleted { get; set; } = false;

     
        
    }
}
