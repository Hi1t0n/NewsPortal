namespace PostService.Domain.Models;

public class Category
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
}