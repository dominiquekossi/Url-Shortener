namespace Url_Shortener.Entites
{
    public class ShortenerUrl
    {
        public Guid Id { get; set; }
        public string LongUrl { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty ;
        public string code {  get; set; } = string.Empty ;
        public DateTime CreateAt { get; set; }
    }
}
