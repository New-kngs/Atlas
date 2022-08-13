using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlasPOP
{

    public partial class lblState : UserControl
    {
        public lblState(string state)
        {
            InitializeComponent();

            label1.Text = state;

            if (label1.Text == "작업중")
                label1.ForeColor = Color.Green;
            else if (label1.Text == "작업종료")
                label1.ForeColor = Color.Red;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
