using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BEPermisos
    {
        public int Id { get; set; }          // ID único del permiso
        public string Nombre { get; set; } = string.Empty;  // Nombre descriptivo
        public string Menu { get; set; } = string.Empty;   // Nombre del menú del MenuStrip
        public string Item { get; set; } = string.Empty;
    }
}
