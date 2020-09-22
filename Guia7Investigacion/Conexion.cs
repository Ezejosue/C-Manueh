using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Guia7Investigacion
{
    class Conexion
    {
        public string servidor, clave, db, usuario;
        public string cadena;
        

        public void conec()
        {
            servidor = "AEZEQ";
            clave = "123456";
            db = "GestionA_AA200509";
            usuario = "sa";

            cadena = "server=" + servidor + ";uid=" + usuario + ";pwd=" + clave + ";database=" + db;
        }

            
           
    }
}
