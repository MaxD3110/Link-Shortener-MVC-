using LinkShortener.Models.Domain;

namespace LinkShortener.Service
{
    public interface ILinkService
    {
        public Task<IEnumerable<LinkModel>> GetAllAsync();
        public Task<LinkModel?> GetByIdAsync(int id);
        public Task<LinkModel?> GetByUrlAsync(string raw);
        public Task<LinkModel?> GetByTokenAsync(string token);
        public Task<int> IncrementViewed(LinkModel link);
        public Task<int> UpdateAsync(string raw, int id);
        public Task<int> CreateAsync(string raw);
        public Task<int> DeleteAsync(int id);
        public Task<int> RegenerateAsync(int id);
        public bool ValidateUrl(string link);
    }
}
