using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace SamuelNittbaur
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Button_Animation();
        }

      
        public async void Button_Animation()
        {
            while (true)
            {
                await Explore_Btn.ScaleTo(1.1, 1000, Easing.CubicOut);
                await Explore_Btn.ScaleTo(0.9, 1000, Easing.CubicIn);
            }
          
        }

        private void Explore_Btn_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new OverView();
        }
    }
}
