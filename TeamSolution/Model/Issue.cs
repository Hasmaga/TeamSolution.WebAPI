using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Issue")]
    public class Issue : Common
    {
        [Column("AccountIssueId")]
        public Guid AccountIssueId { get; set; }
        public Account AccountIssue { get; set; }

        [Column("AccountFixIssueId")]
        public Guid AccountFixIssueId { get; set; }
        public Account AccountFixIssue { get; set; }

        [Column("IssueTitle")]
        public string IssueTitle { get; set; }

        [Column("MainIssus")]
        public string MainIssus { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("IsClose")]
        public bool IsClose { get; set; } = false;

        [Column("IsCloseDateTime")]
        public DateTime? IsCloseDateTime { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; }

        [Column("DeleteDateTime")]
        public DateTime? DeleteDateTime { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Issue(Guid accountIssueId, Guid accountFixIssueId, string issueTitle, string mainIssus, string description, bool isClose, DateTime? isCloseDateTime, bool isDelete, DateTime createDateTime, DateTime? updateDateTime, DateTime? deleteDateTime)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            AccountIssueId = accountIssueId;
            AccountFixIssueId = accountFixIssueId;
            IssueTitle = issueTitle;
            MainIssus = mainIssus;
            Description = description;
            IsClose = isClose;
            IsCloseDateTime = isCloseDateTime;
            IsDelete = isDelete;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            DeleteDateTime = deleteDateTime;
        }
    }
}
// Co the them IssueType, IssuePriority, IssueStatus,
// IssueCategory, IssueTag, IssueComment, IssueAttachment,
// IssueHistory, IssueNotification, IssueActivity va IssueMessage