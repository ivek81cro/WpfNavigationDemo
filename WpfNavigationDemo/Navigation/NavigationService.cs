using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNavigationDemo.ViewModels;

namespace WpfNavigationDemo.Navigation
{
    public sealed class NavigationService<TViewModel> : INavigationService
    where TViewModel : BaseViewModel // Constraint to ensure TViewModel is a BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        // Constructor that takes a NavigationStore and a factory function to create the ViewModel
        public NavigationService(NavigationStore navigationStore, Func<TViewModel> factory)
        {
            _navigationStore = navigationStore;
            _createViewModel = factory;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}