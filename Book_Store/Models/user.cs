namespace Book_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userid { get; set; }

        [Key]
        [StringLength(20)]
        public string username { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        [Required]
        [StringLength(20)]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(40)]
        public string useraddress { get; set; }
    }
}
