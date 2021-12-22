namespace Book_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin")]
    public partial class admin
    {
        public int adminid
        {
            get;
            set;
        }

        [Required]
        [StringLength(20)]
        public string adminname
        {
            get;
            set;
        }

        [Required]
        [StringLength(20)]
        public string password
        {
            get;
            set;
        }
    }
}
