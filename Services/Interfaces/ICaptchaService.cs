namespace SchoolManagementSystem.Services.Interfaces
{
    public interface ICaptchaService
    {
        string GenerateCaptcha();
        bool ValidateCaptcha(string userInput, string captchaCode);
      
    }
}