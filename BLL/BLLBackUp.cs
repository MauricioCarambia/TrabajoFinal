using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLBackUp
    {
        MPPBackUp oMPPGestorBD;

       
        public BLLBackUp() { oMPPGestorBD = new MPPBackUp(); }

   

        public bool CrearBackup() { return oMPPGestorBD.CrearBackup(); }

        public bool CrearRestore(string nombreBackup) { return oMPPGestorBD.CrearRestore(nombreBackup); }

        public List<string> ListarBackups() { return oMPPGestorBD.ListarBackups(); }

    
    }
}
