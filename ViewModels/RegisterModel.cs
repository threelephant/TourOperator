using System.ComponentModel.DataAnnotations;

namespace TourOperator.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }
    }
}