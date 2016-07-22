namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_ORDER")]
    public partial class TP_ORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TP_ORDER()
        {
            TP_COMMENT = new HashSet<TP_COMMENT>();
            TP_EXCHANGE = new HashSet<TP_EXCHANGE>();
            TP_ORDER_PRODUCT = new HashSet<TP_ORDER_PRODUCT>();
            TP_RETURN = new HashSet<TP_RETURN>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public decimal USER_ID { get; set; }

        public decimal SHIPPING_RECEIVER_ID { get; set; }

        public decimal? EXPRESS_ID { get; set; }

        public decimal PACKAGE_STATE_ID { get; set; }

        public decimal PAYMENT_STATE_ID { get; set; }

        public decimal TOTAL_PRICE { get; set; }

        public DateTime ADD_DATE { get; set; }

        public DateTime? SIGN_TIME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_COMMENT> TP_COMMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_EXCHANGE> TP_EXCHANGE { get; set; }

        public virtual TP_EXPRESS TP_EXPRESS { get; set; }

        public virtual TP_USER TP_USER { get; set; }

        public virtual TP_RECEIVER TP_RECEIVER { get; set; }

        public virtual TP_PACKAGE_STATE TP_PACKAGE_STATE { get; set; }

        public virtual TP_PAYMENT_STATE TP_PAYMENT_STATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_ORDER_PRODUCT> TP_ORDER_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_RETURN> TP_RETURN { get; set; }
    }
}
