using System.Collections.Generic;
using System.Linq;
using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Services
{
    public class DonationSummaryService
    {
        public IDictionary<string, int> SummarizeByCategory(IEnumerable<Donation> donations)
        {
            if (donations == null) return new Dictionary<string, int>();

            return donations
                .GroupBy(d => (d.Category ?? string.Empty).Trim().ToLowerInvariant())
                .Where(g => !string.IsNullOrEmpty(g.Key))
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Quantity));
        }
    }
}
