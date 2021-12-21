namespace Book_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("orderbook")]
    public partial class orderbook
    {
        public int bookid { get; set; }
        public string imgUrl { get; set; }
        public string bookname { get; set; }
        public double price { get; set; }
        public int counts { get; set; }
        public double totalprice { get; set; }
    }
}
