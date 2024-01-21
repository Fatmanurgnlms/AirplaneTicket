namespace AirplaneTicketReservationSystem.Model
{
    public class CustomLocationEntity
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Airport { get; set; }
        public bool IsActive { get; set; }
    }
}
