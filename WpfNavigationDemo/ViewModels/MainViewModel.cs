using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using WpfNavigationDemo.Commands;
using WpfNavigationDemo.Navigation;

namespace WpfNavigationDemo.ViewModels;

public sealed class MainViewModel : BaseViewModel
{
    private readonly NavigationStore _navigationStore;

    public BaseViewModel? CurrentViewModel => _navigationStore.CurrentViewModel;

    public ICommand NavHomeCommand { get; }
    public ICommand NavSettingsCommand { get; }
    
    public MainViewModel(
        NavigationStore navigationStore,
        [FromKeyedServices("Home")] INavigationService goHome,
        [FromKeyedServices("Settings")] INavigationService goSettings)
    { 
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
        
        NavHomeCommand = new RelayCommand(_ => goHome.Navigate());
        NavSettingsCommand = new RelayCommand(_ => goSettings.Navigate());
    }
}
 