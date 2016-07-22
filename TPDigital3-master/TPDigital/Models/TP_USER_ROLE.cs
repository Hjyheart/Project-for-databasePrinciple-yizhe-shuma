namespace TPDigital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("C##COM.TP_USER_ROLE")]
    public partial class TP_USER_ROLE
    {
        public decimal USER_ID { get; set; }

        public decimal ROLE_ID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }

        public virtual TP_ROLE TP_ROLE { get; set; }

        public virtual TP_USER TP_USER { get; set; }
    }
}
