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
    public partial class frmPlan : BaseForm
    {
        ServiceHelper service = null;

        public frmPlan()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            //OrderID, CustomerID, OrderShip, OrderEndDate, CreateDate, CreateUser, ModifyDate, ModifyUser

            //OrderID, ItemID, Qty, ItemName

            //
        }
    }
}
