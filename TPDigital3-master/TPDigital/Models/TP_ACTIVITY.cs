namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_ACTIVITY")]
    public partial class TP_ACTIVITY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TP_ACTIVITY()
        {
            TP_ACTIVITY_PRODUCT = new HashSet<TP_ACTIVITY_PRODUCT>();
            TP_ACTIVITY_IMAGE = new HashSet<TP_ACTIVITY_IMAGE>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        [Required]
        [StringLength(40)]
        public string NAME { get; set; }

        [StringLength(200)]
        public string DESCRIPTION { get; set; }

        public DateTime START_TIME { get; set; }

        public DateTime END_TIME { get; set; }

        public decimal DISCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_ACTIVITY_PRODUCT> TP_ACTIVITY_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TP_ACTIVITY_IMAGE> TP_ACTIVITY_IMAGE { get; set; }
    }
}
