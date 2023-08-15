using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Comment", Schema ="dbo")]
    public class Comment : Common
    {
        [Column("UserComment")]
        public string UserComment { get; set; }
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
        [Column("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
