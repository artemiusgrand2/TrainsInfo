using System.Text;

namespace TrainsInfo.Common.BusinessObjects
{
    public class Constants
    {
        public static Encoding TextEncoding { get; } = Encoding.UTF8;// Encoding.GetEncoding(1251);
        // Обозначения таблиц левой части табло
        public const string KeyTablePassOpz = "PassOpz";
        public const string KeyTableVostok_Zapad = "Vostok_Zapad";
        public const string KeyTableZapad_Vostok = "Zapad_Vostok";
        public const string KeyTableBroshPoezd = "BroshPoezd";



        // Обозначения таблиц правой части табло
        public const string KeyTableGruzRabota = "GruzRabota";
        public const string KeyTableLok_nod = "Lok_nod";
        public const string KeyTableLok_ojto2 = "Lok_ojto2";
        public const string KeyTableLok_styk = "Lok_styk";
        public const string KeyTablePeredacha = "Peredacha";
        public const string KeyTableRazvoz = "Razvoz";
        public const string KeyTableREG_TAB = "REG_TAB";
        public const string KeyTableREG_TAB_PLAN = "REG_TAB_PLAN";

        public const string KeyLeftTables = "L";
        public const string KeyRightTables = "R";
    }
}
