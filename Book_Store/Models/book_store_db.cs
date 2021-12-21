namespace Book_Store.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class book_store_db : DbContext
    {
        public book_store_db()
            : base("name=book_store_db2")
        {
        }

        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<book> book { get; set; }
        public virtual DbSet<feedback> feedback { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<user> user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admin>()
                .Property(e => e.adminname)
                .IsFixedLength();

            modelBuilder.Entity<admin>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<book>()
                .Property(e => e.bookname)
                .IsFixedLength();

            modelBuilder.Entity<book>()
                .Property(e => e.synopsis)
                .IsFixedLength();

            modelBuilder.Entity<book>()
                .Property(e => e.image_url)
                .IsFixedLength();

            modelBuilder.Entity<book>()
                .Property(e => e.type)
                .IsFixedLength();

            modelBuilder.Entity<feedback>()
                .Property(e => e.substance)
                .IsFixedLength();

            modelBuilder.Entity<order>()
                .Property(e => e.orderid)
                .IsFixedLength();

            modelBuilder.Entity<order>()
                .Property(e => e.address)
                .IsFixedLength();

            modelBuilder.Entity<order>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<order>()
                .Property(e => e.phone)
                .IsFixedLength();

            modelBuilder.Entity<order>()
                .Property(e => e.orderurl)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.phone)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.useraddress)
                .IsFixedLength();
        }
    }
}
