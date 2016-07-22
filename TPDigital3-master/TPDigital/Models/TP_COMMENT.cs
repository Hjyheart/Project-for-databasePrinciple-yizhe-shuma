namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_COMMENT")]
    public partial class TP_COMMENT
    {
        public decimal? USER_ID { get; set; }

        public decimal? PRODUCT_ID { get; set; }

        public decimal? ORDER_ID { get; set; }

        [StringLength(150)]
        public string CONTENT { get; set; }

        public DateTime? ADD_DATE { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal? ID { get; set; }

        public decimal? STARS { get; set; }

        public virtual TP_USER TP_USER { get; set; }

        public virtual TP_PRODUCT TP_PRODUCT { get; set; }

        public virtual TP_ORDER TP_ORDER { get; set; }
    }
}
