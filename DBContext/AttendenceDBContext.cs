using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RESTAPIRNSQLServer.Models;

#nullable disable

namespace RESTAPIRNSQLServer.DBContext
{
    public partial class AttendenceDBContext : DbContext
    {
        public AttendenceDBContext()
        {
        }

        public AttendenceDBContext(DbContextOptions<AttendenceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Assign> Assigns { get; set; }
        public virtual DbSet<CheckIn> CheckIns { get; set; }
        public virtual DbSet<CheckInsPhoto> CheckInsPhotos { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lession> Lessions { get; set; }
        public virtual DbSet<MainClass> MainClasses { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentPhoto> StudentPhotos { get; set; }
        public virtual DbSet<StudyShift> StudyShifts { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeacherPhoto> TeacherPhotos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=AttendenceDB;Trusted_connection=yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.HasKey(e => e.AyId)
                    .HasName("_copy_19");

                entity.Property(e => e.AcademicYearName).IsUnicode(false);
            });

            modelBuilder.Entity<Assign>(entity =>
            {
                entity.HasKey(e => new { e.ClassId, e.CourseId, e.SubjectId })
                    .HasName("_copy_18");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Assigns)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assign_class");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Assigns)
                    .HasForeignKey(d => new { d.CourseId, d.SubjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assign_course");
            });

            modelBuilder.Entity<CheckIn>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SubjectId, e.CourseId, e.ClassId, e.ScheduleId })
                    .HasName("_copy_9");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CheckIns)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_check_student");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.CheckIns)
                    .HasForeignKey(d => new { d.SubjectId, d.CourseId, d.ClassId, d.ScheduleId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_check_schedule");
            });

            modelBuilder.Entity<CheckInsPhoto>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SubjectId, e.CourseId, e.ClassId, e.ScheduleId })
                    .HasName("_copy_9_copy_1");

                entity.HasOne(d => d.CheckIn)
                    .WithOne(p => p.CheckInsPhoto)
                    .HasForeignKey<CheckInsPhoto>(d => new { d.StudentId, d.SubjectId, d.CourseId, d.ClassId, d.ScheduleId })
                    .HasConstraintName("fk_photo_check_ins");
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("_copy_12");

                entity.Property(e => e.ClassName).IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.SubjectId })
                    .HasName("_copy_13");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_course_subject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("fk_course_teacher");
            });

            modelBuilder.Entity<Lession>(entity =>
            {
                entity.HasKey(e => e.LessonId)
                    .HasName("_copy_20");
            });

            modelBuilder.Entity<MainClass>(entity =>
            {
                entity.HasKey(e => new { e.ClassId, e.StudentId })
                    .HasName("_copy_11");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.MainClasses)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main_class");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.MainClasses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_main_student");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomName).IsUnicode(false);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => new { e.ClassId, e.CourseId, e.SubjectId, e.ScheduleId })
                    .HasName("_copy_10");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("fk_schedule_room");

                entity.HasOne(d => d.Assign)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => new { d.ClassId, d.CourseId, d.SubjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_schedule_assign");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.StudentCode).IsUnicode(false);

                entity.HasOne(d => d.AcademicYearNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.AcademicYear)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_student_acayear");
            });

            modelBuilder.Entity<StudentPhoto>(entity =>
            {
                entity.HasKey(e => e.StudentId)
                    .HasName("PK__Student___2A33069A03CC63F1");

                entity.Property(e => e.StudentId).ValueGeneratedNever();

                entity.HasOne(d => d.Student)
                    .WithOne(p => p.StudentPhoto)
                    .HasForeignKey<StudentPhoto>(d => d.StudentId)
                    .HasConstraintName("fk_photo_student");
            });

            modelBuilder.Entity<StudyShift>(entity =>
            {
                entity.HasKey(e => new { e.LessonId, e.ClassId, e.CourseId, e.SubjectId })
                    .HasName("_copy_21");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.StudyShifts)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_shift_lession");

                entity.HasOne(d => d.Assign)
                    .WithMany(p => p.StudyShifts)
                    .HasForeignKey(d => new { d.ClassId, d.CourseId, d.SubjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_shifts_schedule");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectCode).IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.TeacherCode).IsUnicode(false);
            });

            modelBuilder.Entity<TeacherPhoto>(entity =>
            {
                entity.HasKey(e => e.TeacherId)
                    .HasName("PK__Teacher___03AE777ECAF806A8");

                entity.Property(e => e.TeacherId).ValueGeneratedNever();

                entity.HasOne(d => d.Teacher)
                    .WithOne(p => p.TeacherPhoto)
                    .HasForeignKey<TeacherPhoto>(d => d.TeacherId)
                    .HasConstraintName("fk_photo_teachers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
