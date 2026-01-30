using System.Data.Entity;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA
{
    public class TestDbContext : DbContext
    {
        public TestDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<ClientDog> ClientDogs { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .ToTable("Clients")
                .HasKey(c => c.Id);

            modelBuilder.Entity<Client>()
                .Property(c => c.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Dog>()
                .ToTable("Dogs")
                .HasKey(d => d.Id);

            modelBuilder.Entity<Dog>()
                .Property(d => d.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ClientDog>()
                .ToTable("ClientDogs")
                .HasKey(cd => new { cd.ClientId, cd.DogId });

            modelBuilder.Entity<ClientDog>()
                .HasRequired<Client>(cd => cd.Client)
                .WithMany()
                .HasForeignKey(cd => cd.ClientId);

            modelBuilder.Entity<ClientDog>()
                .HasRequired<Dog>(cd => cd.Dog)
                .WithMany()
                .HasForeignKey(cd => cd.DogId);

            // Walk configuration
            modelBuilder.Entity<Walk>()
                .ToTable("Walks")
                .HasKey(w => w.Id);

            modelBuilder.Entity<Walk>()
                .Property(w => w.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Walk>()
                .HasRequired<Client>(w => w.Client)
                .WithMany()
                .HasForeignKey(w => w.ClientId);

            modelBuilder.Entity<Walk>()
                .HasRequired<Dog>(w => w.Dog)
                .WithMany()
                .HasForeignKey(w => w.DogId);

            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.CreateDate)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
