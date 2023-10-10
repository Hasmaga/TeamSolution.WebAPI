using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("ShipperReportIssueTour")]
    public class ShipperReportIssueTour : Common
    {
        [Column("TourId")]
        public Guid TourId { get; set; }
        public TourShipper Tour { get; set; }

        [Column("MainIssue")]
        public string MainIssue { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; }

        [Column("IsClose")]
        public bool IsClose { get; set; } = false;

        [Column("IsCloseDateTime")]
        public DateTime? IsCloseDateTime { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ShipperReportIssueTour(Guid tourId, string mainIssue, string description, bool isDelete, DateTime createDateTime, DateTime? updateDateTime, bool isClose, DateTime? isCloseDateTime)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            TourId = tourId;
            MainIssue = mainIssue;
            Description = description;
            IsDelete = isDelete;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            IsClose = isClose;
            IsCloseDateTime = isCloseDateTime;
        }
    }
}
