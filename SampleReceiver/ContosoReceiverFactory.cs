using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;
using Prism.Modularity;
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleReceiver
{
    [Module(ModuleName = TypeId), UsedImplicitly]
    public class ContosoReceiverFactory : CustomReceiverFactory
    {
        internal const string TypeId = "ContosoReceiver";

        public ContosoReceiverFactory() 
        {
            ReceiverTypeId = TypeId;
            Name = "Contoso Receiver";
            HelpKeyword = null;
            ToolTip = "Opens the custom CONTOSO log files";
            ConfigTypes = new[] { typeof(ContosoConfig), };
            Log4ViewExtensionApi.RegisterSerializableType(typeof(ContosoConfig));

            SmallImage = new BitmapImage(GetImageUri("glyph"));
            LargeImage = new BitmapImage(GetImageUri("largeGlyph"));

            AutomationId = "ContosoReceiver";
        }

        private static Uri GetImageUri(string imageName)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().FullName;
            return new Uri($"pack://application:,,,/{assemblyName};component/Resources/{imageName}.png", UriKind.Absolute);
        }

        public override ICustomConfigData CreateCustomConfigData()
        {
            return new ContosoConfig();
        }

        public override IReceiverPlugin CreateReceiverPlugin(ICustomReceiver receiverCore, IReceiverConfig config)
        {
            if (config is ICustomReceiverConfig crc) {

                return new ContosoReceiverPlugin(receiverCore, crc);
            }

            return null;
        }

        /// <summary>
        /// Returns a custom receiver configuration record which provides all details necesarry to create a receiver.
        /// </summary>
        /// <param name="config">The custom receiver configuration.</param>
        /// <param name="edit">Controls, if the configuration dialog is is created to edit an existing receiver (true),
        /// or to create a new receiver (false).</param>
        /// <returns></returns>
        public override ICustomReceiverConfigVm CreateReceiverConfigurator(ICustomReceiverConfig config, bool edit)
        {
            return new ContosoReceiverConfigVm(this, config, edit);
        }

        public override Type GetViewType()
        {
            return typeof(ContosoReceiverConfigControl);
        }
    }
}
