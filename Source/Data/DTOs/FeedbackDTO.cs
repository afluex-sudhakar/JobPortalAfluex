namespace Data.DTOs
{
    public class FeedbackDTO : UserLogDTO
    {
        public int? UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
    }
}
