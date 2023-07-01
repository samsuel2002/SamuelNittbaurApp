using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SamuelNittbaur
{
    public partial class OverView : ContentPage
    {
        public OverView()
        {
            InitializeComponent();
        }

        private void PassionsButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Passions();
        }

        private void SkillsButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Skills();
        }

        private void QualificationsButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Qualifications();
        }

        private void ThisApp_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new ThisApp();

        }
    }
}
