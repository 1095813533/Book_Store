namespace Book_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("book")]
    public partial class book
    {
        public int bookid
        {
            get;
            set;
        }

        [Required]
        [StringLength(20)]
        public string bookname
        {
            get;
            set;
        }

        [Required]
        [StringLength(200)]
        public string synopsis
        {
            get;
            set; }

        [Required]
        [StringLength(200)]
        public string image_url
        {
            get;
            set; }

        public int stock
        {
            get;
            set; }

        public double price
        {
            get;
            set; }

        [Required]
        [StringLength(20)]
        public string type
        {
            get;
            set; }

        public int salenum
        {
            get;
            set;
        }
    }
}
