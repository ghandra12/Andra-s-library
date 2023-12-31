﻿using myLibrary.myLibraryClasses;
using myLibrary.Windows;
using System;
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

namespace myLibrary
{
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public bool IsAdministrator { get; set; }
        public AdministratorWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (sender.Equals(AddReader))
            {
                AddReaderWindow addwindowreader = new AddReaderWindow();
                Visibility = Visibility.Hidden;
                addwindowreader.Show();
            }
            else
                if(sender.Equals(DeleteReader))
            {
                DeleteReaderWindow deletereaderwindow = new DeleteReaderWindow();
                Visibility = Visibility.Hidden;
                deletereaderwindow.Show();
            }
       
          
               
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (sender.Equals(LogOutBtn))
            {
                LoginWindow loginwindow = new LoginWindow();
                Visibility = Visibility.Hidden;
                loginwindow.Show();
            }
            else
                if (sender.Equals(AddBookBtn))
            {
                AddBooksWindow addbookwnd = new AddBooksWindow();
                Visibility = Visibility.Hidden;
                addbookwnd.Show();
            }
            if (sender.Equals(ChangeBtn))
            {
                ChangeDispWindow chdisp = new ChangeDispWindow();
                Visibility = Visibility.Hidden;
                chdisp.Show();
            }
            if (sender.Equals(RaportBtn))
            {
                StatisticsWindow wnd = new StatisticsWindow();
                Visibility = Visibility.Hidden;
                wnd.IsAdministrator = this.IsAdministrator;
                wnd.Show();
            }

        }
    }

}
