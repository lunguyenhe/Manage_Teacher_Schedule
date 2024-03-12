using CsvHelper;
using CsvHelper.Configuration;
using Manage_Teacher_Schedule.DTO;
using Manage_Teacher_Schedule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.WebSockets;
using System.Reflection.PortableExecutable;
using System.Web;
namespace Manage_Teacher_Schedule.Controllers
{
    public class HomeController : Controller
    {
        private Manage_Schedules_TeacherContext context;

        public HomeController(Manage_Schedules_TeacherContext _context)
        {
            this.context = _context;

        }

        public IActionResult Index(int? week, int? yearchoose)
        {

            var listSlot = context.Slots.ToList();
            List<String> dateofweek = new List<String>();

            Dictionary<int, string> listDate = new Dictionary<int, string>();
            DateTime datenow = DateTime.Now;
            string year = DateTime.Now.Year.ToString();
            int yearint = DateTime.Now.Year;
            if (yearchoose is not null)
            {
                yearint = (int)yearchoose;
                year = yearchoose.ToString();
            }


            String startdate = year + "-01-01";
            string format = "yyyy-MM-dd";
            DayOfWeek dayOfWeek = datenow.DayOfWeek;
            int weekOfYear = (datenow.DayOfYear + (7 - (int)datenow.DayOfWeek)) / 7;
            if (dayOfWeek == DayOfWeek.Sunday)
            {
                weekOfYear--;
            }


            DateTime dateTime = DateTime.ParseExact(startdate, format, null);

            for (int i = 1; i <= 53; i++)
            {

                if (dateTime.DayOfWeek == DayOfWeek.Monday)
                {
                    listDate.Add(i, dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "-" + dateTime.AddDays(6).Day.ToString() + "/" + dateTime.AddDays(6).Month.ToString());
                    dateTime = dateTime.AddDays(7);

                }
                else if (dateTime.DayOfWeek == DayOfWeek.Tuesday)
                {
                    listDate.Add(i, dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "-" + dateTime.AddDays(5).Day.ToString() + "/" + dateTime.AddDays(5).Month.ToString());
                    dateTime = dateTime.AddDays(6);

                }
                else if (dateTime.DayOfWeek == DayOfWeek.Wednesday)
                {
                    listDate.Add(i, dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "-" + dateTime.AddDays(4).Day.ToString() + "/" + dateTime.AddDays(4).Month.ToString());
                    dateTime = dateTime.AddDays(5);
                }
                else if (dateTime.DayOfWeek == DayOfWeek.Thursday)
                {
                    listDate.Add(i, dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "-" + dateTime.AddDays(3).Day.ToString() + "/" + dateTime.AddDays(3).Month.ToString());
                    dateTime = dateTime.AddDays(4);
                }
                else if (dateTime.DayOfWeek == DayOfWeek.Friday)
                {
                    listDate.Add(i, dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "-" + dateTime.AddDays(2).Day.ToString() + "/" + dateTime.AddDays(2).Month.ToString());
                    dateTime = dateTime.AddDays(3);
                }
                else if (dateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    listDate.Add(i, dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "-" + dateTime.AddDays(1).Day.ToString() + "/" + dateTime.AddDays(1).Month.ToString());
                    dateTime = dateTime.AddDays(2);

                }
                else if (dateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    listDate.Add(i, dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "-" + dateTime.AddDays(0).Day.ToString() + "/" + dateTime.AddDays(0).Month.ToString());
                    dateTime = dateTime.AddDays(1);
                }
                if (i == 53)
                {
                    Console.WriteLine(dateTime);
                    if (dateTime.Year > DateTime.Now.Year)
                    {
                        listDate.Remove(53);
                    }
                }
                if (i == weekOfYear && week is null)
                {
                    var dayweek = listDate.Where(s => s.Key == i).FirstOrDefault();
                    string[] parts = dayweek.Value.Split('/', '-');
                    int[] numbers = new int[parts.Length];

                    // Chuyển đổi từng phần tử của mảng thành số nguyên và lưu vào mảng số nguyên
                    for (int j = 0; j < parts.Length; j++)
                    {
                        if (int.TryParse(parts[j], out int result))
                        {
                            numbers[j] = result;
                        }

                    }
                    DateTime daystart = new DateTime(yearint, numbers[1], numbers[0]);
                    DateTime dayend = new DateTime(yearint, numbers[3], numbers[2]);
                    Dictionary<int, List<Schedule>> listscheduleall = new Dictionary<int, List<Schedule>>();
                    var listschedule = context.Schedules.Include(s => s.ClassSubject).Where(s => s.TeachingDay <= dayend && s.TeachingDay >= daystart).ToList();
                    var listscheduleDistinct = listschedule.DistinctBy(s => s.SlotId).ToList();

                    listscheduleall.Add(1, listschedule.Where(s => s.SlotId == 1).ToList());
                    listscheduleall.Add(2, listschedule.Where(s => s.SlotId == 2).ToList());
                    listscheduleall.Add(3, listschedule.Where(s => s.SlotId == 3).ToList());
                    listscheduleall.Add(4, listschedule.Where(s => s.SlotId == 4).ToList());
                    //   ViewData["schedule"] = listschedule;
                    ViewData["schedule"] = listscheduleall;
                    ViewData["scheduleoneDateDistinct"] = listscheduleDistinct;
                    for (DateTime date = daystart; date <= dayend; date = date.AddDays(1))
                    {
                        dateofweek.Add(date.ToString("dd/MM"));
                        Console.WriteLine(date.ToString("dd/MM"));
                    }
                }
                else if (week is not null)
                {
                    if (week != i)
                    {
                        continue;
                    }
                    var dayweek = listDate.Where(s => s.Key == i).FirstOrDefault();
                    string[] parts = dayweek.Value.Split('/', '-');
                    int[] numbers = new int[parts.Length];

                    // Chuyển đổi từng phần tử của mảng thành số nguyên và lưu vào mảng số nguyên
                    for (int j = 0; j < parts.Length; j++)
                    {
                        if (int.TryParse(parts[j], out int result))
                        {
                            numbers[j] = result;
                        }

                    }
                    DateTime daystart = new DateTime(yearint, numbers[1], numbers[0]);
                    DateTime dayend = new DateTime(yearint, numbers[3], numbers[2]);
                    Dictionary<int, List<Schedule>> listscheduleall = new Dictionary<int, List<Schedule>>();
                    var listschedule = context.Schedules.Include(s => s.ClassSubject).Where(s => s.TeachingDay <= dayend && s.TeachingDay >= daystart).ToList();
                    var listscheduleDistinct = listschedule.DistinctBy(s => s.TeachingDay).ToList();

                    listscheduleall.Add(1, listschedule.Where(s => s.SlotId == 1).ToList());
                    listscheduleall.Add(2, listschedule.Where(s => s.SlotId == 2).ToList());
                    listscheduleall.Add(3, listschedule.Where(s => s.SlotId == 3).ToList());
                    listscheduleall.Add(4, listschedule.Where(s => s.SlotId == 4).ToList());
                    //   ViewData["schedule"] = listschedule;
                    ViewData["schedule"] = listscheduleall;
                    ViewData["scheduleoneDateDistinct"] = listscheduleDistinct;
                    for (DateTime date = daystart; date <= dayend; date = date.AddDays(1))
                    {
                        dateofweek.Add(date.ToString("dd/MM"));
                        Console.WriteLine(date.ToString("dd/MM"));
                    }

                }

            }

            if (week is not null)
            {
                ViewData["weekOfYear"] = (int)week;
            }
            else
            {
                ViewData["weekOfYear"] = weekOfYear;
            }

            foreach (var pair in listDate)
            {
                Console.WriteLine("Key: " + pair.Key + ", Value: " + pair.Value);
            }

            ViewData["listdate"] = listDate;
            ViewData["dateofweek"] = dateofweek;
            ViewData["year"] = yearint;
            ViewData["slots"] = listSlot;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using(FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var list = this.GetSchedules(file.FileName);
            foreach(var i in list)
            {
               // var add = new Schedule();
                DateTime startDate =  DateTime.Parse(i.StartDate);
                DateTime endDate = DateTime.Parse(i.EndDate);
                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    
                    //  Console.WriteLine(date.ToString("yyyy-MM-dd"));
                    if (i.TimeSlot[0]==('A'))
                    {
                        var add = new Schedule();
                        DayOfWeek day = date.DayOfWeek;
                        int dayNumber = (int)day + 1;
                        if (day == DayOfWeek.Sunday)
                        {
                            dayNumber = 8;
                        }
                       
                        char secondChar = i.TimeSlot[1];
                        int timeslot1 = int.Parse(secondChar.ToString());

                        char secondChar2 = i.TimeSlot[2];
                        int timeslot2 = int.Parse(secondChar2.ToString());
                        if (dayNumber == timeslot1)
                        {
                            var classsub = context.ClassSubjects.FirstOrDefault(s => s.SubjectId == i.Subject && s.ClassId == i.Class);

                            if (classsub == null)
                            {
                                var classsub1 = new ClassSubject();
                                classsub1.SubjectId = i.Subject;
                                classsub1.ClassId = i.Class;
                                context.ClassSubjects.Add(classsub1);
                                context.SaveChanges();
                                classsub = context.ClassSubjects.FirstOrDefault(s => s.SubjectId == i.Subject && s.ClassId == i.Class);

                            }
                            add.ClassSubjectId= classsub.ClassSubjectId;
                            add.SlotId = 1;
                            add.RoomId = i.Room;
                            add.TeacherId = i.Teacher;
                            add.TeachingDay = date;
                            context.Schedules.Add(add);
                            context.SaveChanges();

                        }
                        if (dayNumber == timeslot2)
                        {

                            var classsub = context.ClassSubjects.FirstOrDefault(s => s.SubjectId == i.Subject && s.ClassId == i.Class);

                            if (classsub == null)
                            {
                                var classsub1 = new ClassSubject();
                                classsub1.SubjectId = i.Subject;
                                classsub1.ClassId = i.Class;
                                context.ClassSubjects.Add(classsub1);
                                context.SaveChanges();
                                classsub = context.ClassSubjects.FirstOrDefault(s => s.SubjectId == i.Subject && s.ClassId == i.Class);

                            }
                            add.ClassSubjectId = classsub.ClassSubjectId;
                            add.SlotId = 2;
                            add.RoomId = i.Room;
                            add.TeacherId = i.Teacher;
                            add.TeachingDay = date;
                            context.Schedules.Add(add);
                            context.SaveChanges();
                        }
                    }
                    else if(i.TimeSlot[0]=='P')
                    {
                        var add = new Schedule();
                        DayOfWeek day = date.DayOfWeek;
                        int dayNumber = (int)day + 1;
                        if (day == DayOfWeek.Sunday)
                        {
                            dayNumber = 8;
                        }

                        char secondChar = i.TimeSlot[1];
                        int timeslot1 = int.Parse(secondChar.ToString());

                        char secondChar2 = i.TimeSlot[2];
                        int timeslot2 = int.Parse(secondChar2.ToString());
                        if (dayNumber == timeslot1)
                        {
                            var classsub = context.ClassSubjects.FirstOrDefault(s => s.SubjectId == i.Subject && s.ClassId == i.Class);

                            if (classsub == null)
                            {
                                var classsub1 = new ClassSubject();
                                classsub1.SubjectId = i.Subject;
                                classsub1.ClassId = i.Class;
                                context.ClassSubjects.Add(classsub1);
                                context.SaveChanges();
                                classsub = context.ClassSubjects.FirstOrDefault(s => s.SubjectId == i.Subject && s.ClassId == i.Class);

                            }
                            add.ClassSubjectId = classsub.ClassSubjectId;
                            add.SlotId = 3;
                            add.RoomId = i.Room;
                            add.TeacherId = i.Teacher;
                            add.TeachingDay = date;
                            context.Schedules.Add(add);
                            context.SaveChanges();

                        }
                        if (dayNumber == timeslot2)
                        {

                            var classsub = context.ClassSubjects.FirstOrDefault(s => s.SubjectId == i.Subject && s.ClassId == i.Class);

                            if (classsub == null)
                            {
                                var classsub1 = new ClassSubject();
                                classsub1.SubjectId = i.Subject;
                                classsub1.ClassId = i.Class;
                                context.ClassSubjects.Add(classsub1);
                                context.SaveChanges();
                                classsub = context.ClassSubjects.FirstOrDefault(s => s.SubjectId == i.Subject && s.ClassId == i.Class);

                            }
                            add.ClassSubjectId = classsub.ClassSubjectId;
                            add.SlotId = 4; 
                            add.RoomId = i.Room;
                            add.TeacherId = i.Teacher;
                            add.TeachingDay = date;
                            context.Schedules.Add(add);
                            context.SaveChanges();
                        }
                    }
                }

            }
            // Xử lý khi không có file hoặc file rỗng
            return RedirectToAction("Index");
        }

     
        private List<ScheduleRecord> GetSchedules(string file)
        {
            var list = new List<ScheduleRecord>();

            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + file;
            using(var reader =new StreamReader(path))
                using(var csv =new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var schedule = csv.GetRecord<ScheduleRecord>();
                    list.Add(schedule);

                }
            }
         

            return list;




        }

    }
            
}