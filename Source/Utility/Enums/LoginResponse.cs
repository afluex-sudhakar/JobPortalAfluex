namespace Utility.Enums
{
    public enum LoginResponse
    {
        Success = 1,
        InvalidUser = 2,
        InvalidPassword = 3,
        NotVerified = 4,
        Error = -1,
        Blocked = 5,
        InvalidDeviceId = 6
    }
}
