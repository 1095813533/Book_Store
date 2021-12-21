namespace Book_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("car")]
    public partial class shoppingcaritem
    {
        public int id { get; set; }
        public string imgUrl { get; set; }
        public string goodsInfo { get; set; }
        public double price { get; set; }
        public int goodsCount { get; set; }
        public double singleGoodsMoney { get; set; }
    }
}
