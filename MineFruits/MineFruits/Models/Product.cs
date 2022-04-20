namespace MineFruits.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [DisplayName("Tên sản phẩm")]
        [StringLength(200)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Trọng lượng không được để trống")]
        [DisplayName("Trọng lượng")]
        [StringLength(100)]
        public string Weight { get; set; }

        [Required(ErrorMessage = "Xuất xứ không được để trống")]
        [DisplayName("Xuất xứ")]
        [StringLength(100)]
        public string Origin { get; set; }

        //[Required(ErrorMessage = "Mô tả không được để trống")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [DisplayName("Giá")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Giá sản phẩm phải là một số.")]
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        [DisplayName("Hình Ảnh")]
        [StringLength(100)]
        public string Image { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [DisplayName("Số Lượng")]
        public int? Amount { get; set; }

        [DisplayName("Khuyến mại")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Phần trăm khuyến mại phải là một số.")]
        public double? Promotion { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int CategoryID { get; set; }

        public int BrandID { get; set; }

        public virtual Brand Brand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual Category Category { get; set; }
    }
}
