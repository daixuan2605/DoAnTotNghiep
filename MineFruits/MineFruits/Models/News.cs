namespace MineFruits.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        public int NewsID { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [DisplayName("Tiêu đề")]
        [StringLength(255)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mô tả ngắn không được để trống")]
        [DisplayName("Mô tả ngắn")]
        [StringLength(300)]
        public string ShortDescription { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Mô tả chi tiết không được để trống")]
        [DisplayName("Mô tả chi tiết")]
        public string DetailDescription { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        [DisplayName("Hình ảnh")]
        [StringLength(100)]
        public string Image { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        public virtual Account Account { get; set; }
    }
}
