using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FitnessFinal.Models;

public partial class FinalContext : DbContext
{
    public FinalContext()
    {
    }

    public FinalContext(DbContextOptions<FinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Enquiry> Enquiries { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Membershiptype> Membershiptypes { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<TrainerEnquiry> TrainerEnquiries { get; set; }

    public virtual DbSet<UserMembership> UserMemberships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MsSqlLocalDb;Initial Catalog=Final;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enquiry>(entity =>
        {
            entity.HasKey(e => e.Enqid).HasName("PK__tmp_ms_x__06973B78DD206541");

            entity.ToTable("Enquiry");

            entity.Property(e => e.Enqid).HasColumnName("enqid");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fee)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fee");
            entity.Property(e => e.Gympackage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gympackage");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Enquiries)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enquiry__user_id__34C8D9D1");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tmp_ms_x__B9BE370F6BD40781");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "UQ__tmp_ms_x__AB6E6164F7C864EC").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.SecurityAns)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("security_ans");
            entity.Property(e => e.SecurityQues)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("security_ques");
        });

        modelBuilder.Entity<Membershiptype>(entity =>
        {
            entity.HasKey(e => e.MembershipId).HasName("PK__Membersh__CAE49DDDDD7E64E1");

            entity.ToTable("Membershiptype");

            entity.Property(e => e.MembershipId).HasColumnName("membership_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Fee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fee");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__Trainers__65A4B6299A522C4F");

            entity.HasIndex(e => e.Email, "UQ__Trainers__AB6E61648B564E5F").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__Trainers__B43B145F7AA96840").IsUnique();

            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Specialization)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("specialization");
        });

        modelBuilder.Entity<TrainerEnquiry>(entity =>
        {
            entity.HasKey(e => e.TrainerEnquiryId).HasName("PK__trainer___6B66FD488C9D4F61");

            entity.ToTable("trainer_enquiry");

            entity.Property(e => e.TrainerEnquiryId).HasColumnName("trainer_enquiry_id");
            entity.Property(e => e.AssignedDate).HasColumnName("assigned_date");
            entity.Property(e => e.Enqid).HasColumnName("enqid");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Enq).WithMany(p => p.TrainerEnquiries)
                .HasForeignKey(d => d.Enqid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trainer_e__enqid__3C69FB99");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TrainerEnquiries)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trainer_e__train__3B75D760");
        });

        modelBuilder.Entity<UserMembership>(entity =>
        {
            entity.HasKey(e => e.UserMembershipId).HasName("PK__user_mem__E37A253424466EB0");

            entity.ToTable("user_membership");

            entity.Property(e => e.UserMembershipId).HasColumnName("user_membership_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.MembershipId).HasColumnName("membership_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Membership).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.MembershipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_memb__membe__38996AB5");

            entity.HasOne(d => d.User).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_memb__user___37A5467C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
