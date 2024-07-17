using Group3WPF.Models;
using Group3WPF.Services;
using Group3WPF.VieModel;
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
using System.Windows.Shapes;

namespace Group3WPF.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
       
        public Login()
        {
            InitializeComponent();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccountMenberService accountMenberService = new AccountMenberService();
            String account = this.txt_account.Text;
            string password = this.txt_password.Text;
            AccountMember accountMember = accountMenberService.LoginAccountAsync(account, password);
           
            if (accountMember != null)
            {
                Dashboard dashboard = new Dashboard();
                if (accountMember.Role == "staff") {
                    //ôỗc nay quy định menu àon ê
                    dashboard.CategoryButton.Visibility = Visibility.Collapsed;
                    dashboard.ProductButton.Visibility = Visibility.Collapsed;
                    //dashboard.SuppliesButton.Visibility = Visibility.Collapsed;

                }
                //nếu k là stafff thì là admin
                dashboard.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("tài khoản or password sai");
            }
        }
    }
}
