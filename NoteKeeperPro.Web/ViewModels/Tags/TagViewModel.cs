namespace NoteKeeperPro.Web.ViewModels.Tags
{
    public class TagViewModel
    {
        public int Id { get; set; }

        // Name of the tag
        public string Name { get; set; } = string.Empty;

        // Soft delete flag
        public bool IsDeleted { get; set; } = false;

        // List of notes associated with this tag
        public ICollection<int> NoteIds { get; set; } = new List<int>();
    }
}
