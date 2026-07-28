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
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=SOCIAL-CIRCLE;Trusted_Connection=True;TrustServerCertificate=True;"
        );
    }

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

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534425F86FD").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__User__C9F28456048E4268").IsUnique();

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

            entity.HasMany(d => d.Posts).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Like",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Like__PostId__4AB81AF0"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Like__UserId__49C3F6B7"),
                    j =>
                    {
                        j.HasKey("UserId", "PostId").HasName("PK__Like__8D29EA4D8BD08C4C");
                        j.ToTable("Like");
                    });

            entity.HasMany(d => d.StoriesNavigation).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "StoryView",
                    r => r.HasOne<Story>().WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StoryView__Story__5BE2A6F2"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__StoryView__UserI__5AEE82B9"),
                    j =>
                    {
                        j.HasKey("UserId", "StoryId").HasName("PK__StoryVie__8460E048D31478A4");
                        j.ToTable("StoryView");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
