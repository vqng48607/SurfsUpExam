namespace SurfsUpExam.Models
{
    public class Renting
    {
        public int Id { get; set; }
        public DateTime RentingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? SurfboardId { get; set; }
        public string? UserId { get; set; }
    }
}
