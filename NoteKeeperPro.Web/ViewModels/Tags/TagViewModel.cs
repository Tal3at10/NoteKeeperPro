using System.ComponentModel.DataAnnotations;

namespace NoteKeeperPro.Web.ViewModels.Tags
{
    public class TagViewModel
    {
        public int Id { get; set; }

        // اسم التاج
        [Required(ErrorMessage = "اسم التاج مطلوب")]
        [StringLength(100, ErrorMessage = "اسم التاج يجب أن يكون أقل من 100 حرف.")]
        public string Name { get; set; } = string.Empty;

        // علامة الحذف الناعم
        public bool IsDeleted { get; set; } = false;

        // قائمة الملاحظات المرتبطة بهذا التاج (سنضع فقط الـ IDs هنا)
        public ICollection<int> NoteIds { get; set; } = new List<int>();
    }
}
