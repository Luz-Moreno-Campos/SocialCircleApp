using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SocialCircle.Models;

namespace SocialCircle.DAL;

public partial class SocialCircleContext : DbContext
{
    public SocialCircleContext()
    {
    }

    public SocialCircleContext(DbContextOptions<SocialCircleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<DirectMessage> DirectMessages { get; set; }
    public virtual DbSet<Follow> Follows { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Story> Stories { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-Q3A9CJA\\SQLEXPRESS;Database=SOCIAL-CIRCLE;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFCA5ED120E0");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentText)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CommentTimeStamp).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__PostId__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__UserId__4E88ABD4");
        });

        modelBuilder.Entity<DirectMessage>(entity =>
        {
            entity.HasKey(e => e.DirectMessageId).HasName("PK__DirectMe__8332D39CAA7D7C3F");

            entity.ToTable("DirectMessage");

            entity.Property(e => e.DmTimestamp).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.MessageText)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Receiver).WithMany(p => p.DirectMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DirectMes__Recei__60A75C0F");

            entity.HasOne(d => d.Sender).WithMany(p => p.DirectMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DirectMes__Sende__5FB337D6");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => new { e.FollowerId, e.FollowingId }).HasName("PK__Follow__79CB0335499DA56E");

            entity.ToTable("Follow");

            entity.Property(e => e.FollowStartTimestamp).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Follower).WithMany(p => p.FollowFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Follow__Follower__534D60F1");

            entity.HasOne(d => d.Following).WithMany(p => p.FollowFollowings)
                .HasForeignKey(d => d.FollowingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Follow__Followin__5441852A");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA1260185D27A8CB");

            entity.ToTable("Post");

            entity.Property(e => e.PostTimeStamp).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.TextContent)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.PostsNavigation)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Post__UserId__46E78A0C");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__Story__3E82C048EA02E589");

            entity.ToTable("Story");

            entity.Property(e => e.CreationTimestamp).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.StoryText)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Stories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Story__UserId__5812160E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C1E259766");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();

            entity.Property(e => e.Bio)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

