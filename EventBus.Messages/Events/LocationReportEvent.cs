namespace EventBus.Messages.Events
{
    public class LocationReportEvent
    {
        public string Location { get; set; }
        public int PeopleCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
