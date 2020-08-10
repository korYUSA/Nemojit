using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nemojit
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
            bool isNew = true;
            Mutex mutex = new Mutex(true, "Nemojit Mutex", out isNew);

            if (isNew == false)
            {
                MessageBox.Show("네모짓이 이미 실행중입니다.\n트레이 아이콘(작업 표시줄 우하단 작은 아이콘)을 확인해주세요.", "네모짓이 이미 실행중입니다.");
                return;
            }
            Application.Run(new Main());
            mutex.ReleaseMutex();
        }
    }
}
