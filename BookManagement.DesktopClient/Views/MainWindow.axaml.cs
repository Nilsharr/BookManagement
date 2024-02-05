using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using BookManagement.DesktopClient.Models;
using BookManagement.DesktopClient.ViewModels;
using ReactiveUI;

namespace BookManagement.DesktopClient.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(action =>
            action(ViewModel!.ShowBookDetailsDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<BookDetailsWindowViewModel,
        Book?> interaction)
    {
        var dialog = new BookDetailsWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<Book?>(this);
        interaction.SetOutput(result);
    }
}