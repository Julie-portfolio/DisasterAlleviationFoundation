using System.Collections.Generic;
using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Services
{
    public interface IDonationRepository
    {
        void Add(Donation donation);
        Donation? GetById(int id);
        IEnumerable<Donation> GetAll();
        void Update(Donation donation);
    }
}
