using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace WebRJP.Models
{

    public partial class RJPContext : IdentityDbContext
    {
        IConfiguration _configuration;
        public RJPContext()
        {
        }

        public RJPContext(DbContextOptions<RJPContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<KeyWords> KeyWords { get; set; }
        public virtual DbSet<MaterialsType> MaterialsType { get; set; }
        public virtual DbSet<MovieCasts> MovieCasts { get; set; }
        public virtual DbSet<MovieCredits> MovieCredits { get; set; }
        public virtual DbSet<MovieCrews> MovieCrews { get; set; }
        public virtual DbSet<MovieGenres> MovieGenres { get; set; }
        public virtual DbSet<MovieKeyWords> MovieKeyWords { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<TVShowCasts> TVShowCasts { get; set; }
        public virtual DbSet<TVShowCredits> TVShowCredits { get; set; }
        public virtual DbSet<TVShowCrews> TVShowCrews { get; set; }
        public virtual DbSet<TVShowGenres> TVShowGenres { get; set; }
        public virtual DbSet<TVShowKeyWords> TVShowKeyWords { get; set; }
        public virtual DbSet<TVShows> TVShows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultString"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Genres>(entity =>
            {
                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.material_type_)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.material_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Genres_MaterialTypes_materialtypeid");
            });

            modelBuilder.Entity<KeyWords>(entity =>
            {
                entity.Property(e => e.material_type_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.material_type_)
                    .WithMany(p => p.KeyWords)
                    .HasForeignKey(d => d.material_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KeyWords_MaterialTypes_materialtypeid");
            });

            modelBuilder.Entity<MaterialsType>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<MovieCasts>(entity =>
            {
                entity.HasKey(e => e.credit_id);

                entity.Property(e => e.credit_id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.character)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.profile_path).HasMaxLength(50);

                entity.HasOne(d => d.credit_)
                    .WithOne(p => p.MovieCasts)
                    .HasForeignKey<MovieCasts>(d => d.credit_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieCasts_MovieCredits_CreditId");
            });

            modelBuilder.Entity<MovieCredits>(entity =>
            {
                entity.HasKey(e => e.credit_id);

                entity.Property(e => e.credit_id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.movie_)
                    .WithMany(p => p.MovieCredits)
                    .HasForeignKey(d => d.movie_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Credits_Movies_movieid");
            });

            modelBuilder.Entity<MovieCrews>(entity =>
            {
                entity.HasKey(e => e.credit_id);

                entity.Property(e => e.credit_id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.department)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.job)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.profile_path).HasMaxLength(50);

                entity.HasOne(d => d.credit_)
                    .WithOne(p => p.MovieCrews)
                    .HasForeignKey<MovieCrews>(d => d.credit_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Crews_Credits_CreditId");
            });

            modelBuilder.Entity<MovieGenres>(entity =>
            {
                entity.HasKey(e => new { e.movie_id, e.movie_genre_id });

                entity.Property(e => e.id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.movie_genre_)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.movie_genre_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieGenres_Genres_moviegenreid");

                entity.HasOne(d => d.movie_)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.movie_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieGenres_Movies_movieid");
            });

            modelBuilder.Entity<MovieKeyWords>(entity =>
            {
                entity.HasKey(e => new { e.movie_id, e.movie_keyword_id });

                entity.Property(e => e.id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.movie_)
                    .WithMany(p => p.MovieKeyWords)
                    .HasForeignKey(d => d.movie_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieKeyWords_Movies_movieid");

                entity.HasOne(d => d.movie_keyword_)
                    .WithMany(p => p.MovieKeyWords)
                    .HasForeignKey(d => d.movie_keyword_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieKeyWords_KeyWords_moviekeywordid");
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.backdrop_path).HasMaxLength(50);

                entity.Property(e => e.original_language)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.original_title).HasMaxLength(150);

                entity.Property(e => e.overview)
                    .IsRequired()
                    .HasMaxLength(1500);

                   entity.Property(e => e.status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.popularity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.poster_path).HasMaxLength(50);

                entity.Property(e => e.release_date).HasColumnType("date");

                entity.Property(e => e.title)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TVShowCasts>(entity =>
            {
                entity.HasKey(e => e.credit_id);

                entity.Property(e => e.credit_id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.character)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.profile_path).HasMaxLength(50);

                entity.HasOne(d => d.credit_)
                    .WithOne(p => p.TVShowCasts)
                    .HasForeignKey<TVShowCasts>(d => d.credit_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TVShowCasts_TVShowCredits_creditid");
            });

            modelBuilder.Entity<TVShowCredits>(entity =>
            {
                entity.HasKey(e => e.credit_id);

                entity.Property(e => e.credit_id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.tvshow_)
                    .WithMany(p => p.TVShowCredits)
                    .HasForeignKey(d => d.tvshow_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TVShowCredits_TVShows_tvshowid");
            });

            modelBuilder.Entity<TVShowCrews>(entity =>
            {
                entity.HasKey(e => e.credit_id);

                entity.Property(e => e.credit_id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.department)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.job)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.profile_path).HasMaxLength(50);

                entity.HasOne(d => d.credit_)
                    .WithOne(p => p.TVShowCrews)
                    .HasForeignKey<TVShowCrews>(d => d.credit_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TVShowCrews_TVShowCredits_creditid");
            });

            modelBuilder.Entity<TVShowGenres>(entity =>
            {
                entity.HasKey(e => new { e.tvshow_id, e.tvshow_genre_id });

                entity.HasOne(d => d.tvshow_genre_)
                    .WithMany(p => p.TVShowGenres)
                    .HasForeignKey(d => d.tvshow_genre_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TVShowGenres_Genres_tvshowgenreid");

                entity.HasOne(d => d.tvshow_)
                    .WithMany(p => p.TVShowGenres)
                    .HasForeignKey(d => d.tvshow_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TVShowGenres_TVShows_tvshowid");
            });

            modelBuilder.Entity<TVShowKeyWords>(entity =>
            {
                entity.HasKey(e => new { e.tvshow_id, e.tvshow_keyword_id });

                entity.HasOne(d => d.tvshow_)
                    .WithMany(p => p.TVShowKeyWords)
                    .HasForeignKey(d => d.tvshow_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TVShowKeyWords_TVShows_tvshowid");

                entity.HasOne(d => d.tvshow_keyword_)
                    .WithMany(p => p.TVShowKeyWords)
                    .HasForeignKey(d => d.tvshow_keyword_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TVShowKeyWords_KeyWords_tvshowkeywordid");
            });

            modelBuilder.Entity<TVShows>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.backdrop_path).HasMaxLength(50);

                entity.Property(e => e.first_air_date).HasColumnType("date");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.original_language)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.original_name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.overview)
                    .IsRequired()
                    .HasMaxLength(1500);

                entity.Property(e => e.popularity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.poster_path).HasMaxLength(50);

                entity.Property(e => e.status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.type)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
