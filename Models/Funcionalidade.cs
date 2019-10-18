using System.ComponentModel.DataAnnotations;

namespace crudmysql.Models
{
    public class Funcionalidade
    {
        [Key]
        public int IdFuncionalidade { get; set; }
        [Required(ErrorMessage = "Favor digitar o nome da funcionalidade")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Favor digitar a descricao da funcionalidade")]
        public string Descricao { get; set; }
        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "Favor selecionar o perfil da funcionalidade")]        
        public int IdPerfil { get; set; }
        public virtual Perfil Perfil { get; set; }

    }
}