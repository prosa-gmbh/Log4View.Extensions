using System.Windows.Controls;
using System.Windows.Input;

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
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int _))
            {
                e.Handled = true;
            }
        }
    }
}
