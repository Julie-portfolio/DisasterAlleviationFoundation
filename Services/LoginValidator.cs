namespace DisasterAlleviationApp.Services
{
    public class LoginValidator
    {
        // Simple validation rules for testing
        public bool IsValid(string? username, string? password)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password.Length < 6) return false; // minimum length
            return true;
        }
    }
}
