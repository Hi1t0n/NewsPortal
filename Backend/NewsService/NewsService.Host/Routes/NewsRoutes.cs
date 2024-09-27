namespace NewsService.Host.Routes;

public static class NewsRoutes
{
    public static WebApplication AddNewsRouters(this WebApplication webApplication)
    {
        var newsGroup = webApplication.MapGroup("/api/news");
        
        return webApplication;
    }
}