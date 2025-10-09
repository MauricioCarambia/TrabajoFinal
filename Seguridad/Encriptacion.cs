using System.Security.Cryptography;
using System.Text;

namespace Seguridad
{
    public static class Encriptacion
    {
        #region "Métodos"

        public static string EncriptarPassword(string pPassword)
        {
            try
            {
                byte[] encriptado = Encoding.Unicode.GetBytes(pPassword);
                string resultado = Convert.ToBase64String(encriptado);
                return resultado;
            }
            catch (CryptographicException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public static string DesencriptarPassword(this string pPPasswordEncriptado)
        {
            try
            {
                byte[] desencriptar = Convert.FromBase64String(pPPasswordEncriptado);
                string resultado = Encoding.Unicode.GetString(desencriptar);
                return resultado;
            }
            catch (CryptographicException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        #endregion
    }
}
