namespace Book_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feedback")]
    public partial class feedback
    {
        public int feedbackid { get; set; }

        public int userid { get; set; }

        [Required]
        [StringLength(200)]
        public string substance { get; set; }
        public DateTime time { get; set; }
    }
}
