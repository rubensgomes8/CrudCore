using System.ComponentModel.DataAnnotations;

namespace crudmysql.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Favor digitar o nome do usuario")]
        public string Nome { get; set; }
        [Range(0, 200, ErrorMessage = "Favor digitar uma idade entre 0 e 200.")]
        [Required(ErrorMessage = "Favor digitar a idade do usuario")]
        public int Idade { get; set; }
        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "Favor selecionar o perfil do usuario")]
        public int IdPerfil { get; set; }
        public virtual Perfil Perfil { get; set; }
    }
}