namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_USER")]
    public partial class TP_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TP_USER()
        {
            TP_COMMENT = new HashSet<TP_COMMENT>();
            TP_EXCHANGE = new HashSet<TP_EXCHANGE>();
            TP_FAVORITE = new HashSet<TP_FAVORITE>();
            TP_ORDER = new HashSet<TP_ORDER>();
            TP_RECEIVER = new HashSet<TP_RECEIVER>();
            TP_RETURN = new HashSet<TP_RETURN>();
            TP_SHOPPING_CART = new HashSet<TP_SHOPPING_CART>();
            TP_USER_ROLE = new HashSet<TP_USER_ROLE>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        [Required]
        [StringLength(20)]
        public string NAME { get; set; }

        [Required]
        [StringLength(10)]
        public string SALT { get; set; }

        [Required]
        [StringLength(30)]
        public string PASSWORD { get; set; }

        [Required]
        [StringLength(11)]
        public string PHONE_NUMBER { get; set; }

        [Required]
        [StringLength(40)]
        public string EMAIL { get; set; }

        public decimal? ICON_ID { get; set; }

        public decimal? DEFAULT_RECEIVER_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_COMMENT> TP_COMMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_EXCHANGE> TP_EXCHANGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_FAVORITE> TP_FAVORITE { get; set; }

        public virtual TP_IMAGE TP_IMAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_ORDER> TP_ORDER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_RECEIVER> TP_RECEIVER { get; set; }

        public virtual TP_RECEIVER TP_RECEIVER1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_RETURN> TP_RETURN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_SHOPPING_CART> TP_SHOPPING_CART { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_USER_ROLE> TP_USER_ROLE { get; set; }
    }
}
