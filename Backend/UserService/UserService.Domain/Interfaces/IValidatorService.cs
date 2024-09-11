namespace UserService.Domain.Interfaces;

public interface IValidatorService
{
    Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken);
    Task<bool> BeUniquePhoneNumber(string phoneNumber, CancellationToken cancellationToken);
    Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken);
    Task<bool> BeUserExist(Guid id, CancellationToken cancellationToken);
}