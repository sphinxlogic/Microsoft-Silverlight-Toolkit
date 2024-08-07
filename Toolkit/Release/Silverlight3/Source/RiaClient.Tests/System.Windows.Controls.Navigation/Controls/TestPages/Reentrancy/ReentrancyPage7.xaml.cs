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
using System.Windows.Navigation;
using System.Collections.ObjectModel;

namespace System.Windows.Controls.UnitTests
{
    public partial class ReentrancyPage7 : PageThatRecordsVirtuals
    {
        private static readonly string TestPagesPath = @"/System.Windows.Controls.Navigation/Controls/TestPages/";

        private Uri ReentrancyUri(string pageNumber)
        {
            return new Uri(TestPagesPath + "Reentrancy/ReentrancyPage" + pageNumber + ".xaml", UriKind.Relative);
        }

        public ReentrancyPage7()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.Uri == ReentrancyUri("4"))
            {
                e.Cancel = true;
                this.NavigationService.Navigate(ReentrancyUri("9"));
            }
        }

    }
}
