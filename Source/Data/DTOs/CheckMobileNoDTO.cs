namespace Data.DTOs
{
    public class CheckMobileNoDTO : UserLogDTO
    {
        public string MobileNo { get; set; }
    }
    public class CheckMobileNoResponseDTO
    {
        public bool IsExist { get; set; }
    }
}
