﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Text;
using WILAPI;
using WILAPI.Models;


namespace AttendanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        DbWilContext context = new DbWilContext();

        [HttpPost]
        public string PostAttendance([FromBody] AttendanceRequest request)//Post method to get data from scanner
        {
            Attendance attendance = new Attendance(request);

            var lecture = context.TblStudentLectures.Where(x => x.UserId == attendance.UserID && x.LectureDate == attendance.Date).FirstOrDefault();

            if (lecture != null)//Check if lecture is already in DB
            {
                lecture.ScanOut = attendance.Time;//Update scan out time

                return "Success";
            } else //If there is no preexisting lecture in DB add a new one
            {
                if (!attendance.classroomCode.IsNullOrEmpty() && !attendance.UserID.IsNullOrEmpty() && !attendance.moduleCode.IsNullOrEmpty())
                {
                    context.TblStudentLectures.Add(new TblStudentLecture()
                    {
                        LectureId = "L" + context.TblStudentLectures.Count(),
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

                TblUser newUser = new TblUser
                {
                    UserId = user.UserId,
                    UserName = "New User",
                    Password = hasher.GetHash(),
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

        [HttpGet("Lecture")]
        public List<TblStaffLecture> GetLectures(string UserId)
        {
            return context.TblStaffLectures.Where(x => x.UserId == UserId).ToList();
        }

        [HttpPost("Lecture")]
        public string StartLecture(TblStaffLecture lecture)
        {
            try
            {
                context.TblStaffLectures.Update(lecture);
                context.SaveChanges();

                return "Success";

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
}

