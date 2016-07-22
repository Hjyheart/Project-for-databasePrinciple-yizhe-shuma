namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_EXCHANGE")]
    public partial class TP_EXCHANGE
    {
        public decimal ORDER_ID { get; set; }

        public decimal PRODUCT_ID { get; set; }

        public decimal USER_ID { get; set; }

        public decimal QUANTITY { get; set; }

        public decimal PACKAGE_STATE { get; set; }

        public decimal? EXPRESS_ID { get; set; }

        public DateTime APPLY_DATE { get; set; }

        public decimal? RETURN_EXPRESS_ID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public virtual TP_ORDER TP_ORDER { get; set; }

        public virtual TP_PRODUCT TP_PRODUCT { get; set; }

        public virtual TP_USER TP_USER { get; set; }

        public virtual TP_PACKAGE_STATE TP_PACKAGE_STATE { get; set; }

        public virtual TP_EXPRESS TP_EXPRESS { get; set; }

        public virtual TP_EXPRESS TP_EXPRESS1 { get; set; }
    }
}
