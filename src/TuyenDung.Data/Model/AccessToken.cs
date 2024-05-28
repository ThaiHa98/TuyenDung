using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Model.Enum;

namespace TuyenDung.Data.Model
{
    public class AccessToken
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? AcessToken { get; set; }
        public StatusToken Status { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
