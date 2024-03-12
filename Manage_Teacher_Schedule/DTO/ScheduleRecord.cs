using CsvHelper.Configuration.Attributes;

namespace Manage_Teacher_Schedule.DTO
{
    public class ScheduleRecord
    {
        [Index(0)]
        public string Class { get; set; } = "";
        [Index(1)]
        public string Subject { get; set; } = "";
        [Index(2)]
        public string Room { get; set; } = "";
        [Index(3)]
        public string Teacher { get; set; } = "";
        [Index(4)]
        public string TimeSlot { get; set; } = "";
        [Index(5)]
        public string StartDate { get; set; } = "";
        [Index(6)]
        public string EndDate { get; set; } = "";
    }
}
