namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_PRODUCT_IMAGE")]
    public partial class TP_PRODUCT_IMAGE
    {
        public decimal? IMAGE_ID { get; set; }

        public decimal PRODUCT_ID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public virtual TP_IMAGE TP_IMAGE { get; set; }

        public virtual TP_PRODUCT TP_PRODUCT { get; set; }
    }
}
