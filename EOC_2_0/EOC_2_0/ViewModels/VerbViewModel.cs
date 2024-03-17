using System.ComponentModel.DataAnnotations;
using EOC_2_0.Data.Models;

namespace EOC_2_0.ViewModels
{
    public class VerbViewModel
    {
        public long Id { get; set; }


        [Display(Name = "Глагол")]
        [Required(ErrorMessage = "Введите текст результата")]
        public string Word { get; set; }


        [Display(Name = "Уровень классификации")]
        public int Level { get; set; }
    }
}
