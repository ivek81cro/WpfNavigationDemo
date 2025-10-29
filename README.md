Opis

Čist MVVM bez Frame/Page i bez vanjskih frameworka.
ViewModel-first (VM → View preko DataTemplate u App.xaml).
Jednostavni NavigationStore i generički NavigationService<TViewModel>.
(Opcionalno) lijevi vertikalni meni s isticanjem aktivne stavke.

Kako radi

MainWindow ima ContentControl vezan na MainViewModel.CurrentViewModel.
App.xaml sadrži DataTemplate mapiranja VM → View (npr. HomeViewModel → HomeView).
NavigationService<T> kreira novi VM i postavlja ga u NavigationStore.CurrentViewModel.
MainViewModel izlaže komandne akcije (npr. NavHomeCommand) koje pozivaju Navigate().

Reuse u drugom projektu

Kopiraj: BaseViewModel, RelayCommand, /Navigation folder, (po želji i Converter).
Uskladi namespace u svim .cs i XAML x:Class / clr-namespace: mapama.
App.xaml: DataTemplate mapiranja za sve VM-ove koje prikazuješ. (Bez StartupUri.)
App.xaml.cs: složi NavigationStore, NavigationService<T>, početni VM; postavi MainWindow.DataContext.
MainWindow.xaml: ContentControl (i po želji lijevi meni).

Dodavanje novog ekrana

Kreiraj Views/AboutView.xaml(.cs) i ViewModels/AboutViewModel.cs.
U App.xaml dodaj:

<DataTemplate DataType="{x:Type vm:AboutViewModel}">
  <views:AboutView/>
</DataTemplate>

U App.xaml.cs:

var goAbout = new NavigationService<AboutViewModel>(store, () => new AboutViewModel());


U MainViewModel dodaj ICommand NavAboutCommand = new RelayCommand(_ => goAbout.Navigate());
Dodaj gumb u izbornik.

Napredne varijante

Parametrizirana navigacija: NavigationService<TViewModel, TParam>.Navigate(TParam p) (factory prima parametar).
Povijest (Back): u NavigationStore dodaj Stack<BaseViewModel> i GoBack().
DI Container: zamijeni lokalne factory delegate s Microsoft.Extensions.DependencyInjection registracijama.
