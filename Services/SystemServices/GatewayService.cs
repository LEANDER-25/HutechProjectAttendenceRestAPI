using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RESTAPIRNSQLServer.DBContext;
using RESTAPIRNSQLServer.DTOs.SystemDTOs;
using RESTAPIRNSQLServer.Applications.System;
using RESTAPIRNSQLServer.IServices;
using System.Security.Claims;
using System;
using RESTAPIRNSQLServer.Models;
using RESTAPIRNSQLServer.Extensions;
using RESTAPIRNSQLServer.Applications.Logic;

namespace RESTAPIRNSQLServer.Services.SystemServices
{
    public class GatewayService : IGatewayService
    {
        private readonly JWTSettings _jwtsettings;
        private readonly AttendenceDBContext _context;

        public GatewayService(AttendenceDBContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }

        private UserWithToken GenerateToken(UserWithToken infoUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, infoUser.Email),
                    new Claim("role", infoUser.Role.Value.ToString()),
                    new Claim("code", infoUser.Code)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            infoUser.Token = tokenHandler.WriteToken(token);

            return infoUser;
        }

        public async Task<UserWithToken> StudentLogin(UserLoginDTO user)
        {
            var existedUser = await _context.Students.Where(s => s.Email.Equals(user.Email)).Select(s => new
            {
                Email = s.Email,
                Password = s.Password,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Code = s.StudentCode
            }).FirstOrDefaultAsync();
            if (existedUser == null)
                throw new System.Exception("Email or Password is Invalid");
            if (existedUser.Password.Equals(user.Password.MD5Hash()))
            {

                var userWithToken = new UserWithToken()
                {
                    Code = existedUser.Code,
                    Email = existedUser.Email,
                    FirstName = existedUser.FirstName,
                    LastName = existedUser.LastName,
                    Role = Role.Student
                };
                userWithToken = GenerateToken(userWithToken);
                return userWithToken;
            }
            else
            {
                throw new System.Exception("Password is Invalid");
            }
        }
        public async Task<UserWithToken> TeacherLogin(UserLoginDTO user)
        {
            var existedUser = await _context.Teachers.Where(s => s.Email.Equals(user.Email)).Select(s => new
            {
                Email = s.Email,
                Password = s.Password,
                FirstName = s.TeacherFirstName,
                LastName = s.TeacherLastName,
                Code = s.TeacherCode
            }).FirstOrDefaultAsync();
            if (existedUser == null)
                throw new System.Exception("Email or Password is Invalid");
            if (existedUser.Password.Equals(user.Password.MD5Hash()))
            {

                var userWithToken = new UserWithToken()
                {
                    Code = existedUser.Code,
                    Email = existedUser.Email,
                    FirstName = existedUser.FirstName,
                    LastName = existedUser.LastName,
                    Role = Role.Teacher
                };
                userWithToken = GenerateToken(userWithToken);
                return userWithToken;
            }
            else
            {
                throw new System.Exception("Password is Invalid");
            }
        }

        private void ValidateEmailPassword(string email, string password)
        {
            if (!ValidationInput.IsEmailValid(email))
            {
                throw new Exception("Email is Invalid");
            }
            if (!ValidationInput.IsPasswordValid(password))
            {
                throw new Exception("Password is Invalid");
            }
        }

        public async Task<bool> StudentRegister(StudentRegisterDTO student)
        {
            bool isExist = await _context.Students
            .AnyAsync(
                s => s.Email.Equals(student.Email) || s.StudentCode == student.BusinessCode
            );

            if (isExist)
                throw new Exception("Student is existed!");

            ValidateEmailPassword(student.Email, student.Password);

            if (!ValidationInput.IsStudentCodeValid(student.BusinessCode))
            {
                throw new Exception("StudentCode is Invalid");
            }

            var newStudent = new Student()
            {
                StudentCode = student.BusinessCode,
                Email = student.Email,
                Password = student.Password.MD5Hash(),
                FirstName = student.FirstName,
                LastName = student.LastName
            };

            var academicYearID = await _context.AcademicYears
            .Where(
                y => y.AcademicYearName.Equals(student.AcademicYear)
            )
            .Select(y => y.AyId)
            .FirstOrDefaultAsync();

            if (academicYearID == 0)
            {
                throw new Exception($"Can not find the specify academic year: {student.AcademicYear}");
            }
            newStudent.AcademicYear = academicYearID;

            //get Class ID
            var idClass = await _context.Classrooms
            .Where(
                c => c.ClassName.Equals(student.ClassName)
            )
            .Select(c => c.ClassId)
            .FirstOrDefaultAsync();
            if (idClass == 0)
            {
                throw new Exception($"Can not find the specify class: {student.ClassName}");
            }

            //insert student into Student table
            await _context.Students.AddAsync(newStudent);
            await _context.SaveChangesAsync();

            //get id of new Student was inserted
            var idStudent = await _context.Students
            .Where(
                s => s.StudentCode.Equals(student.BusinessCode))
            .Select(s => s.StudentId)
            .FirstOrDefaultAsync();

            //insert student and class into MainClass table
            await _context.MainClasses.AddAsync(
                new MainClass
                {
                    StudentId = idStudent,
                    ClassId = idClass
                }
            );
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> TeacherRegister(TeacherRegisterDTO teacher)
        {
            bool isExist = await _context.Teachers
            .AnyAsync(
                t => t.Email.Equals(teacher.Email) || t.TeacherCode.Equals(teacher.BusinessCode)
            );

            if (isExist)
                throw new Exception("Student is existed!");

            ValidateEmailPassword(teacher.Email, teacher.Password);

            if (!ValidationInput.IsStudentCodeValid(teacher.BusinessCode))
            {
                throw new Exception("StudentCode is Invalid");
            }

            var newTeacher = new Teacher()
            {
                TeacherCode = teacher.BusinessCode,
                Email = teacher.Email,
                Password = teacher.Password.MD5Hash(),
                TeacherFirstName = teacher.FirstName,
                TeacherLastName = teacher.LastName
            };

            //insert student into Student table
            await _context.Teachers.AddAsync(newTeacher);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}