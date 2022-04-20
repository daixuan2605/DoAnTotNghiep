namespace MineFruits.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CartID { get; set; }

        [DisplayName("Giá")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Giá sản phẩm phải là một số.")]
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal? Price { get; set; }

        public int? Amount { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }
    }
}
