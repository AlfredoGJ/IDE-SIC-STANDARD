using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IDE_ProgSistemas
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<string> ListaErrores = new List<string>();
        public static Dictionary<string, string> Tabsim= new Dictionary<string, string>();
        public static List<CodeRow> Codigo = new List<CodeRow>();
       


    }
}
