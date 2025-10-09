using System.Xml;

namespace BackUp
{
    public class Gestion
    {
        public static void CrearCarpetaBackup()
        {
            try
            {
                //Obtengo la Dirección de la ruta actual de FordFox V2.0:
                string ruta = AppDomain.CurrentDomain.BaseDirectory;
                //Conbino la direccion de la ruta anterior con la Carpeta Backup:
                string rutaBackup = Path.Combine(ruta, "Backup");
                //Verifico si no existe la carpeta Backup:
                if (!Directory.Exists(rutaBackup))
                {
                    //Si no existe, la creo:
                    Directory.CreateDirectory(rutaBackup);
                }

            }
            catch (Exception ex) { throw ex; }
        }
        public void CrearBackUp()
        {
            try
            {
                //Verifico que exista la carpeta BD:
                CrearCarpetaBD();
                //Verifico que exista la carpeta Backup:
                CrearCarpetaBackup();
                //Obtengo la ruta de la BD:
                string rutaBD = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,  "BD.xml");
                //Nombre del Backup:
                string nombreBackup = $"BD_Backup_{DateTime.Now:dd-MM-yyyy HH-mm-ss}.xml";
                //Ruta Destino del Backup:
                string rutaBackup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup", nombreBackup);
                //Verifico que exista la BD:
                if (File.Exists(rutaBD))
                {
                    //Verifico que no exista el nombre del Backup que se intenta guardar:
                    if (!File.Exists(rutaBackup))
                    {
                        //Copiamos la BD para que quede como un Backup:
                        File.Copy(rutaBD, rutaBackup);
                    }
                    else { throw new Exception("Error: El Backup que intenta guardar ya existe!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar la dirección de la Base de Datos!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public void CrearRestore(string nombreBackup)
        {
            try
            {
                //Verifico que exista la carpeta BD:
                CrearCarpetaBD();
                //Verifico que exista la carpeta Backup:
                CrearCarpetaBackup();
                //Obtengo la ruta de la BD:
                string rutaBD = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,  "BD.xml");
                //Especifico la dirección de los Backups y del nombre del backup seleccionado:
                string rutaBackup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup", nombreBackup);
                //Verifico que la rutaBD no sea nula:
                if (File.Exists(rutaBD))
                {
                    //Verifico que la rutaBackup no sea nula:
                    if (File.Exists(rutaBackup))
                    {
                        //Copio el Backup seleccionado a la carpeta de la BD, y se pone true en el 3° parámetro para sobreescribir el archivo BD.xml existente:
                        File.Copy(rutaBackup, rutaBD, true);
                    }
                    else { throw new Exception("Error: No se pudo recuperar la dirección de los Backups para realizar el Restore!"); }
                }
                else { throw new Exception("Error: No se pudo recuperar la dirección de la Base de Datos!"); }


            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }

        public List<string> ListarBackups()
        {
            try
            {
                //Obtengo la ruta de los Backups:
                string rutaBackup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup");
                //Verifico que exista la Carpeta:
                if (Directory.Exists(rutaBackup))
                {
                    //Recupeto todos los archivos XML Backups:
                    string[] backups = Directory.GetFiles(rutaBackup, "*.xml");
                    //FALTA VERIFICAR QUE PASA SI NO EXISTEN BACKUPS:

                    //Devuevlo los Backups:
                    return backups.Select(Path.GetFileName).ToList();
                }
                else { throw new Exception("Error: No se pudo recuperar la dirección de los Backups!"); }
            }
            catch (XmlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { }
        }
        public static void CrearCarpetaBitacora()
        {
            try
            {
                //Obtengo la Dirección de la ruta actual de FordFox V2.0:
                string ruta = AppDomain.CurrentDomain.BaseDirectory;
                //Conbino la direccion de la ruta anterior con la Carpeta Restore:
                string rutaBitacora = Path.Combine(ruta, "Bitacora");
                //Verifico si no existe la carpeta Restore:
                if (!Directory.Exists(rutaBitacora))
                {
                    //Si no existe, la creo:
                    Directory.CreateDirectory(rutaBitacora);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void CrearCarpetaBD()
        {
            try
            {
                //Obtengo la Dirección de la ruta actual de FordFox V2.0:
                string ruta = AppDomain.CurrentDomain.BaseDirectory;
                //Conbino la direccion de la ruta anterior con la BD:
                string rutaBD = Path.Combine(ruta, "BaseDatos");
                //Verifico si no existe la carpeta BD:
                if (!Directory.Exists(rutaBD))
                {
                    //Si no existe, la creo:
                    Directory.CreateDirectory(rutaBD);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void CrearCarpetaPDFs(string nombreCarpeta)
        {
            try
            {
                // Obtengo la Dirección de la ruta actual de FordFox V2.0
                string ruta = AppDomain.CurrentDomain.BaseDirectory;
                // Combino la direccion de la ruta anterior con la carpeta de PDFs
                string rutaPDF = Path.Combine(ruta, nombreCarpeta);
                // Verifico si no existe la carpeta PDF
                if (!Directory.Exists(rutaPDF))
                {
                    // Si no existe, la creo
                    Directory.CreateDirectory(rutaPDF);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static void CrearCarpetaInformes()
        {
            try
            {
                //Obtengo la Dirección de la ruta actual de FordFox V2.0:
                string ruta = AppDomain.CurrentDomain.BaseDirectory;
                //Conbino la direccion de la ruta anterior con la BD:
                string rutaInforme = Path.Combine(ruta, "Informes");
                //Verifico si no existe la carpeta Informes:
                if (!Directory.Exists(rutaInforme))
                {
                    Directory.CreateDirectory(rutaInforme);
                }

            }
            catch (Exception ex) { throw ex; }
        }

        public static string UbicacionBD(string nombreXML)
        {
            try
            {
                string baseDatos = "BaseDatos";
                CrearCarpetaBD();
                return Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseDatos), nombreXML);

            }
            catch (Exception ex) { throw ex; }
        }


        public static string UbicacionBitacora(string nombreXML)
        {
            try
            {
                string bitacora = "Bitacora";
                CrearCarpetaBD();
                return Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, bitacora), nombreXML);

            }
            catch (Exception ex) { throw ex; }
        }

        public static string UbicacionInformes(string nombreXML)
        {
            try
            {
                string informes = "Informes";
                CrearCarpetaInformes();
                return Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, informes), nombreXML);
            }
            catch (Exception ex) { throw ex; }
        }

        public static string UbicacionPDFs(string nombreArchivo)
        {
            try
            {
                string carpetaPDFs = "PDFs";
                CrearCarpetaPDFs(carpetaPDFs);
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, carpetaPDFs, nombreArchivo);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
