using System.ComponentModel.DataAnnotations;

namespace FinaFlow.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Titulo invalido")]
        [MaxLength(80, ErrorMessage = "O titulo de conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição invalida")]
        public string Description { get; set; } = string.Empty;
    }
}
