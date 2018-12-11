namespace ChessAPI_LocalDB_.Models
{
    using System.Data.Entity;
    public partial class ChessContext : DbContext
    {
        public ChessContext() : base("name=ChessContextDB") { }
        public virtual DbSet<Game> Games { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .Property(e => e.FEN)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.Status)
                .IsUnicode(false);
        }
    }
}
