namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_PRODUCT")]
    public partial class TP_PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TP_PRODUCT()
        {
            TP_ACTIVITY_PRODUCT = new HashSet<TP_ACTIVITY_PRODUCT>();
            TP_COMMENT = new HashSet<TP_COMMENT>();
            TP_EXCHANGE = new HashSet<TP_EXCHANGE>();
            TP_FAVORITE = new HashSet<TP_FAVORITE>();
            TP_ORDER_PRODUCT = new HashSet<TP_ORDER_PRODUCT>();
            TP_PRODUCT_IMAGE = new HashSet<TP_PRODUCT_IMAGE>();
            TP_RETURN = new HashSet<TP_RETURN>();
            TP_SHOPPING_CART = new HashSet<TP_SHOPPING_CART>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public decimal? CATEGORY_ID { get; set; }

        [Required]
        [StringLength(40)]
        public string NAME { get; set; }

        [StringLength(150)]
        public string DESCRIPTION { get; set; }

        public decimal PRICE { get; set; }

        public DateTime ADD_DATE { get; set; }

        public decimal INVENTORY { get; set; }

        public string DETAILS { get; set; }

        [StringLength(30)]
        public string BRAND_NAME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_ACTIVITY_PRODUCT> TP_ACTIVITY_PRODUCT { get; set; }

        public virtual TP_CATEGORY TP_CATEGORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_COMMENT> TP_COMMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_EXCHANGE> TP_EXCHANGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_FAVORITE> TP_FAVORITE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_ORDER_PRODUCT> TP_ORDER_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_PRODUCT_IMAGE> TP_PRODUCT_IMAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_RETURN> TP_RETURN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_SHOPPING_CART> TP_SHOPPING_CART { get; set; }
    }
}
