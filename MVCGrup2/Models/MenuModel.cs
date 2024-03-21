using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Models
{
    public class MenuModel
    {
        [Required(ErrorMessage = "Menü adı boş olamaz.")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Menü fiyatı boş olamaz.")]
        public decimal? Fiyat { get; set; }
        public IFormFile Resim
        {
            get; set;

        }
    }
}
