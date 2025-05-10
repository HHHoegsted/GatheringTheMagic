using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GatheringTheMagic.Models;

public partial class MtgcardsContext : DbContext
{
    public MtgcardsContext()
    {
    }

    public MtgcardsContext(DbContextOptions<MtgcardsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardColor> CardColors { get; set; }

    public virtual DbSet<CardColorIdentity> CardColorIdentities { get; set; }

    public virtual DbSet<CardPrinting> CardPrintings { get; set; }

    public virtual DbSet<CardSubtype> CardSubtypes { get; set; }

    public virtual DbSet<CardType> CardTypes { get; set; }

    public virtual DbSet<ForeignName> ForeignNames { get; set; }

    public virtual DbSet<Legality> Legalities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CardsDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cards__3213E83F42AF7AB6");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Artist)
                .HasMaxLength(255)
                .HasColumnName("artist");
            entity.Property(e => e.Cmc).HasColumnName("cmc");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("imageUrl");
            entity.Property(e => e.Layout)
                .HasMaxLength(50)
                .HasColumnName("layout");
            entity.Property(e => e.ManaCost)
                .HasMaxLength(50)
                .HasColumnName("manaCost");
            entity.Property(e => e.Multiverseid).HasColumnName("multiverseid");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(10)
                .HasColumnName("number");
            entity.Property(e => e.OriginalText).HasColumnName("originalText");
            entity.Property(e => e.OriginalType)
                .HasMaxLength(255)
                .HasColumnName("originalType");
            entity.Property(e => e.Power)
                .HasMaxLength(10)
                .HasColumnName("power");
            entity.Property(e => e.Rarity)
                .HasMaxLength(50)
                .HasColumnName("rarity");
            entity.Property(e => e.SetCode)
                .HasMaxLength(10)
                .HasColumnName("setCode");
            entity.Property(e => e.SetName)
                .HasMaxLength(255)
                .HasColumnName("setName");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Toughness)
                .HasMaxLength(10)
                .HasColumnName("toughness");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
        });

        modelBuilder.Entity<CardColor>(entity =>
        {
            entity.ToTable("CardColors");
            entity.HasKey(cc => new { cc.CardId, cc.Color});
            

            entity.Property(e => e.CardId)
                .HasMaxLength(255)
                .HasColumnName("card_id");
            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .HasColumnName("color");
            entity.HasOne(cc => cc.Card)
                .WithMany(c => c.CardColors)
                .HasForeignKey(cc => cc.CardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CardColor__card___4AB81AF0");
        });

        modelBuilder.Entity<CardColorIdentity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CardColorIdentity");

            entity.Property(e => e.CardId)
                .HasMaxLength(255)
                .HasColumnName("card_id");
            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .HasColumnName("color");

            entity.HasOne(d => d.Card).WithMany()
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CardColor__card___4CA06362");
        });

        modelBuilder.Entity<CardPrinting>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CardId)
                .HasMaxLength(255)
                .HasColumnName("card_id");
            entity.Property(e => e.SetCode)
                .HasMaxLength(10)
                .HasColumnName("setCode");

            entity.HasOne(d => d.Card).WithMany()
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CardPrint__card___52593CB8");
        });

        modelBuilder.Entity<CardSubtype>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CardId)
                .HasMaxLength(255)
                .HasColumnName("card_id");
            entity.Property(e => e.Subtype)
                .HasMaxLength(50)
                .HasColumnName("subtype");

            entity.HasOne(d => d.Card).WithMany()
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CardSubty__card___5070F446");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CardId)
                .HasMaxLength(255)
                .HasColumnName("card_id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Card).WithMany()
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__CardTypes__card___4E88ABD4");
        });

        modelBuilder.Entity<ForeignName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ForeignN__3213E83FCEA1D7C7");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId)
                .HasMaxLength(255)
                .HasColumnName("card_id");
            entity.Property(e => e.Flavor).HasColumnName("flavor");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("imageUrl");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .HasColumnName("language");
            entity.Property(e => e.Multiverseid).HasColumnName("multiverseid");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");

            entity.HasOne(d => d.Card).WithMany(p => p.ForeignNames)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ForeignNa__card___5535A963");
        });

        modelBuilder.Entity<Legality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Legaliti__3213E83FE0739955");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardId)
                .HasMaxLength(255)
                .HasColumnName("card_id");
            entity.Property(e => e.Format)
                .HasMaxLength(50)
                .HasColumnName("format");
            entity.Property(e => e.Legality1)
                .HasMaxLength(50)
                .HasColumnName("legality");

            entity.HasOne(d => d.Card).WithMany(p => p.Legalities)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Legalitie__card___5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
