using MessagePack;

namespace DK_Project.Models.Models
{
    [MessagePackObject]
    public record Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}