namespace ECI.Test.BL.Services.Interfaces
{
    public interface IUserService
    {
        bool ValidateCredentials(string userName, string password);
    }
}