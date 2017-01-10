using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Core;
using WinEchek.IO;
using Container = WinEchek.Model.Container;
using GameModeSelection = WinEchek.Views.GameModeSelection;
using Home = WinEchek.Views.Home;

namespace WinEchek
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ItemSource =
                ThemeManager.Accents.Select(
                        a =>
                            new AccentColorMenuData
                            {
                                Name = a.Name,
                                ColorBrush = a.Resources["AccentColorBrush"] as Brush
                            })
                    .ToList();
        }

        public List<AccentColorMenuData> ItemSource { get; set; }


        private void MenuItemQuit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //TODO has to do with the logger
            if (File.Exists("log.temp"))
                File.Delete("log.temp");
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Environment.GetCommandLineArgs().Length != 1)
            {
                ILoader loader = new BinaryLoader();
                Container container = null;

                try
                {
                    container = loader.Load(Environment.GetCommandLineArgs()[1]);
                }
                catch (Exception)
                {
                    this.ShowMessageAsync("Impossible de lire le fichier selectionner",
                        Environment.GetCommandLineArgs()[1]);
                }

                if (container != null)
                    MainControl.Content = new GameModeSelection(container, this);
                else
                    MainControl.Content = new Home(this);
            }
            else
                MainControl.Content = new Home(this);
        }
    }
}

public class AccentColorMenuData
{
    private ICommand _changeAccentCommand;
    public string Name { get; set; }
    public Brush BorderColorBrush { get; set; }
    public Brush ColorBrush { get; set; }

    public ICommand ChangeAccentCommand
    {
        get
        {
            return _changeAccentCommand ??
                   (_changeAccentCommand =
                       new SimpleCommand {CanExecuteDelegate = x => true, ExecuteDelegate = x => DoChangeTheme(x)});
        }
    }

    protected virtual void DoChangeTheme(object sender)
    {
        var theme = ThemeManager.DetectAppStyle(Application.Current);
        var accent = ThemeManager.GetAccent(Name);
        ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
    }
}

public class SimpleCommand : ICommand
{
    public Predicate<object> CanExecuteDelegate { get; set; }
    public Action<object> ExecuteDelegate { get; set; }

    public bool CanExecute(object parameter)
    {
        if (CanExecuteDelegate != null)
            return CanExecuteDelegate(parameter);
        return true;
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
        ExecuteDelegate?.Invoke(parameter);
    }
}