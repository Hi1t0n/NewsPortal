namespace PostService.Domain.Models;

public class PostCategory
{
    public Guid PostId = Guid.Empty;
    public Guid CategoryId = Guid.Empty;
    public Post Post { get; set; } = new Post();
    public Category Category { get; set; } = new Category();
}