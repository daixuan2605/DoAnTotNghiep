namespace MineFruits.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Carts = new HashSet<Cart>();
            News = new HashSet<News>();
        }

        [Key]
        [StringLength(100)]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [MaxLength(20, ErrorMessage = "Tên đăng nhập phải có tối đa 20 ký tự")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có tối thiểu 3 ký tự")]
        [DisplayName("Tên tài khoản")]
        public string Username { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MaxLength(20, ErrorMessage = "Mật khẩu phải có tối đa 20 ký tự")]
        [MinLength(3, ErrorMessage = "Mật khẩu nhập phải có tối thiểu 3 ký tự")]
        [DisplayName("Mật khẩu"), DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [DisplayName("Địa chỉ email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email cần nhập đúng định dạng. VD: example@gmail.com")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [MaxLength(30, ErrorMessage = "Mật khẩu phải có tối đa 30 ký tự")]
        [DisplayName("Họ và tên")]
        public string Name { get; set; }

        [StringLength(200)]
        [DisplayName("Địa chỉ")]
        [MaxLength(100, ErrorMessage = "Địa chỉ phải có tối đa 100 ký tự")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Address { get; set; }

        [DisplayName("Quyền")]
        public int? Power { get; set; }

        [DisplayName("Trạng thái")]
        [UIHint("Boolean")]
        public bool? Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }
    }
}
