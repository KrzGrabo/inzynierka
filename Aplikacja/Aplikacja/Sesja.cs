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
        public static void UstawId()
        {
            id_sesji = 0;
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
