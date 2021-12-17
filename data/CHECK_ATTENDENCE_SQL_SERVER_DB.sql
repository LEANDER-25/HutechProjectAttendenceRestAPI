--create database AttendenceDB
--use AttendenceDB;

CREATE TABLE [Academic_Years] (
  [ay_id] int IDENTITY(1,1) NOT NULL,
  [academic_year_name] varchar(25) NOT NULL,
  [start_year] INT NOT NULL,
  [end_year] INT NOT NULL,
  CONSTRAINT [_copy_19] PRIMARY KEY CLUSTERED ([ay_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Assigns] (
  [class_id] int NOT NULL,
  [course_id] int NOT NULL,
  [subject_id] int NOT NULL,
  CONSTRAINT [_copy_18] PRIMARY KEY CLUSTERED ([class_id], [course_id], [subject_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Classrooms] (
  [class_id] int IDENTITY(1,1) NOT NULL,
  [class_name] varchar(25) NOT NULL,
  [description] nvarchar(255) NULL,
  CONSTRAINT [_copy_12] PRIMARY KEY CLUSTERED ([class_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Courses] (
  [course_id] int NOT NULL,
  [subject_id] int NOT NULL,
  [point] float NULL,
  [semester] int NULL,
  [year] int NULL,
  [teacher_id] int NULL,
  CONSTRAINT [_copy_13] PRIMARY KEY CLUSTERED ([course_id], [subject_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Check_Ins] (
  [student_id] int NOT NULL,
  [subject_id] int NOT NULL,
  [course_id] int NOT NULL,
  [class_id] int NOT NULL,
  [schedule_id] int NOT NULL,
  [cur_location] nvarchar(500) NOT NULL,
  CONSTRAINT [_copy_9] PRIMARY KEY CLUSTERED ([student_id], [subject_id], [course_id], [class_id], [schedule_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Check_Ins_Photos] (
  [student_id] int NOT NULL,
  [subject_id] int NOT NULL,
  [course_id] int NOT NULL,
  [class_id] int NOT NULL,
  [schedule_id] int NOT NULL,
  [photo] image NOT NULL,
  CONSTRAINT [_copy_9_copy_1] PRIMARY KEY CLUSTERED ([student_id], [subject_id], [course_id], [class_id], [schedule_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Lessions] (
  [lesson_id] int IDENTITY(1,1) NOT NULL,
  [lesson_name] nvarchar(255) NOT NULL,
  [start_time] time NOT NULL,
  [end_time] time NOT NULL,
  CONSTRAINT [_copy_20] PRIMARY KEY CLUSTERED ([lesson_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Main_Classes] (
  [class_id] int NOT NULL,
  [student_id] int NOT NULL,
  CONSTRAINT [_copy_11] PRIMARY KEY CLUSTERED ([class_id], [student_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Rooms] (
  [room_id] int IDENTITY(1,1) NOT NULL,
  [room_name] varchar(25) NOT NULL,
  [description] nvarchar(255) NULL,
  CONSTRAINT [_copy_16] PRIMARY KEY CLUSTERED ([room_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Schedules] (
  [class_id] int NOT NULL,
  [course_id] int NOT NULL,
  [subject_id] int NOT NULL,
  [schedule_id] int NOT NULL,
  [study_date] date NULL,
  [room_id] int NULL,
  CONSTRAINT [_copy_10] PRIMARY KEY CLUSTERED ([class_id], [course_id], [subject_id], [schedule_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Student_Photos] (
  [student_id] int NOT NULL,
  [photo] image NULL,
  PRIMARY KEY CLUSTERED ([student_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Students] (
  [student_id] int IDENTITY(1,1) NOT NULL,
  [student_code] varchar(25) NOT NULL,
  [first_name] nvarchar(255) NOT NULL,
  [last_name] nvarchar(255) NOT NULL,
  [password] varchar(100) NOT NULL,
  [email] varchar(255) NOT NULL,
  [academic_year] int NOT NULL,
  CONSTRAINT [_copy_17] PRIMARY KEY CLUSTERED ([student_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_email_student]
ON [Students] (
  [email]
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_student_id]
ON [Students] (
  [student_code]
)
GO

CREATE TABLE [Study_Shifts] (
  [lesson_id] int NOT NULL,
  [class_id] int NOT NULL,
  [course_id] int NOT NULL,
  [subject_id] int NOT NULL,
  CONSTRAINT [_copy_21] PRIMARY KEY CLUSTERED ([lesson_id], [class_id], [course_id], [subject_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Subjects] (
  [subject_id] int IDENTITY(1,1) NOT NULL,
  [subject_code] varchar(25) NOT NULL,
  [subject_name] nvarchar(255) NOT NULL,
  CONSTRAINT [_copy_15] PRIMARY KEY CLUSTERED ([subject_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Teacher_Photos] (
  [teacher_id] int NOT NULL,
  [photos] image NULL,
  PRIMARY KEY CLUSTERED ([teacher_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO

CREATE TABLE [Teachers] (
  [teacher_id] int IDENTITY(1,1) NOT NULL,
  [teacher_code] varchar(25) NOT NULL,
  [teacher_first_name] nvarchar(255) NOT NULL,
  [teacher_last_name] nvarchar(255) NOT NULL,
  [email] varchar(255) NOT NULL,
  [password] varchar(100) NOT NULL,
  CONSTRAINT [_copy_14] PRIMARY KEY CLUSTERED ([teacher_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [idx_email_teacher]
ON [Teachers] (
  [email],
  [teacher_code]
)
GO

ALTER TABLE [Assigns] ADD CONSTRAINT [fk_assign_class] FOREIGN KEY ([class_id]) REFERENCES [Classrooms] ([class_id])
GO
ALTER TABLE [Assigns] ADD CONSTRAINT [fk_assign_course] FOREIGN KEY ([course_id], [subject_id]) REFERENCES [Courses] ([course_id], [subject_id])
GO
ALTER TABLE [Courses] ADD CONSTRAINT [fk_course_subject] FOREIGN KEY ([subject_id]) REFERENCES [Subjects] ([subject_id])
GO
ALTER TABLE [Courses] ADD CONSTRAINT [fk_course_teacher] FOREIGN KEY ([teacher_id]) REFERENCES [Teachers] ([teacher_id])
GO
ALTER TABLE [Check_Ins] ADD CONSTRAINT [fk_check_student] FOREIGN KEY ([student_id]) REFERENCES [Students] ([student_id])
GO
ALTER TABLE [Check_Ins] ADD CONSTRAINT [fk_check_schedule] FOREIGN KEY ([subject_id], [course_id], [class_id], [schedule_id]) REFERENCES [Schedules] ([class_id], [course_id], [subject_id], [schedule_id])
GO
ALTER TABLE [Check_Ins_Photos] ADD CONSTRAINT [fk_photo_check_ins] FOREIGN KEY ([student_id], [subject_id], [course_id], [class_id], [schedule_id]) REFERENCES [Check_Ins] ([student_id], [subject_id], [course_id], [class_id], [schedule_id]) ON DELETE CASCADE
GO
ALTER TABLE [Main_Classes] ADD CONSTRAINT [fk_main_student] FOREIGN KEY ([student_id]) REFERENCES [Students] ([student_id])
GO
ALTER TABLE [Main_Classes] ADD CONSTRAINT [fk_main_class] FOREIGN KEY ([class_id]) REFERENCES [Classrooms] ([class_id])
GO
ALTER TABLE [Schedules] ADD CONSTRAINT [fk_schedule_assign] FOREIGN KEY ([class_id], [course_id], [subject_id]) REFERENCES [Assigns] ([class_id], [course_id], [subject_id])
GO
ALTER TABLE [Schedules] ADD CONSTRAINT [fk_schedule_room] FOREIGN KEY ([room_id]) REFERENCES [Rooms] ([room_id])
GO
ALTER TABLE [Student_Photos] ADD CONSTRAINT [fk_photo_student] FOREIGN KEY ([student_id]) REFERENCES [Students] ([student_id]) ON DELETE CASCADE
GO
ALTER TABLE [Students] ADD CONSTRAINT [fk_student_acayear] FOREIGN KEY ([academic_year]) REFERENCES [Academic_Years] ([ay_id])
GO
ALTER TABLE [Study_Shifts] ADD CONSTRAINT [fk_shift_lession] FOREIGN KEY ([lesson_id]) REFERENCES [Lessions] ([lesson_id])
GO
ALTER TABLE [Study_Shifts] ADD CONSTRAINT [fk_shifts_schedule] FOREIGN KEY ([class_id], [course_id], [subject_id]) REFERENCES [Assigns] ([class_id], [course_id], [subject_id])
GO
ALTER TABLE [Teacher_Photos] ADD CONSTRAINT [fk_photo_teachers] FOREIGN KEY ([teacher_id]) REFERENCES [Teachers] ([teacher_id]) ON DELETE CASCADE
GO

