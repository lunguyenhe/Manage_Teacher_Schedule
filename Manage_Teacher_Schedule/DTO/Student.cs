using CsvHelper.Configuration.Attributes;

namespace Manage_Teacher_Schedule.DTO
{
    public class Student
    {
        [Index(0)]
        public string Name { get; set; } = "";
        [Index(2)]
        public string Roll { get; set; } = "";
        [Index(1)]
        public string Email { get; set; } = "";
    }
}
