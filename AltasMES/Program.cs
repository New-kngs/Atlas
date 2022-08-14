using AtlasDTO;
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

            EmplogVO emp = new EmplogVO
            {
                EmpID = "Master",
                LogText = "MES 시스템 시작"
            };

            ServiceHelper service = new ServiceHelper("");
            ResMessage<List<EmplogVO>> result = service.PostAsync<EmplogVO, List<EmplogVO>>("api/Emplog/SaveEmplog", emp);

            Application.Run(new Main());


            

        }
    }
}
