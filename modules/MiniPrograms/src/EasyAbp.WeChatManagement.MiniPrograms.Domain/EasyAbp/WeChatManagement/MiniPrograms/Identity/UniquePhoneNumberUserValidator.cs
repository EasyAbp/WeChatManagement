using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.WeChatManagement.MiniPrograms.Identity
{
    public class UniquePhoneNumberUserValidator : IUserValidator<IdentityUser>
    {
        public const string PhoneNumberStartsWithZeroErrorCode = "PhoneNumberStartsWithZero";
        public const string PhoneNumberStartsWithZeroDescription = "PhoneNumberStartsWithZero";
        public const string NonNumericPhoneNumberErrorCode = "NonNumericPhoneNumber";
        public const string NonNumericPhoneNumberDescription = "NonNumericPhoneNumber";
        public const string DuplicatePhoneNumberErrorCode = "DuplicatePhoneNumber";
        public const string DuplicatePhoneNumberDescription = "DuplicatePhoneNumber";

        private readonly IStringLocalizer<MiniProgramsResource> _localizer;
        private readonly IUniquePhoneNumberIdentityUserRepository _userRepository;

        public UniquePhoneNumberUserValidator(
            IStringLocalizer<MiniProgramsResource> localizer,
            IUniquePhoneNumberIdentityUserRepository userRepository)
        {
            _localizer = localizer;
            _userRepository = userRepository;
        }
        
        public virtual async Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
        {
            var errors = new List<IdentityError>();

            var phoneNumber = await manager.GetPhoneNumberAsync(user);

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return IdentityResult.Success;
            }

            CheckNotStartsWithZero(phoneNumber, errors);
            
            CheckIsNumeric(phoneNumber, errors);
            
            // PhoneNumber can be duplicated but confirmed PhoneNumber can't.
            if (user.PhoneNumberConfirmed)
            {
                await CheckIsNotDuplicateAsync(phoneNumber, manager, user, errors);
            }

            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }

        protected virtual async Task CheckIsNotDuplicateAsync(string phoneNumber, UserManager<IdentityUser> userManager,
            IdentityUser user, List<IdentityError> errors)
        {
            var owner = await _userRepository.FindByConfirmedPhoneNumberAsync(phoneNumber);
            
            if (owner != null &&
                !string.Equals(await userManager.GetUserIdAsync(owner), await userManager.GetUserIdAsync(user)))
            {
                errors.Add(new IdentityError
                {
                    Code = DuplicatePhoneNumberErrorCode,
                    Description = _localizer[DuplicatePhoneNumberDescription]
                });
            }
        }

        protected virtual void CheckIsNumeric(string phoneNumber, List<IdentityError> errors)
        {
            if (!phoneNumber.All(char.IsDigit))
            {
                errors.Add(new IdentityError
                {
                    Code = NonNumericPhoneNumberErrorCode,
                    Description = _localizer[NonNumericPhoneNumberDescription]
                });
            }
        }

        protected virtual void CheckNotStartsWithZero(string phoneNumber, List<IdentityError> errors)
        {
            if (phoneNumber.StartsWith("0"))
            {
                errors.Add(new IdentityError
                {
                    Code = PhoneNumberStartsWithZeroErrorCode,
                    Description = _localizer[PhoneNumberStartsWithZeroDescription]
                });
            }
        }
    }
}