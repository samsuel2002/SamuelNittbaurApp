using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SamuelNittbaur
{
    public partial class Qualifications : ContentPage
    {
        public Qualifications()
        {
            InitializeComponent();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new OverView();
        }

        private void OpenIU_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.iu.de"));

        } 
        private void OpenOneDrive_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://1drv.ms/f/s!AmQ0qEhRpx1qku4vmOjE7FrdDiQoQg?e=cJp9Lo"));

        }
    }
}
