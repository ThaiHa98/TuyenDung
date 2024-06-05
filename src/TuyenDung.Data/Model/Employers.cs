using System.ComponentModel.DataAnnotations;

namespace TuyenDung.Data.Model
{
    public class Employers //Nhà tuyển dụng
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Industry { get; set; } // Ngành công nghiệp
        public string Website {  get; set; }  //web của công ty
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactPosition { get; set; }//Liên hệ vị trí
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
