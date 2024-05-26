using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Model
{
    public class Messages
    {
        public int Id { get; set; }
        public int SenderID { get; set; }//ID của người gửi tin nhắn
        public int ReceiverID { get; set; }//ID của người nhận tin nhắn
        public string Content { get; set; }//lưu trữ nội dung của tin nhắn.
        public DateTime SentTime { get; set; }//lưu trữ thời gian gửi tin nhắn

    }
}
