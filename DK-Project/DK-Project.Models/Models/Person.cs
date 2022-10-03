namespace DK_Project.Models.Models
{
    public record Person
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
        public DateTime DateOfBirth { get; set; }
    }
}