using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("StoreModeSeting", Schema ="dbo")]
    public class StoreModeSetting : Common
    {
        [Column("SuperFastWeightIn1Kg")]
        public decimal SuperFastWeightIn1Kg { get; set; }

        [Column("FastWeightIn1Kg")]
        public decimal FastWeightIn1Kg { get; set; }

        [Column("NormalWeightIn1Kg")]
        public decimal NormalWeightIn1Kg { get; set; }  
        
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Store> Stores { get; set; }

        public StoreModeSetting(decimal superFastWeightIn1Kg, decimal fastWeightIn1Kg, decimal normalWeightIn1Kg)
        {
            SuperFastWeightIn1Kg = superFastWeightIn1Kg;
            FastWeightIn1Kg = fastWeightIn1Kg;
            NormalWeightIn1Kg = normalWeightIn1Kg;
        }
    }
}
