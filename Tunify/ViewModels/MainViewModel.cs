using Avalonia.Media.Imaging;
using ReactiveUI;
using System.Threading.Tasks;
using System;
using ReactiveUI.Fody.Helpers;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using Tunify.Helpers;

namespace Tunify.ViewModels;

public class MainViewModel : ReactiveObject
{
    [Reactive]
    public string ?uriCover1 { get; set; }

}
