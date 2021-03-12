using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace newProfileBook.PopUpsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPopUpView : PopupPage
    {
        public ListPopUpView(string ImagePath)
        {
            InitializeComponent();
            ImgView.Source = ImagePath;
        }
    }
}