using Microsoft.EntityFrameworkCore;
using Url_Shortener.Infrastructure;

namespace Url_Shortener.Services
{
    public class UrlShorteningService
    {
        public  const int NumberOfCharacter = 7;
        private const string Alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        private readonly Random _random = new();
        private readonly ApplicationDbContext _context;

        public UrlShorteningService( ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<string> GenerateUniqueCode() { 
        
            var charCode = new  char[NumberOfCharacter];

            while (true) {
                for (int i = 0; i < NumberOfCharacter; i++)
                {

                    var randomCharIndex = _random.Next(Alfabet.Length - 1);
                    charCode[i] = Alfabet[randomCharIndex];
                }

                var codeRandom = new string(charCode);
                if ( !await _context.shortenerUrls.AnyAsync(s => s.code == codeRandom))
                {
                    return codeRandom;
                }
            }

        }

    }
}
