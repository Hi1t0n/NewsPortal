namespace NewsService.Domain.Models;

public class News
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime DateTime { get; set; } = DateTime.UtcNow;
}