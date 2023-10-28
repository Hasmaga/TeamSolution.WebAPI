using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamSolution.Model.Abstract
{
    public class Common
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        [Column("DeleteDataTime")]
        public DateTime? DeleteDateTime { get; set; } = null;

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; } = null;

    }
}
