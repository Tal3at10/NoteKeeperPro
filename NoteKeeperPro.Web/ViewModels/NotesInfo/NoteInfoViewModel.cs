namespace NoteKeeperPro.Web.ViewModels.NotesInfo
{
    public class NoteInfoViewModel
    {
        public int Id { get; set; }

        // The note this info belongs to
        public int NoteId { get; set; }

        // Timestamp of note creation
        public DateTime CreatedAt { get; set; }

        // Timestamp of the last modification of the note
        public DateTime LastModifiedAt { get; set; }

        // Word count in the note
        public int WordCount { get; set; }

        // Character count in the note
        public int CharacterCount { get; set; }

        // Soft delete flag
        public bool IsDeleted { get; set; }
    }
}
