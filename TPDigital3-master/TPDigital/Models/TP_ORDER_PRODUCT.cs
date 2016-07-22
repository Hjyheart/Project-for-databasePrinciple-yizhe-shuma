namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_ORDER_PRODUCT")]
    public partial class TP_ORDER_PRODUCT
    {
        public decimal ORDER_ID { get; set; }

        public decimal PRODUCT_ID { get; set; }

        public decimal QUANTITY { get; set; }

        public decimal PRICE { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public bool? IS_RETURN { get; set; }

        public virtual TP_ORDER TP_ORDER { get; set; }

        public virtual TP_PRODUCT TP_PRODUCT { get; set; }
    }
}
