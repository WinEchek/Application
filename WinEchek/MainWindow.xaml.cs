using WinEchek.GUI;
using WinEchek.Model;


namespace WinEchek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainControl.Content = new GUI.Core.Home(this);
	    }
    }
}
