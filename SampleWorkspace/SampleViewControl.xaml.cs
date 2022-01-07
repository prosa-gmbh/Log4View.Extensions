using System.Windows;

namespace Prosa.Log4View.SampleWorkspace
{
    /// <summary>
    /// Interaction logic for SampleViewControl.xaml
    /// </summary>
    public partial class SampleViewControl
    {
        private SampleViewVm Vm => (SampleViewVm)DataContext;

        public SampleViewControl()
        {

            InitializeComponent();
        }

        private void Action(object sender, RoutedEventArgs e)
        {
            Vm.Action();
        }

        private void RefreshMsgCount(object sender, RoutedEventArgs e)
        {
            Vm.RefreshMsgCount();
        }
    }
}
