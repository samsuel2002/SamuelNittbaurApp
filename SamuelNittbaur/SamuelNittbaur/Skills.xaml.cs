using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SamuelNittbaur
{
    public partial class Skills : ContentPage
    {
        public Skills()
        {
            InitializeComponent();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new OverView();
        }

        private void OpenGit_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://github.com/samsuel2002/SamuelNittbaurApp"));

        }
    }
}
