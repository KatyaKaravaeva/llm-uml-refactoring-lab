namespace BadDesignApp.Services.Validation;

// Устранено нарушение SRP - отдельный класс для валидации пользователя
public class UserValidator : IUserValidator
{
    private const int MinUserIdLength = 5;
    
    public ValidationResult Validate(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return ValidationResult.Failure("User ID cannot be empty");
        }
        
        if (userId.Length < MinUserIdLength)
        {
            return ValidationResult.Failure($"User ID must be at least {MinUserIdLength} characters long");
        }
        
        return ValidationResult.Success();
    }
}

