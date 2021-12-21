namespace Book_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("order")]
    public partial class order
    {
        [StringLength(50)]
        public string orderid { get; set; }

        public int userid { get; set; }

        public DateTime time { get; set; }

        [Required]
        [StringLength(40)]
        public string address { get; set; }

        [Required]
        [StringLength(40)]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        public string phone { get; set; }

        public double totalprice { get; set; }

        [Required]
        [StringLength(100)]
        public string orderurl { get; set; }

        public bool status { get; set; }
    }
}
