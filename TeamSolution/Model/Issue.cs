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
        public Account? AccountIssue { get; set; }

        [Column("AccountFixIssueId")]
        public Guid AccountFixIssueId { get; set; }
        public Account? AccountFixIssue { get; set; }

        [Column("IssueTitle")]
        public string IssueTitle { get; set; } = null!;

        [Column("MainIssus")]
        public string MainIssus { get; set; } = null!;

        [Column("Description")]
        public string Description { get; set; } = null!;

        [Column("IsClose")]
        public bool IsClose { get; set; } = false;

        [Column("IsCloseDateTime")]
        public DateTime? IsCloseDateTime { get; set; }       
    }
}
// Co the them IssueType, IssuePriority, IssueStatus,
// IssueCategory, IssueTag, IssueComment, IssueAttachment,
// IssueHistory, IssueNotification, IssueActivity va IssueMessage