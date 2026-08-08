using System;
using System.IO;
using System.Web;

namespace Practica_03.Servicios
{
    public class UtilitarioService
    {
        public void RegistrarErrorBitacora(string mensaje, string lugar)
        {
            try
            {
                var rutaBase = HttpContext.Current != null
                    ? HttpContext.Current.Server.MapPath("~/App_Data")
                    : AppDomain.CurrentDomain.BaseDirectory;

                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                var rutaLog = Path.Combine(rutaBase, "Errores.log");
                var linea = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | " + lugar + " | " + mensaje;

                File.AppendAllText(rutaLog, linea + Environment.NewLine);
            }
            catch
            {
            }
        }
    }
}
