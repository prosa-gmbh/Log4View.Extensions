using System.Windows;
using System.Windows.Controls;

namespace Prosa.Log4View.SampleReceiver
{
    /// <summary>
    /// Interaction logic for SampleReceiverConfigView.xaml
    /// </summary>
    public partial class ContosoReceiverConfigControl : UserControl
    {
        public ContosoReceiverConfigControl()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
