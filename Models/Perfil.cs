using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace crudmysql.Models
{
    public class Perfil
    {
        [Key]
        public int IdPerfil { get; set; }
        [Required(ErrorMessage = "Favor digitar o nome do perfil")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Favor digitar a descricao do perfil")]
        public string Descricao { get; set; }
        public virtual ICollection<Funcionalidade> Funcionalidades { get; set; }
    }
}