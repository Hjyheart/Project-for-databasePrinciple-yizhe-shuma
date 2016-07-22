namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_ACTIVITY_PRODUCT")]
    public partial class TP_ACTIVITY_PRODUCT
    {
        public decimal ACTIVITY_ID { get; set; }

        public decimal PRODUCT_ID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public virtual TP_ACTIVITY TP_ACTIVITY { get; set; }

        public virtual TP_PRODUCT TP_PRODUCT { get; set; }
    }
}
