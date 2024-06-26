﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Globalization;

namespace System.Windows.Controls.UnitTests
{
    public partial class UserControl1 : UserControl
    {
        static int _instances;

        public UserControl1()
        {
            InitializeComponent();

            _instances++;
            _txtBlock.Text += string.Format(CultureInfo.InvariantCulture, ", Instance #{0}", _instances);
        }
    }
}
