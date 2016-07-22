namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_RECEIVER")]
    public partial class TP_RECEIVER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TP_RECEIVER()
        {
            TP_ORDER = new HashSet<TP_ORDER>();
            TP_USER1 = new HashSet<TP_USER>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        [Required]
        [StringLength(20)]
        public string NAME { get; set; }

        [Required]
        [StringLength(11)]
        public string PHONE_NUMBER { get; set; }

        public decimal USER_ID { get; set; }

        public decimal LOCATION_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ADDR_DETAIL { get; set; }

        public virtual TP_LOCATION TP_LOCATION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_ORDER> TP_ORDER { get; set; }

        public virtual TP_USER TP_USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_USER> TP_USER1 { get; set; }
    }
}
