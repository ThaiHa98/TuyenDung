using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Services.InterfaceIServices
{
    public interface IMessagesIService
    {
        public Messages Crate(MessagesDto messageDto);
        string Update(MessagesDto messageDto);
        bool Delete(int id);
    }
}
