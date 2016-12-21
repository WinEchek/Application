using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using Microsoft.Win32;
using WinEchek.Core.Persistance;
using WinEchek.GUI.Core;
using Container = WinEchek.Model.Container;

//TODO: TOUT REMETTRE DANS LES BON NAMESPACE APRÈS LES CHANGEMENTS DANS LE MODEL (PAR RAPPORT À L'ENGINE) VINZOU




namespace WinEchek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public List<AccentColorMenuData> ItemSource { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            if (Environment.GetCommandLineArgs().Length == 1)
                MainControl.Content = new GUI.Core.Home(this);
            else
            {
                ILoader loader = new BinaryLoader();

                Container container = loader.Load(Environment.GetCommandLineArgs()[1]);

                MainControl.Content = new GameModeSelection(container, this);
            }

            /**
             * On remplit la liste des couleurs disponnibles pour le thème
             */
            ItemSource = ThemeManager.Accents.Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush }).ToList();
        }

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
    }
}



public class AccentColorMenuData {
    public string Name { get; set; }
    public Brush BorderColorBrush { get; set; }
    public Brush ColorBrush { get; set; }

    private ICommand _changeAccentCommand;

    public ICommand ChangeAccentCommand {
        get { return this._changeAccentCommand ?? (_changeAccentCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = x => this.DoChangeTheme(x) }); }
    }

    protected virtual void DoChangeTheme(object sender) {
        var theme = ThemeManager.DetectAppStyle(Application.Current);
        var accent = ThemeManager.GetAccent(this.Name);
        ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
    }
}

public class SimpleCommand : ICommand {
    public Predicate<object> CanExecuteDelegate { get; set; }
    public Action<object> ExecuteDelegate { get; set; }

    public bool CanExecute(object parameter) {
        if (CanExecuteDelegate != null)
            return CanExecuteDelegate(parameter);
        return true;
    }

    public event EventHandler CanExecuteChanged {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
        ExecuteDelegate?.Invoke(parameter);
    }
}
