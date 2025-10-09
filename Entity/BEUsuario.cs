using Entity.Composite;
using System;

namespace Entity
{
    public class BEUsuario
    {
      

        public int Id { get; set; }
        public string Usuario { get; set; } 
        public string Password { get; set; }
        public bool Activo { get; set; }

        public bool Bloqueado { get; set; }
        public List<BERol> listaRoles { get; set; }

        public List<BEPermiso> listaPermisos { get; set; }

        public BEUsuario()
        {
            listaPermisos = new List<BEPermiso>();
            listaRoles = new List<BERol>();
        }
        public BEUsuario(int pId, string pUsuario, string pPassword, bool pActivo, bool pBloqueado)
        {
            Id = pId;
            Usuario = pUsuario;
            Password = pPassword;
            Activo = pActivo;
            Bloqueado = pBloqueado;
        }
        public override string ToString()
        {
            return Id.ToString().Trim();
        }
    }
}
