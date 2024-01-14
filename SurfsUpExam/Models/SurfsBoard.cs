using System.ComponentModel.DataAnnotations;

namespace SurfsUpExam.Models
{
    public class SurfsBoard
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Thickness { get; set; }
        public double? Volume { get; set; }
        public string? BoardType { get; set; }
        public decimal Price { get; set; }
        public string? Equipment { get; set; }
    }
}
