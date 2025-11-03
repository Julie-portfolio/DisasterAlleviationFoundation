using System.ComponentModel.DataAnnotations;
using DisasterAlleviationApp.Models;

namespace DisasterAlleviationApp.Services
{
    public class DonationValidator
    {
        /// <summary>
        /// Validates a donation according to simple business rules:
        /// - Quantity must be greater than zero
        /// - Category must be non-empty
        /// - If DonorEmail is provided it must be a valid email address
        /// </summary>
        public bool IsValid(Donation donation)
        {
            if (donation == null) return false;
            if (donation.Quantity <= 0) return false;
            if (string.IsNullOrWhiteSpace(donation.Category)) return false;

            if (!string.IsNullOrWhiteSpace(donation.DonorEmail))
            {
                var emailAttr = new EmailAddressAttribute();
                if (!emailAttr.IsValid(donation.DonorEmail))
                    return false;
            }

            return true;
        }
    }
}
