using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Dto
{
    public class MessagesDto
    {
        public int Id { get; set; }
        public int SenderID { get; set; }//ID của người gửi tin nhắn
        public int ReceiverID { get; set; }//ID của người nhận tin nhắn
        [Column(TypeName = "TEXT")]
        public string Content { get; set; }//lưu trữ nội dung của tin nhắn.
    }
}
