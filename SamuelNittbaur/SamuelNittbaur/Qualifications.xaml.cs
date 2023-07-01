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
            Device.OpenUri(new Uri("https://www.iu.de/?utm_source=google&utm_medium=cpc&utm_campaign=10501369421&utm_term=iu&utm_content=125084765412&device=c&gclid=Cj0KCQjw1_SkBhDwARIsANbGpFtEsA6New__OsPNoqy8Dfl-5jCQ8zZUgq0OHREuwDc4cowup_JZu1MaAlNxEALw_wcB"));

        }
    }
}
