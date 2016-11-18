using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro;
using WinEchek.GUI;
using WinEchek.Model;


namespace WinEchek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Core.WinEchek WinEchek { get; set; }
        public List<AccentColorMenuData> ItemSource { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            MainControl.Content = new GUI.Core.Home(this);
            WinEchek = new Core.WinEchek();

            /**
             * On remplit la liste des couleurs disponnibles pour le thème
             */
            ItemSource = ThemeManager.Accents.Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush }).ToList();
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
