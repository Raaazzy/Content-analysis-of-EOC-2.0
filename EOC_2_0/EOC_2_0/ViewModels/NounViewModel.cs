using System.ComponentModel.DataAnnotations;
using EOC_2_0.Data.Models;

namespace EOC_2_0.ViewModels
{
    public class NounViewModel
    {
        public long Id { get; set; }


        [Display(Name = "Существительное")]
        [Required(ErrorMessage = "Введите текст результата")]
        public string Word { get; set; }


        [Display(Name = "Глагол")]
        public int IdVerb { get; set; }
    }
}
