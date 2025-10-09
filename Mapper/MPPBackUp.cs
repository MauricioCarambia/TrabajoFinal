using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackUp;

namespace Mapper
{
    public class MPPBackUp
    {
        //dentro de backup tengo la clase gestion
        Gestion gestionDB = new Gestion();
        public bool CrearBackup()
        {
            try
            {
                gestionDB.CrearBackUp();
                return true;
            }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public bool CrearRestore(string nombreBackup)
        {
            try
            {
                gestionDB.CrearRestore(nombreBackup);
                return true;
            }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public List<string> ListarBackups()
        {
            try
            {
                List<string> listaNombreBackups = new List<string>();
                listaNombreBackups = gestionDB.ListarBackups();
                if (listaNombreBackups != null) { return listaNombreBackups; }
                else { return listaNombreBackups = null; }
            }
            catch (Exception ex) { throw ex; }
            finally { }
        }
    }
}
