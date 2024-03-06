using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface IChatRepository : IRepositoryBase<Chat>
    {
        MessageListResponseDTO GetMessageList(MessageRequestDTO req);
        MessageChatListResponseDTO GetMessageChats(MessageChatRequestDTO req);
        bool SendMessage(MessageSendRequestDTO req);
        MessageListResponseDTO GetMessageListEmployer(MessageRequestDTO req);
        MessageChatListResponseDTO GetMessageChatsEmployer(MessageChatRequestDTO req);
        bool SendMessageEmployer(MessageSendEmployerRequestDTO req);
        int GetUnreadMessgeCount(int EmployerId);
    }
}
