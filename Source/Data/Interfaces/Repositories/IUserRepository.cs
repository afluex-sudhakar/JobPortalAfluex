using Data.DTOs;
using System.Collections.Generic;
using Utility.Enums;

namespace Data.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        bool Add(UserDTO req);

        bool Update(UpdateUserDTO req);

        bool Delete(int id, int userId);

        new List<User> GetAll();
        User GetDetailById(int Id);

        List<EmployerPlacementDTO> GetEmployerWisePlacement(int EmployerId);
        int RegisterUser(UserRegistrationDTO req);
        bool RegisterDevice(RegisterDeviceDTO req);
        bool UpdateProfile(UPdateProfileDTO req);
        bool UploadDocument(UploadDocumentDTO req);
        UserLoginResponseWEBDTO LoginWeb(UserLoginDTO req);
        UserDetailResponseDTO LoginMobile(UserLoginMobileDTO req, out LoginResponse status);
        UserDetailResponseDTO CheckLoginMobile(UserLogDTO req, out LoginResponse status);
        int ValidateOTP(ValidateOTPDTO req);
        bool GenerateOTP(GenerateOTPDTO req);
        bool GenerateTemporaryPassword(GenerateOTPDTO req);
        UserProfileResponseDTO GetProfileData(UserProfileReqDTO req);
        int ValidateTemporaryPassword(UserDTO req);
        // bool ChangePassword(UserDTO req);

        int CheckMobileNo(CheckMobileNoDTO req);
        bool GenerateOTPMobile(GenerateOTPDTO req);
        int ValidateOTPMobile(ValidateOTPDTO req);
        UserDetailResponseDTO RegisterUserMobile(UserRegistrationDTO req);

        bool ResetPassword(UserDTO req);
        int ValidateOldPassword(UserDTO req);
        bool UpdateUser(UpdateUserDTO req);
        bool ChangePassword(ChangePasswordDTO req);
        UserLoginResponseDTO AdminLogin(UserLoginDTO req);

        bool ForgetPasswordMobile(ForgetPasswordDTO req);

        int ResetPasswordMobile(ResetPasswordDTO req);
        bool ChangePasswordMobile(ChangePasswordMobileDTO req);

        bool UpdateProfilePersonalDetailMobile(UPdateProfilePersonalDetailDTO req);
        bool UpdateProfileCompanyDetailMobile(UPdateProfileCompanyDetailDTO req);
        bool UpdateProfileAboutMobile(UPdateProfileAboutDTO req);

        bool UpdateProfileSkillsMobile(UpdateProfileSkillsDTO req);

        bool UpdateProfileEducationMobile(UpdateProfileEducationDTO req);

        bool UpdateProfileExperienceMobile(UpdateProfileExperienceDTO req);

        bool UpdateProfilePic(int Id, string fileName, string DeviceId, string Lat, string Long, string Address);

        bool UpdateResume(int Id, string fileName, string DeviceId, string Lat, string Long, string Address);
        bool SaveUserEducation(UpdateUserDTO obj);
        bool DeleteUserEducation(UpdateUserDTO obj);

        bool SaveUserExperience(UpdateUserDTO req);

        bool ApproveEmployer(int Id, int UserId);

        bool AddEmployee(UpdateUserDTO model);

        bool LogoutMobile(UserLogoutDTO req);
        bool UpdateProfilePicWeb(UPdateProfileDTO model);

       List<UserNotificationDTO> GetFCMIdofUsers(string MobileNo);
    }
}
