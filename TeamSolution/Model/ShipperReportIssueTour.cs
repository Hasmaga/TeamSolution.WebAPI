using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("ShipperReportIssueTour")]
    public class ShipperReportIssueTour : Common
    {
        [Column("TourId")]
        public Guid TourId { get; set; }
        public TourShipper Tour { get; set; } = null!;

        [Column("MainIssue")]
        public string MainIssue { get; set; } = null!;

        [Column("Description")]
        public string Description { get; set; } = null!;

        [Column("CloseDateTime")]
        public DateTime? CloseDateTime { get; set; } = null;    
    }
}
