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
        public MainWindow()
        {
            InitializeComponent();
            MainControl.Content = new GUI.Core.Home(this);
            WinEchek = new Core.WinEchek();
	    }
    }
}
