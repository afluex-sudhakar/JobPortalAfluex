using System;

namespace Utility
{
    public class Constants
    {
        public DateTime IST_DATE_TIME = DateTime.UtcNow.AddHours(5.30);
        public static string EncKey = "C1A79RLK0M1Or34A";
        public const int PAGE_SIZE = 5;

        public const string MAIL_FROM = "Career Mitra";
        public const string MAIL_FROM_EMAIL = "itofficer.mscl@gmail.com";
        public const string MAIL_PASSWORD = "Ascl@0532";
        public const string MAIL_SENT_SUCCESSFUL_MESSAGE = "Mail sent successfuly!";
        public const string SMS_SENT_SUCCESSFUL_MESSAGE = "SMS sent successfuly!";

        public const string HTTPSTATUS_FORGET_PASSWORD_SUCCESS = "A temporary password has been sent to!";
        public const string HTTPSTATUS_RECORD_FOUND = "Record found!";
        public const string HTTPSTATUS_RESET_PASSWORD_SUCCESS = "Password reseted successfully!";
        public const string HTTPSTATUS_CHANGE_PASSWORD_SUCCESS = "Password changed successfully!";
        public const string HTTPSTATUS_RESET_PASSWORD_EXPIRED = "Temporary Password expired!";
        public const string HTTPSTATUS_OTP_GENERATED = "OTP generated";
        public const string HTTPSTATUS_OTP_GENERATION_FAILED = "OTP generation failed";
        public const string HTTPSTATUS_RECORD_NOT_FOUND = "Record not found!";
        public const string HTTPSTATUS_SUCCESSFULLY_APPLIED_FOR_JOB = "You have successfully applied for this job !";
        public const string HTTPSTATUS_ALREADY_APPLIED_FOR_JOB = "You have already applied for this job !";
        public const string HTTPSTATUS_SUCCESSFULLY_APPLIED_FOR_ENROLLMENT_PROGRAM = "You have successfully applied for this enrollment program !";
        public const string HTTPSTATUS_ALREADY_APPLIED_FOR_ENROLLMENT_PROGRAM = "You have already applied for this job !";

        public const string HTTPSTATUS_SUCCESS = "Success!";
        public const string HTTPSTATUS_FAILED = "Failed!";
        public const string HTTPSTATUS_ALREADY_EXIST = "Already Exist!";
        public const string HTTPSTATUS_PROFILE_UPDATE_SUCCESS = "Profile updation successful!";
        public const string HTTPSTATUS_REGISTRATION_SUCCESSFUL = "Successfully registered!";
        public const string HTTPSTATUS_LOGIN_SUCCESSFUL = "Login successful!";
        public const string HTTPSTATUS_LOGOUT_SUCCESSFUL = "Logout successful!";
        public const string HTTPSTATUS_INVALID_PASSWORD = "Invalid password!";
        public const string HTTPSTATUS_INVALID_DEVICEID = "Invalid device!";
        public const string HTTPSTATUS_INVALID_USER = "Invalid user!";
        public const string HTTPSTATUS_BLOCKED_USER = "Blocked user!";
        public const string HTTPSTATUS_USER_NOT_VERIFIED = "User not verified!";
        public const string HTTPSTATUS_ERROR_OCCURED = "Error occured!";
        public const string HTTPSTATUS_OTP_EXPIRED = "OTP Expired!";
        public const string HTTPSTATUS_OTP_VALIDATION_SUCCESS = "OTP validation successful!";
        public const string HTTPSTATUS_OTP_VALIDATION_FAILED = "OTP validation failed!";

        public const string HTTPSTATUS_FORGET_PASSWORD_SUCCESS_HI = "एक अस्थायी पासवर्ड भेजा गया है!";
        public const string HTTPSTATUS_RECORD_FOUND_HI = "रिकॉर्ड मिला!";
        public const string HTTPSTATUS_RESET_PASSWORD_SUCCESS_HI = "पासवर्ड सफलतापूर्वक रीसेट किया गया!";
        public const string HTTPSTATUS_CHANGE_PASSWORD_SUCCESS_HI = "पासवर्ड सफलतापूर्वक बदला गया!";
        public const string HTTPSTATUS_RESET_PASSWORD_EXPIRED_HI = "अस्थायी पासवर्ड की समय सीमा समाप्त हो गई!";
        public const string HTTPSTATUS_OTP_GENERATED_HI = "ओटीपी जनरेट हुआ";
        public const string HTTPSTATUS_OTP_GENERATION_FAILED_HI = "ओटीपी जनरेट विफल रहा";
        public const string HTTPSTATUS_RECORD_NOT_FOUND_HI = "रिकॉर्ड नहीं मिला!";
        public const string HTTPSTATUS_SUCCESSFULLY_APPLIED_FOR_JOB_HI = "आपने इस नौकरी के लिए सफलतापूर्वक आवेदन किया है !";
        public const string HTTPSTATUS_ALREADY_APPLIED_FOR_JOB_HI = "आपने इस नौकरी के लिए पहले ही आवेदन कर दिया है !";
        public const string HTTPSTATUS_SUCCESSFULLY_APPLIED_FOR_ENROLLMENT_PROGRAM_HI = "आपने इस नामांकन कार्यक्रम के लिए सफलतापूर्वक आवेदन किया है !";
        public const string HTTPSTATUS_ALREADY_APPLIED_FOR_ENROLLMENT_PROGRAM_HI = "आपने इस नामांकन कार्यक्रमरी के लिए पहले ही आवेदन कर दिया है !";

        public const string HTTPSTATUS_SUCCESS_HI = "सफलता!";
        public const string HTTPSTATUS_FAILED_HI = "असफल हुआ!";
        public const string HTTPSTATUS_ALREADY_EXIST_HI = "पहले से ही मौजूद!";
        public const string HTTPSTATUS_PROFILE_UPDATE_SUCCESS_HI = "प्रोफाइल अपडेशन सफल!";
        public const string HTTPSTATUS_REGISTRATION_SUCCESSFUL_HI = "पंजीकरण सफलतापूर्वक हो गया है!";
        public const string HTTPSTATUS_LOGIN_SUCCESSFUL_HI = "लॉगिन की सफलता!";
        public const string HTTPSTATUS_LOGOUT_SUCCESSFUL_HI = "लॉगआउट सफल!";
        public const string HTTPSTATUS_INVALID_PASSWORD_HI = "अवैध पासवर्ड!";
        public const string HTTPSTATUS_INVALID_DEVICEID_HI = "अमान्य उपकरण!";
        public const string HTTPSTATUS_INVALID_USER_HI = "अमान्य उपयोगकर्ता!";
        public const string HTTPSTATUS_BLOCKED_USER_HI = "अवरुद्ध उपयोगकर्ता!";
        public const string HTTPSTATUS_USER_NOT_VERIFIED_HI = "उपयोगकर्ता सत्यापित नहीं है!";
        public const string HTTPSTATUS_ERROR_OCCURED_HI = "त्रुटि हुई!";
        public const string HTTPSTATUS_OTP_EXPIRED_HI = "ओटीपी का समय समाप्त हो गया!";
        public const string HTTPSTATUS_OTP_VALIDATION_SUCCESS_HI = "ओटीपी सत्यापन सफल!";
        public const string HTTPSTATUS_OTP_VALIDATION_FAILED_HI = "ओटीपी सत्यापन विफल हुआ!";

        public const string LOG_USER_PROFILE_UPDATE_Success = "Profile Updated";
        public const string LOG_USER_PROFILE_UPDATE = " : User profile update on dated : ";
        public const string LOG_USER_PROFILE_UPDATE_FAILED = " : User profile update on dated : ";
        public const string LOG_USER_PROFILE_UPDATE_FAIL = "Profile not Updated";
        public const string LOG_USER_PROFILE_UPDATE_FAILED_NO_RECORD = " : User profile update on dated : ";
        public const string LOG_USER_DOCUMENT_UPLOAD = " : User document uploaded on dated : ";
        public const string LOG_USER_DOCUMENT_UPLOAD_UPDATE = " : User document updated on dated : ";
        public const string LOG_USER_DOCUMENT_UPLOAD_FAILED = " Document upload failed on dated : ";
        public const string LOG_USER_LOGIN_ATTEMPT_SUCCESSFUL = " : User login attempt successful on dated : ";
        public const string LOG_USER_CHECK_LOGIN_ATTEMPT_SUCCESSFUL = " : User check login attempt successful on dated : ";
        public const string LOG_USER_LOG_OUT_ERROR = " : User logout error on dated : ";
        public const string LOG_USER_LOG_OUT_FAILED = " : User logout attempt failed on dated : ";
        public const string LOG_USER_LOGGED_OUT = " : User logout on dated : ";
        public const string LOG_USER_BLOCKED = " : User blocked on dated : ";
        public const string LOG_USER_DEVICEID_NOT_EXIST = " : User device id not exist on dated : ";
        public const string LOG_USER_NOT_FOUND_CHANGE_PASSWORD = " : User not exist for change password on dated : ";
        public const string LOG_USER_CHECK_LOGIN_ERROR = " : User check login error on dated : ";
        public const string LOG_USER_LOGIN_ATTEMPT_FAILED_UNAUTHORIZED = " Unauthorize invalid user : User Login attempt failed on dated : ";
        public const string LOG_USER_LOGIN_ATTEMPT_FAILED_UNAUTHORIZED_INVALID_PASSWORD = " Unauthorize invalid password : User Login attempt failed on dated : ";
        public const string LOG_USER_LOGIN_ATTEMPT_FAILED_NOT_VERIFIED = " User not verified : User Login attempt failed on dated : ";
        public const string LOG_USER_LOGIN_ATTEMPT_FAILED = " User Login attempt failed on dated : ";
        public const string LOG_OTP_VERIFICATION_FAILED_EXPIRED = " : OTP verification failed (expired) on dated : ";
        public const string LOG_TEMP_PASSWORD_VERIFICATION_FAILED_EXPIRED = " : Temp password verification failed (expired) on dated : ";
        public const string LOG_USER_LOGIN_FAILED = "Login or Password is incorrect";

        public const string LOG_OTP_VERIFICATION_SUCCESSFUL = " : OTP verification successful on dated : ";
        public const string LOG_RESET_PASSWORD_SUCCESSFUL = " : Reset password successful on dated : ";
        public const string LOG_OTP_VERIFICATION_FAILED = " : OTP verification failed on dated : ";
        public const string LOG_OTP_VERIFICATION_FAILED_INVALID = " : Invalid OTP! OTP verification failed on dated : ";
        public const string LOG_TEMP_PASSWORD_VERIFICATION_FAILED_INVALID = " : Invalid Temp Password! Temp password verification failed on dated : ";

        public const string LOG_TempPassword_VERIFICATION_FAILED_INVALID = " : Invalid Temporary Password! Temporary Password verification failed on dated : ";
        public const string LOG_TempPassword_VERIFICATION_SUCCESSFUL = " : Temporary Password Successful on dated : ";
        public const string LOG_TempPassword_VERIFICATION_Failed = "Temporary Password is Incorrect ";
        public const string LOG_TempPassword_generation_Failed = " : Temporary Password generation failed on dated : ";
        public const string LOG_Temporary_Password_generated = " : Temporary Password generated on dated : ";
        public const string LOG_Temporary_Password_Msg = "Your Temporary Password is ";


        public const string LOG_USER_REGISTRATION_MOBILENUMBER_ALREADYUSED = "Already Registered";

        public const string LOG_OTP_GENERATED = " : OTP generated on dated : ";
        public const string LOG_OTP_GENERATION_FAILED = " OTP generation failed on dated : ";
        public const string LOG_RESET_PASSWORD_FAILED = " Reset password failed on dated : ";
        public const string LOG_USER_REGISTERED = " : User registered on dated : ";
        public const string LOG_USER_REGISTRATION_ATTEMPT_FAILED = "User registration attempt failed on dated : ";
        public const string LOG_USER_VIEW_PROFILE_SUCCESS = " User profile viewed on dated : ";
        public const string LOG_USER_VIEW_PROFILE_FAILED = " User profile view failed on dated : ";
        public const string LOG_USER_DEVICE_REGISTRATION_FAILED = " : User device registration failed on dated : ";
        public const string LOG_USER_DEVICE_REGISTRATION_UPDATED = " : User device registration updated on dated : ";
        public const string LOG_USER_DEVICE_REGISTRATION_ADDED = " : User device registration added on dated : ";

        public const string LOG_JOB_SEARCH_FAILED = " Job search attempt failed on dated : ";
        public const string LOG_JOB_SEARCH_SUCCESS = " Job search attempt success on dated : ";
        public const string LOG_JOB_DETAIL_FAILED = " Job detail fetch failed on dated : ";
        public const string LOG_JOB_DETAIL_SUCCESS = " Job detail fetch success on dated : ";
        public const string LOG_APPLY_JOB_ALREADY_APPLIED = " Job apply : already applied on dated : ";
        public const string LOG_APPLY_JOB_SUCCESSFUL = " Job apply : job applied successfully on dated : ";
        public const string LOG_APPLY_JOB_FAILED = " Job apply : apply job failed on dated : ";
        public const string LOG_CHECK_MOBILE_NO_EXISTANCE = " Check mobile no on dated : ";
        public const string LOG_CHECK_MOBILE_NO_EXISTANCE_FAILED = " Check mobile no failed on dated : ";

        public const string LOG_APPLY_ENROLLMENT_PROGRAM_ALREADY_APPLIED = " Enrollment Program apply : already applied on dated : ";
        public const string LOG_APPLY_ENROLLMENT_PROGRAM_SUCCESSFUL = " Enrollment Program apply : Enrollment Program applied successfully on dated : ";
        public const string LOG_APPLY_ENROLLMENT_PROGRAM_FAILED = " Enrollment Program apply : apply Enrollment Program failed on dated : ";

        public const string LOG_OldPassword_VERIFICATION_FAILED_INVALID = " : Invalid Old Password! Old Password verification failed on dated : ";
        public const string LOG_OldPassword_VERIFICATION_Success = " : Old Password Successful on dated : ";
        public const string LOG_OldPassword_VERIFICATION_Failed = "Old Password is Incorrect";

        public const string LOG_PASSWORD_CHANGE_SUCCESSFUL = " Password change successfully";
        public const string LOG_PASSWORD_CHANGE_FAILED = " Password change failed";

        public const string LOG_UPDATE_PROFILE_PERSONAL_INFO_SUCCESS = " Update profile personal info success";
        public const string LOG_UPDATE_PROFILE_PERSONAL_INFO_USER_NOT_FOUND = " Update profile personal info user not found";
        public const string LOG_UPDATE_PROFILE_PERSONAL_INFO_FAILED = " Update profile personal info failed";

        public const string LOG_UPDATE_PROFILE_COMPANY_INFO_SUCCESS = " Update profile company detail success";
        public const string LOG_UPDATE_PROFILE_COMPANY_INFO_USER_NOT_FOUND = " Update profile company detail user not found";
        public const string LOG_UPDATE_PROFILE_COMPANY_INFO_FAILED = " Update profile company detail failed";

        public const string LOG_UPDATE_PROFILE_ABOUT_SUCCESS = " Update profile about success";
        public const string LOG_UPDATE_PROFILE_ABOUT_USER_NOT_FOUND = " Update profile about user not found";
        public const string LOG_UPDATE_PROFILE_ABOUT_FAILED = " Update profile about failed";

        public const string LOG_UPDATE_PROFILE_SKILLS_SUCCESS = " Update profile skills success";
        public const string LOG_UPDATE_PROFILE_SKILLS_USER_NOT_FOUND = " Update profile skills user not found";
        public const string LOG_UPDATE_PROFILE_SKILLS_FAILED = " Update profile skills failed";

        public const string LOG_UPDATE_PROFILE_EDUCATION_SUCCESS = " Update profile education success";
        public const string LOG_UPDATE_PROFILE_EDUCATION_USER_NOT_FOUND = " Update profile education user not found";
        public const string LOG_UPDATE_PROFILE_EDUCATION_FAILED = " Update profile education failed";

        public const string LOG_UPDATE_PROFILE_EXPERIENCE_SUCCESS = " Update profile experience success";
        public const string LOG_UPDATE_PROFILE_EXPERIENCE_USER_NOT_FOUND = " Update profile experience user not found";
        public const string LOG_UPDATE_PROFILE_EXPERIENCE_FAILED = " Update profile experience failed";

        public const string LOG_UPDATE_PROFILE_PHOTO_SUCCESS = " Update profile photo success";
        public const string LOG_UPDATE_PROFILE_PHOTO_USER_NOT_FOUND = " Update profile photo user not found";
        public const string LOG_UPDATE_PROFILE_PHOTO_FAILED = " Update profile photo failed";

        public const string LOG_UPDATE_PROFILE_RESUME_SUCCESS = " Update profile resume success";
        public const string LOG_UPDATE_PROFILE_RESUME_USER_NOT_FOUND = " Update profile resume user not found";
        public const string LOG_UPDATE_PROFILE_RESUME_FAILED = " Update profile resume failed";


        public const string BASE_URL = "https://careermitra.moradabadsmartcity.org/";
        public const string PROFILE_PIC_URL = BASE_URL + "FileUpload/ProfilePhoto/";
        public const string RESUME_URL = BASE_URL + "FileUpload/Resume/";
        public const string TRAINING_MATERIAL_URL = BASE_URL + "FileUpload/Other/";
        public const string LOG_JOB_POST_SUCCESS = "Successfully Job Posted !";
        public const string LOG_JOB_POST_FAILED = "Job Post Failed !";


        public const string MONTHS_ENGLISH = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec";
        public const string CMS_Page_SUCCESS = "CMS Page Details Save Successfull !";
        public const string CMS_Page_UPdate = "CMS Page Details Update Successfull !";
    }
}
