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
        public static List<int> listalinea = new List<int>();
        public static string direccionInicio = "";
        public static string tamaño = "";
        public static string nombre = "";
        public static string tipoArchivo="";
        public static int CP=0;
        public static bool banaviso=false;
        public static int lineaerror;

        //public static Dictionary<String, int> OpCodes = new Dictionary<String, int>()
        //{
        //    {"ADD", "18"},
        //    {"AND", "40"},
        //    {"COMP", "28"},
        //    {"DIV", 0x24},
        //    {"J", 0x3C},
        //    {"JEQ", 0x30},
        //    {"JGT", 0x34},
        //    {"JLT", 0x38},
        //    {"JSUB", 0x48},
        //    {"LDA", 0x00},
        //    {"LDCH", 0x50},
        //    {"LDL", 0x08},
        //    {"LDX", 0x04},
        //    {"MUL", 0x20},
        //    {"OR", 0x44},
        //    {"RD", 0xD8},
        //    {"RSUB", 0x4C},
        //    {"STA", 0x0C},
        //    {"STCH", 0x54},
        //    {"STL", 0x14},
        //    {"STSW", 0xE8},
        //    {"STX", 0x10},
        //    {"SUB", 0x1C},
        //    {"TD", 0xE0},
        //    {"TIX", 0x2C},
        //    {"WD", 0xDC}
        //};


        public static Dictionary<String, int> OpCodes = new Dictionary<String, int>()
        {
            {"ADD", 0x18},
            {"AND", 0x40},
            {"COMP", 0x28},
            {"DIV", 0x24},
            {"J", 0x3C},
            {"JEQ", 0x30},
            {"JGT", 0x34},
            {"JLT", 0x38},
            {"JSUB", 0x48},
            {"LDA", 0x00},
            {"LDCH", 0x50},
            {"LDL", 0x08},
            {"LDX", 0x04},
            {"MUL", 0x20},
            {"OR", 0x44},
            {"RD", 0xD8},
            {"RSUB", 0x4C},
            {"STA", 0x0C},
            {"STCH", 0x54},
            {"STL", 0x14},
            {"STSW", 0xE8},
            {"STX", 0x10},
            {"SUB", 0x1C},
            {"TD", 0xE0},
            {"TIX", 0x2C},
            {"WD", 0xDC}
        };

        public class MyInt
        {
            public int Value { get => valueInt; set =>valueInt = value; }
            public string HEX2 { get => valueInt.ToString("X2"); }
            public string HEX4 { get => valueInt.ToString("X4"); }
            public string HEX6 { get => valueInt.ToString("X6"); }
            private int valueInt;

            public MyInt(int val)
            
            { valueInt = val; }
        }



    }
}
