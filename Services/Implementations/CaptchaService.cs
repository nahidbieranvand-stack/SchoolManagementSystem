using SchoolManagementSystem.Services.Interfaces;

namespace SchoolManagementSystem.Services.Implementations
{
    public class CaptchaService : ICaptchaService
    {
        private const string Characters =
            "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        public string GenerateCaptcha()
        {
            var random = new Random();

            return new string(
                Enumerable.Range(0, 5)
                    .Select(_ => Characters[random.Next(Characters.Length)])
                    .ToArray());
        }

        public bool ValidateCaptcha(string userInput, string captchaCode)
        {
            if (string.IsNullOrWhiteSpace(userInput) ||
                string.IsNullOrWhiteSpace(captchaCode))
            {
                return false;
            }

            return string.Equals(
                userInput.Trim(),
                captchaCode,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}