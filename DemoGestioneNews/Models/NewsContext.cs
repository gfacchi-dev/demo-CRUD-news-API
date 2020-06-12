namespace DemoGestioneNews.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewsContext : DbContext
    {
        public NewsContext()
            : base("name=NewsContext")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<News> News { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.News)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.FKAuthor)
                .WillCascadeOnDelete(false);
        }
    }
}
