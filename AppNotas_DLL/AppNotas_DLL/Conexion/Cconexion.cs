using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;

namespace AppNotas_DLL.Conexion
{
    public class Cconexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://appnotasmvvm-default-rtdb.firebaseio.com/");

    }
}
