using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasMES
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

<<<<<<< HEAD
            Application.Run(new frmEquipment());
=======
            Application.Run(new Main());
>>>>>>> f4230cfd3d9a4b9a2796743d02389152d4bb6d9f
        }
    }
}
