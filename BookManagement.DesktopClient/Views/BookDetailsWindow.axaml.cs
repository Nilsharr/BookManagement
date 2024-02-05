using System;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using BookManagement.DesktopClient.ViewModels;
using ReactiveUI;

namespace BookManagement.DesktopClient.Views;

public partial class BookDetailsWindow : ReactiveWindow<BookDetailsWindowViewModel>
{
    public BookDetailsWindow()
    {
        InitializeComponent();
        this.WhenActivated(action => action(ViewModel!.ReturnBookCommand.Subscribe(Close)));
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}