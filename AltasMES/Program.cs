﻿using System;
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
            Application.Run(new frmWarehouse());
=======

            Application.Run(new Main());
>>>>>>> 45f44d8f68ad9a56a0bce54a3175602e662619a0

        }
    }
}
