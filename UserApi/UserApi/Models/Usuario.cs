using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {get; set;}
        public string email {get; set;}
        public string password {get;set;}
        public bool activo {get; set;}
        
        public Usuario(){}
        public Usuario(int id, string email, string password, bool activo)
        {
            this.id = id;
            this.email = email;
            this.password = password;
            this.activo = activo;
        }
    }    
}