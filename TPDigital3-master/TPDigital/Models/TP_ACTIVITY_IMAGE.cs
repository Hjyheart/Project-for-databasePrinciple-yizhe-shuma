namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_ACTIVITY_IMAGE")]
    public partial class TP_ACTIVITY_IMAGE
    {
        public decimal? ACTIVITY_ID { get; set; }

        public decimal? IMAGE_ID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public virtual TP_ACTIVITY TP_ACTIVITY { get; set; }

        public virtual TP_IMAGE TP_IMAGE { get; set; }
    }
}
