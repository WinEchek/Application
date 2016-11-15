using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinEchek.GUI.Core.Widgets {
    /// <summary>
    /// Logique d'interaction pour HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl {
        public HistoryView() {
            InitializeComponent();
            List<String> list = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                list.Add("Data");
            }

            ListViewHistory.ItemsSource = list;
        }
    }
}
