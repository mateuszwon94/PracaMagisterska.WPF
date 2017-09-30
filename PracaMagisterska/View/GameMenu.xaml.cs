﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PracaMagisterska.WPF.Utils;

namespace PracaMagisterska.WPF.View {
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    public partial class GameMenu : Page {
        public GameMenu() { InitializeComponent(); }

        private void LessonButton_OnClick(object sender, RoutedEventArgs e) {
            NavigationService?.Navigate(new SourceCode());
        }
    }
}