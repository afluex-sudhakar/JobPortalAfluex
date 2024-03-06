using System;
using System.Collections.Generic;

namespace Data.DTOs
{
    public class MessageRequestDTO : UserLogDTO
    {
        public int UserId { get; set; }
    }

    public class MessageListResponseDTO
    {
        public int Id { get; set; }
        public List<MessageHeadDTO> Messages { get; set; }
    }

    public class MessageHeadDTO
    {
        public int? EmployerId { get; set; }
        public int? JobSeekerId { get; set; }
        public int? JobId { get; set; }
        public int Id { get; set; }
        public string EmployerName { get; set; }
        public string CompanyName { get; set; }
        public string JobSeekerName { get; set; }
        public string Job { get; set; }
        public string LastMessage { get; set; }
        public bool IsUnread { get; set; }
        public int UnReadCount { get; set; }
        public DateTime Date { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }


    public class MessageChatRequestDTO : UserLogDTO
    {
        public int Id { get; set; }
        public int JobSeekerId { get; set; }
        public int UserId { get; set; }
    }

    public class MessageChatListResponseDTO
    {
        public List<MessageDTO> Messages { get; set; }
    }

    public class MessageDTO
    {
        public int? SenderId { get; set; }
        public int? JobId { get; set; }
        public string EmployerName { get; set; }
        //public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public bool IsUnread { get; set; }
        public DateTime Date { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }

    public class MessageSendRequestDTO : UserLogDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }


    public class MessageSendEmployerRequestDTO : MessageSendRequestDTO
    {
        public int? Employer { get; set; }
        public int? JobId { get; set; }
        public int? JobSeeker { get; set; }  
        public string Title { get; set; }
        public string UserName { get; set; }
        public string ExperienceMax { get; set; }
        public string ExperienceMin { get; set; }
        public string JobLocation { get; set; }
        public string JobType { get; set; }
        public string City { get; set; }
        public string ShortDescription { get; set; }
        public List<MessageDTO> lstMessage { get; set; }
        public List<MessageHeadDTO> MessageList { get; set; }
    }

    public class MessageSendUserRequestDTO : MessageSendRequestDTO
    {
        public int? Employer { get; set; }
        public int? JobId { get; set; }
        public int? JobSeeker { get; set; }
        public int ChatId { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string ExperienceMax { get; set; }
        public string ExperienceMin { get; set; }
        public string JobType { get; set; }
        public string City { get; set; }
        public string ShortDescription { get; set; }
        public string Location { get; set; }
        public List<MessageDTO> lstMessage { get; set; }
        public List<MessageHeadDTO> MessageList { get; set; }
    }
}
