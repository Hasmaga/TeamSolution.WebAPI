using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("FeedBack", Schema = "dbo")]
    public class FeedBack : Common
    {
        [Column("Rating")]
        public int Rating { get; set; } 

        [Column("Comment")]        
        public string Comment { get; set; } = null!;
        
        [Column("CustomerId")]
        public Guid CustomerId { get; set; }
        public Account? Customer { get; set; }        

        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store? Store { get; set; }        
    }
}
