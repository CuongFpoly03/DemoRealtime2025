namespace Realtime.Infrastructure.DTOs
{
    public class TodoSearchResultDto
    { 
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}