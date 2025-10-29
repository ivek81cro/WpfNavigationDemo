using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfNavigationDemo.Commands;
using WpfNavigationDemo.Navigation;

namespace WpfNavigationDemo.ViewModels
{
    public sealed class HomeViewModel : BaseViewModel
    {
        public string Title => "Home";
    }
}
