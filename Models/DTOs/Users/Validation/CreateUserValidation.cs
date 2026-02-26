using FluentValidation;

namespace MITCRMS.Models.DTOs.Users.Validation
{
    public class CreateUserValidation: AbstractValidator<CreateUserRequestModel>
    {
        public CreateUserValidation()
        { 
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format");
            RuleFor(x => x.PasswordHash)
             .NotEmpty().WithMessage("Password is required.")
             .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
             .MaximumLength(50).WithMessage("Password must not exceed 50 characters.")
             .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
             .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
             .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
             .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.PasswordHash).WithMessage("Confirm password must match the password.");
            RuleFor(x => x.DepartmentId).NotEmpty().WithMessage("Department is required");
        RuleFor(x => x.RoleIds).NotEmpty().WithMessage("At least one role must be selected");
    }
    }
}
