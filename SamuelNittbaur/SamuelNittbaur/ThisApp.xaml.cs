using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Npgsql;


namespace SamuelNittbaur
{
    public partial class ThisApp : ContentPage
    {
        public ThisApp()
        {
            InitializeComponent();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new OverView();
        }

       
    }
}
