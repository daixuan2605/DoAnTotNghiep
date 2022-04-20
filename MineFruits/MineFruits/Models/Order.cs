namespace MineFruits.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        [DisplayName("Mã hóa đơn")]
        public int OrderID { get; set; }

        [DisplayName("Mã giỏ hàng")]
        public int CartID { get; set; }

        
        [StringLength(100)]
        [DisplayName("Trạng thái")]
        public string Status { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        [DisplayName("Phí vận chuyển")]
        public decimal? FeeShip { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Ghi chú")]
        public string Note { get; set; }

        [StringLength(255)]
        [DisplayName("Địa chỉ nhận hàng")]
        public string Address { get; set; }

        [DisplayName("Ngày đặt")]
        public DateTime? CreateDate { get; set; }

        [DisplayName("Ngày cập nhật")]
        public DateTime? UpdateDate { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
