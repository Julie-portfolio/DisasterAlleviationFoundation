using System.Collections.Generic;
using System.Linq;
using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Services;

namespace DisasterAlleviationApp.Tests
{
    public class FakeDonationRepository : IDonationRepository
    {
        private readonly List<Donation> _list = new();
        private int _nextId = 1;

        public void Add(Donation donation)
        {
            if (donation.Id == 0) donation.Id = _nextId++;
            _list.Add(Clone(donation));
        }

        public Donation? GetById(int id) => _list.FirstOrDefault(d => d.Id == id);

        public IEnumerable<Donation> GetAll() => _list.Select(Clone);

        public void Update(Donation donation)
        {
            var idx = _list.FindIndex(d => d.Id == donation.Id);
            if (idx >= 0) _list[idx] = Clone(donation);
        }

        private static Donation Clone(Donation d) => new Donation
        {
            Id = d.Id,
            Category = d.Category,
            Quantity = d.Quantity,
            DonorName = d.DonorName,
            DonorEmail = d.DonorEmail,
            CreatedAt = d.CreatedAt,
            Status = d.Status
        };
    }
}
