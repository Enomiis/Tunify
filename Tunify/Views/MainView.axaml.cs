using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Disposables;
using Tunify.ViewModels;

namespace Tunify.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        ViewModel = new MainViewModel();
        InitializeComponent();
    }
}
