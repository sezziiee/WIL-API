﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Text;
using System.Text.Json.Serialization;
using WILAPI;
using WILAPI.Models;
//using XBCADAttendance.Models;


namespace AttendanceAPI.Controllers
{
   /* [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        DbWilContext context = new DbWilContext();

        [HttpPost]
        public string PostAttendance([FromBody] AttendanceRequest request)//Post method to get data from scanner
        {
            Attendance attendance = new Attendance(request);

            var lecture = context.TblStudentLectures.Where(x => x.LectureId == attendance.lectureID).FirstOrDefault();

            if (lecture != null)//Check if lecture is already in DB
            {
                lecture.ScanOut = attendance.Time;//Update scan out time
                context.SaveChanges();
                
                return "Success";
            } else //If there is no preexisting lecture in DB add a new one
            {
                if (!attendance.classroomCode.IsNullOrEmpty() && !attendance.UserID.IsNullOrEmpty() && !attendance.moduleCode.IsNullOrEmpty())
                {
                    context.TblStudentLectures.Add(new TblStudentLecture()
                    {
                        LectureId = attendance.lectureID,
                        UserId = attendance.UserID,
                        ClassroomCode = attendance.classroomCode,
                        LectureDate = (DateOnly)attendance.Date!,
                        ScanIn = (TimeOnly)attendance.Time!,
                        ModuleCode = attendance.moduleCode,
                    });

                    context.SaveChanges();
                    return "Success";
                } else
                {
                    return "Error: Null values in request";
                }
            }
        }

        [HttpGet]
        public List<string> GetModules()//Get method to populate module options in scanner
        {
            List<string> Output = new List<string>();

            var modules = context.TblModules.Where(x => x != null).Select(x => x.ModuleCode).ToList();

            if (modules != null)//Null check
            {
                Output.AddRange(modules);
            } else
            {
                Output.Add("Error: No modules found");
            }

            return Output;
        }

        [HttpPost("Add")]
        public string AddStudent([FromBody] NewUser user)
        {
            try
            {
                Hasher hasher = new Hasher("0000");
                string pw = hasher.GetHash();
                TblUser newUser = new TblUser
                {
                    UserId = user.UserId,
                    UserName = "New User",
                    Password = pw,
                };

                TblStudent newStudent = new TblStudent
                {
                    UserId = newUser.UserId,
                    StudentNo = user.StudentNo,
                };

                context.TblUsers.Add(newUser);
                context.TblStudents.Add(newStudent);
                newUser.TblStudent = newStudent;
                context.SaveChanges();

                return "Success";
            } catch (Exception e)
            {
                return e.ToString();
            }
        }

        [HttpPost("Staff/Add")]
        public string AddStaff([FromBody] NewStaff staff)
        {
            try
            {
                Hasher hasher = new Hasher("0000");
                string pw = hasher.GetHash();
                TblUser newUser = new TblUser
                {
                    UserId = staff.UserId,
                    UserName = "New User",
                    Password = pw,
                };

                TblStaff newStudent = new TblStaff
                {
                    UserId = newUser.UserId,
                    StaffId = staff.StaffNo,
                };

                context.TblUsers.Add(newUser);
                context.TblStaffs.Add(newStudent);
                newUser.TblStaff = newStudent;
                context.SaveChanges();

                return "Success";
            } catch (Exception e)
            {
                return e.ToString();
            }
        }

        [HttpGet("Lecture")]
        public List<TblStaffLecture> GetLectures(string UserId)
        {
            return context.TblStaffLectures.Where(x => x.UserId == UserId && x.Finish == null).ToList();
        }

        [HttpPost("Lecture")]
        public string UpdateLecture(string lectureId, string classroomCode)
        {
            try
            {
                var dbLecture = context.TblStaffLectures.Where(x => x.LectureId == lectureId).FirstOrDefault();
                
                if (dbLecture != null)
                {
                    if (dbLecture.Start != null)
                    {
                        dbLecture.ClassroomCode = classroomCode;
                        dbLecture.Finish = TimeOnly.FromDateTime(DateTime.Now);
                    }else
                    {
                        dbLecture.Start = TimeOnly.FromDateTime(DateTime.Now);
                    }

                    context.TblStaffLectures.Update(dbLecture);
                    context.SaveChanges();

                    return "Success";
                }else
                {
                    return "Lecture not found";
                }
            } catch (Exception e)
            {
                return e.ToString();
            }
        }
    }

    public struct NewUser
    {
        public string UserId { get; set; }
        public string StudentNo { get; set; }
    }

    public struct NewStaff
    {
        public string UserId { get; set; }
        public string StaffNo { get; set; }
    }
  */
}

