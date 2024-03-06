using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Linq;
using Utility;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public MessageListResponseDTO GetMessageList(MessageRequestDTO req)
        {
            try
            {
                MessageListResponseDTO res = new MessageListResponseDTO();
                var data = GetAll(x => x.IsDeleted == false && x.JobSeeker == req.UserId).Select(x => new MessageHeadDTO
                {
                    Date = x.CreatedAt,
                    EmployerId = x.Employer,
                    CompanyName = x.Employer != null ? x.User.UserDetails.Select(xx => xx.CompanyName).FirstOrDefault():"",
                    EmployerName = x.Employer != null ? x.User.UserDetails.Select(xx => xx.FirstName).FirstOrDefault() : "",
                    Id = x.Id,
                    LastMessage = x.ChatMessages.Where(xx => xx.IsDeleted == false).OrderByDescending(xx => xx.Id).Select(xx => xx.Message).FirstOrDefault(),
                    IsUnread = x.ChatMessages.Where(xx => xx.IsDeleted == false && xx.IsUnRead == true && xx.Employer != null).Any(),
                    UnReadCount = x.ChatMessages.Where(xx => xx.IsDeleted == false && xx.IsUnRead == true && xx.Employer != null).Count(),

                    Job = x.Job != null ? x.Job.Title : "",
                    JobId = x.Job.Id
                }).ToList();
                res.Messages = data;
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public MessageChatListResponseDTO GetMessageChats(MessageChatRequestDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                MessageChatListResponseDTO res = new MessageChatListResponseDTO();
                var data = db.ChatMessages.Where(xx => xx.IsDeleted == false && xx.Chat.Id == req.Id && xx.Chat.IsDeleted == false).Select(xx => new MessageDTO
                {
                    Date = xx.CreatedAt,
                    SenderId = xx.Employer != null ? xx.Employer : xx.JobSeeker,
                    //UserId = xx.JobSeeker,
                    EmployerName = xx.Employer != null ? xx.User.UserDetails.Select(y => y.FirstName).FirstOrDefault() : "",
                    UserName = xx.JobSeeker != null ? xx.User.UserDetails.Select(y => y.FirstName).FirstOrDefault() : "",
                    IsUnread = xx.IsUnRead,
                    Message = xx.Message,
                    UpdatedTime=xx.ModifiedAt
                }).ToList();
                 
                if (data != null && data.Count > 0)
                {
                    var dd = GetAll(x => x.IsDeleted == false && x.Id == req.Id).FirstOrDefault();
                    if (dd != null)
                    {
                        var cm = dd.ChatMessages.Where(xx => xx.IsDeleted == false && xx.Employer != null).ToList();
                        foreach (var item in cm)
                        {
                            item.IsUnRead = false;
                            //item.ModifiedAt = createdAt;
                        }
                        Update(dd);
                    }
                }

                res.Messages = data;
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool SendMessage(MessageSendRequestDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var data = GetAll(x => x.IsDeleted == false && x.Id == req.Id).FirstOrDefault();
                if (data != null)
                {
                    ChatMessage cm = new ChatMessage();
                    cm.CreatedAt = createdAt;
                    cm.ModifiedAt = createdAt;
                    cm.IsDeleted = false;
                    cm.IsUnRead = true;
                    cm.JobSeeker = req.UserId;
                    cm.Message = req.Message;
                    //cm.Employer = req.EmployerId;
                    data.ChatMessages.Add(cm);
                    Update(data);
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public MessageListResponseDTO GetMessageListEmployer(MessageRequestDTO req)
        {
            try
            {
                MessageListResponseDTO res = new MessageListResponseDTO();
                var data = GetAll(x => x.IsDeleted == false && x.Employer == req.UserId).Select(x => new MessageHeadDTO
                {
                    Date = x.CreatedAt,
                    JobSeekerId = x.JobSeeker,
                    JobId = x.JobId,
                    JobSeekerName = x.JobSeeker != null ? x.User1.UserDetails.Select(xx => xx.FirstName).FirstOrDefault() : "",
                    Id = x.Id,
                    LastMessage = x.ChatMessages.Where(xx => xx.IsDeleted == false).OrderByDescending(xx => xx.Id).Select(xx => xx.Message).FirstOrDefault(),
                    IsUnread = x.ChatMessages.Where(xx => xx.IsDeleted == false && xx.IsUnRead == true && xx.JobSeeker != null).Any(),
                    UnReadCount = x.ChatMessages.Where(xx => xx.IsDeleted == false && xx.IsUnRead == true && xx.JobSeeker != null).Count(),
                    Job = x.Job != null ? x.Job.Title : "",
                    UpdatedTime = x.ModifiedAt
                }).ToList();
                res.Messages = data;
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public MessageChatListResponseDTO GetMessageChatsEmployer(MessageChatRequestDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                MessageChatListResponseDTO res = new MessageChatListResponseDTO();
                var data = db.ChatMessages.Where(xx => xx.IsDeleted == false && xx.Chat.Employer == req.UserId && xx.Chat.JobId == req.Id && xx.Chat.JobSeeker == req.JobSeekerId && xx.Chat.IsDeleted == false).Select(xx => new MessageDTO
                {
                    Date = xx.CreatedAt,
                    SenderId = xx.Employer != null ? xx.Employer : xx.JobSeeker,
                    //UserId = xx.JobSeeker,
                    EmployerName = xx.Employer != null ? xx.User.UserDetails.Select(y => y.FirstName).FirstOrDefault() : "",
                    UserName = xx.JobSeeker != null ? xx.User.UserDetails.Select(y => y.FirstName).FirstOrDefault() : "",
                    IsUnread = xx.IsUnRead,
                    Message = xx.Message,
                    UpdatedTime = xx.ModifiedAt
                }).ToList();
                if (data != null && data.Count > 0)
                {
                    var dd = GetAll(x => x.IsDeleted == false && x.Employer == req.UserId && x.JobId == req.Id && x.JobSeeker == req.JobSeekerId && x.IsDeleted == false).FirstOrDefault();
                    if (dd != null)
                    {
                        var cm = dd.ChatMessages.Where(xx => xx.IsDeleted == false && xx.JobSeeker != null).ToList();
                        foreach (var item in cm)
                        {
                            item.IsUnRead = false;
                            //item.ModifiedAt = createdAt;
                        }
                        Update(dd);
                    }
                }
                res.Messages = data;
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool SendMessageEmployer(MessageSendEmployerRequestDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                var data = GetAll(x => x.IsDeleted == false && x.JobId == req.Id && x.JobSeeker == req.UserId && x.Employer == req.Employer).FirstOrDefault();
                if (data != null)
                {
                    ChatMessage cm = new ChatMessage();
                    cm.CreatedAt = createdAt;
                    cm.ModifiedAt = createdAt;
                    cm.IsDeleted = false;
                    cm.IsUnRead = true;
                    cm.Message = req.Message;
                    cm.Employer = req.Employer;
                    data.ChatMessages.Add(cm);
                    Update(data);
                    return true;
                }
                else
                {
                    Chat c = new Chat();
                    c.IsDeleted = false;
                    c.CreatedAt = createdAt;
                    c.ModifiedAt = createdAt;
                    c.Employer = req.Employer;
                    c.JobSeeker = req.UserId;
                    c.JobId = req.Id;

                    ChatMessage cm = new ChatMessage();
                    cm.CreatedAt = createdAt;
                    cm.ModifiedAt = createdAt;
                    cm.IsDeleted = false;
                    cm.IsUnRead = true;
                    cm.Message = req.Message;
                    cm.Employer = req.Employer;
                    c.ChatMessages.Add(cm);
                    Add(c);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetUnreadMessgeCount(int EmployerId)
        {
            try
            {
                var data = db.ChatMessages.Where(x => x.IsDeleted == false && x.Chat.Employer == EmployerId && x.Chat.IsDeleted == false && x.IsUnRead == true && x.JobSeeker != null && x.Employer == null).Count();  
                return data;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
