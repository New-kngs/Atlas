﻿using AtlasDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasMES
{
    public partial class frmPlan_Add : Form
    {
        ServiceHelper srv = null;
        public PlanVO plan { get; set; }
        public frmPlan_Add()
        {
            InitializeComponent();            
        }
    }
}
