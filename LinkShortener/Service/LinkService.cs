using LinkShortener.Data;
using LinkShortener.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LinkShortener.Service
{
    public class LinkService : ILinkService
    {
        private readonly MariaDbContext _ctx;
        public LinkService(MariaDbContext ctx)
        {
            _ctx = ctx;
        }

        //Create new link
        public async Task<int> CreateAsync(string raw)
        {
            raw = ValidateProtocol(raw);

            LinkModel link = new()
            {
                RawUrl = raw,
                ShortUrl = Shorten()
            };

            await _ctx.Links.AddAsync(link);

            return await _ctx.SaveChangesAsync();
        }

        //Delete existing link
        public async Task<int> DeleteAsync(int id)
        {
            var result = await _ctx.Links.FirstOrDefaultAsync(el => el.Id == id);

            if (result != null)
                _ctx.Links.Remove(result);

            return await _ctx.SaveChangesAsync();
        }

        //Get every entry in DB
        public async Task<IEnumerable<LinkModel>> GetAllAsync() => await _ctx.Links.ToListAsync();

        //Get entry by its Id property
        public async Task<LinkModel?> GetByIdAsync(int id)
        {
            var result = await _ctx.Links.FirstOrDefaultAsync(el => el.Id == id);

            return result;
        }

        //Get entry by its RawUrl property
        public async Task<LinkModel?> GetByUrlAsync(string raw)
        {
            raw = ValidateProtocol(raw);

            var result = await _ctx.Links.FirstOrDefaultAsync(el => el.RawUrl == raw);

            return result;
        }

        //Get RawUrl by given ShortUrl
        public async Task<LinkModel?> GetByTokenAsync(string token)
        {
            var result = await _ctx.Links.FirstOrDefaultAsync(el => el.ShortUrl == token);

            return result;
        }

        //Update link
        public async Task<int> UpdateAsync(string raw, int id)
        {
            raw = ValidateProtocol(raw);

            var result = await _ctx.Links.FirstOrDefaultAsync(el => el.Id == id);

            if (result != null)
            {
                result.RawUrl = raw;

                _ctx.Links.Update(result);

                return await _ctx.SaveChangesAsync();
            }

            return 0;
        }

        //Generate new ShortUrl token for selected link
        public async Task<int> RegenerateAsync(int id)
        {
            var result = await _ctx.Links.FirstOrDefaultAsync(el => el.Id == id);

            if (result != null)
            {
                result.ShortUrl = Shorten();

                _ctx.Links.Update(result);

                return await _ctx.SaveChangesAsync();
            }

            return 0;
        }

        //Increment Viewed property
        public async Task<int> IncrementViewed(LinkModel link)
        {
            link.Viewed++;

            return await _ctx.SaveChangesAsync();
        }

        //Check link for URL validity
        public bool ValidateUrl(string link)
        {
            //if (!Uri.TryCreate(link, UriKind.Absolute, out Uri result))
            //    return false;

            //return true;

            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(link);
        }

        //Add protocol if none (e.g. sample.com)
        public string ValidateProtocol(string link)
        {
            link = link.Trim();

            if (!Regex.IsMatch(link, @"^https?:\/\/", RegexOptions.IgnoreCase))
                link = "http://" + link;

            return link;
        }

        //Generating unique token from given elements
        public string Shorten()
        {
            const string Literals = "02356789BbCcDdEeFfGgHhJjKkLlMmNnOoPpQqRrSsTtVvWwXxYyZz"; //Elements for randomization
            const int Length = 6; //Desired token length

            using var generator = new RNGCryptoServiceProvider();

            var bytes = new byte[Length];
            generator.GetBytes(bytes);
            var chars = bytes
                .Select(b => Literals[b % Literals.Length]);
            var token = new string(chars.ToArray());
            return token;
        }
    }
}
