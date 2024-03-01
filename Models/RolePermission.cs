using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class RolePermission
    {
        public int id {  get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsDelete { get; set; }
        public bool IsIdit { get; set; }
        [ForeignKey("role")]
        public int? Role_Id { get; set; }
        public virtual Role? role{ get; set; }
        [ForeignKey("page")]
        public int? Page_Id {  get; set; }
        public virtual Page? page { get; set; }

    }
}
