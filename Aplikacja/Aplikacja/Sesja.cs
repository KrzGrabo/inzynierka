using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja
{
    static class Sesja
    {
        static int id_sesji;
        public static void UstawId(int id)
        {
            id_sesji = id;
        }
        public static int ZwrocId()
        {
            return id_sesji;
        }
        public static void ZapotrzebowanieDieta()
        {

        }

    }

}
