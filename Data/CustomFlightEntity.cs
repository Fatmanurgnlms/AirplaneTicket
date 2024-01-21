namespace AirplaneTicketReservationSystem.Model
{
    public class CustomFlightEntity
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Time { get; set; }
        public int AircraftId { get; set; }
    }
}
