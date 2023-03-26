using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models;

public partial class Lab1Context : DbContext
{
    public Lab1Context()
    {
    }

    public Lab1Context(DbContextOptions<Lab1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Carriage> Carriages { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<TrainSchedule> TrainSchedules { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= DESKTOP-BUN868F\\SQLEXPRESS;Database=Lab1;Trusted_Connection=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carriage>(entity =>
        {
            entity.Property(e => e.CarriageId).HasColumnName("Carriage_id");
            entity.Property(e => e.CarriageName)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Carriage_name");
            entity.Property(e => e.CarriageType).HasColumnName("Carriage_type");
            entity.Property(e => e.PlaceCount).HasColumnName("Place_count");
            entity.Property(e => e.TrainId).HasColumnName("Train_id");

            entity.HasOne(d => d.Train).WithMany(p => p.Carriages)
                .HasForeignKey(d => d.TrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carriages_Trains");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PsId);

            entity.Property(e => e.PsId).HasColumnName("PS_id");
            entity.Property(e => e.PsEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PS_email");
            entity.Property(e => e.PsName)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PS_name");
            entity.Property(e => e.PsPassport)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PS_passport");
            entity.Property(e => e.PsPhone)
                .IsRequired()
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("PS_phone");
            entity.Property(e => e.PsSurname)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("PS_surname");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("Schedule_id");
            entity.Property(e => e.StationName)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Station_name");
            entity.Property(e => e.StationNumber).HasColumnName("Station_number");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.Property(e => e.TicketId).HasColumnName("Ticket_id");
            entity.Property(e => e.CarriageId).HasColumnName("Carriage_id");
            entity.Property(e => e.DateOfBuying)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_buying");
            entity.Property(e => e.PId).HasColumnName("P_id");
            entity.Property(e => e.PlaceNumber).HasColumnName("Place_number");
            entity.Property(e => e.PsId).HasColumnName("PS_id");
            entity.Property(e => e.TicketPrice)
                .HasColumnType("money")
                .HasColumnName("Ticket_price");

            entity.HasOne(d => d.Carriage).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CarriageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Carriages");

            entity.HasOne(d => d.PIdNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Train_schedule");

            entity.HasOne(d => d.Ps).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Passengers");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.Property(e => e.TrainId).HasColumnName("Train_id");
            entity.Property(e => e.ScheduleId).HasColumnName("Schedule_id");
            entity.Property(e => e.TrainDeparture)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Train_departure");
            entity.Property(e => e.TrainDestination)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Train_destination");
            entity.Property(e => e.TrainTimeOfDep).HasColumnName("Train_time_of_dep");
            entity.Property(e => e.TrainTimeOfStop).HasColumnName("Train_time_of_stop");
            entity.Property(e => e.TrainType)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Train_type");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Trains)
                .HasForeignKey(d => d.ScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trains_Schedule");
        });

        modelBuilder.Entity<TrainSchedule>(entity =>
        {
            entity.HasKey(e => e.PId);

            entity.ToTable("Train_schedule");

            entity.Property(e => e.PId).HasColumnName("P_id");
            entity.Property(e => e.TrainDate)
                .HasColumnType("date")
                .HasColumnName("Train_date");
            entity.Property(e => e.TrainId).HasColumnName("Train_id");

            entity.HasOne(d => d.Train).WithMany(p => p.TrainSchedules)
                .HasForeignKey(d => d.TrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Train_schedule_Trains");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
