using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using WpfApp1.Repository;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using WpfApp1.Repository;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
      
        Repository.operations _opr = new Repository.operations();
        public MainWindow()
        {
            InitializeComponent();
         

            KeyPairDetails();
        }
        public void KeyPairDetails()
        {
           
             MyDataGrid.ItemsSource = _opr.KeyPairDetails();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KeyvaluesEntitiy keyvaluesEntitiy = new KeyvaluesEntitiy();
           
            if (btnSubmit.Content == "Save")
            {
                _opr.Save(txtKeyval.Text,txtValueval.Text);
                txtKeyval.Text = "";
                txtValueval.Text = "";
                _opr.KeyPairDetails();
            }
            else if(btnSubmit.Content=="Update")
            {
                _opr.update(txtKeyval.Text, txtValueval.Text);

                txtValueval.Text = "";
                txtKeyval.Text = "";
                _opr.KeyPairDetails();
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddUpdate.Visibility = Visibility.Visible;
            btnSubmit.Content = "Save";

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            AddUpdate.Visibility = Visibility.Visible;
            btnSubmit.Content = "Update";
        }
    }
  
   
}
