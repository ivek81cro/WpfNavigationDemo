using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfNavigationDemo.ViewModels;

namespace WpfNavigationDemo.Navigation
{
    public sealed class NavigationStore
    {
        private BaseViewModel? _currentViewModel;
        public BaseViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    CurrentViewModelChanged?.Invoke(); // Notify subscribers that the current ViewModel has changed
                }
            }
        }

        public Action? CurrentViewModelChanged;
    }
}
