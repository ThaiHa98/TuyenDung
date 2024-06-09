using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.Service.Services.RepositoryServices
{
    public class MessagesService : IMessagesIService
    {
        private readonly MyDb _dbContext;
        public MessagesService(MyDb dbContext)
        {
           _dbContext = dbContext;
        }
        public Messages Crate(MessagesDto messageDto)
        {
            try
            {
                var employers = _dbContext.Employers.FirstOrDefault(x => x.Id == messageDto.SenderID);
                if (employers == null) 
                {
                    throw new Exception("Employers not found");
                }
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == messageDto.ReceiverID);
                if(user == null)
                {
                    throw new Exception("User not found");
                }
                Messages messages = new Messages
                {
                    Content = messageDto.Content,
                };
                return messages;
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating a Messages", ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var messages = _dbContext.Messages.FirstOrDefault(x => x.Id == id);
                if(messages == null)
                {
                    throw new Exception("Messages not found");
                }
                _dbContext.Remove(messages);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while create messages in", ex);
            }
        }

        public string Update(MessagesDto messageDto)
        {
            try
            {
                var message = _dbContext.Messages.FirstOrDefault(x => x.Id == messageDto.Id);
                if(message == null)
                {
                    throw new Exception("message not found");
                }
                message.Content = messageDto.Content;
                return "Update Successfully";
            }
            catch( Exception ex)
            {
                throw new Exception("There was an error updating the message",ex);
            }
        }
    }
}
