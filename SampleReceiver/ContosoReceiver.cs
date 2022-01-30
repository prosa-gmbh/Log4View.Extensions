#region Copyright PROSA GmbH

// ///////////////////////////////////////////////////////////////////////////////
// // Copyright © 2018 by PROSA GmbH, All rights reserved.
// //
// // The information contained herein is confidential, proprietary to PROSA GmbH,
// // and considered a trade secret. Use of this information by anyone other than
// // authorized employees of PROSA GmbH is granted only under a written nondisclosure
// // agreement, expressly prescribing the the scope and manner of such use.
// //
// ///////////////////////////////////////////////////////////////////////////////

#endregion

using System.IO;
using System.Text;
using System.Threading;
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleReceiver {
    public class ContosoReceiverPlugin : IReceiverPlugin {
        private readonly string _filename;
        private readonly AutoResetEvent _terminateThread;
        private readonly ContosoConfig _contosoConfig;
        private readonly ICustomReceiver _receiver;
        private Thread _creatingThread;
        private IMessageParser _parser;

        public ContosoReceiverPlugin(ICustomReceiver receiver, ICustomReceiverConfig config)
        {
            _receiver = receiver;
            _terminateThread = new AutoResetEvent(false);

            //_contosoConfig = JsonConvert.DeserializeObject<ContosoConfig>(config.CustomConfigData);
            _contosoConfig = (ContosoConfig)config.CustomConfigData;
            _filename = _contosoConfig?.Filename;

            // Important: This tells Log4View, that this receiver is ready to run.
            _receiver.IsInitiated = true;
        }

        public void BeginReceive() {
            _receiver.ReceiveMessages = true;

            // *************************************************************************************************
            // Here comes your receiver specific code, e.g parsing a file or listining to another logging source
            // In this sample, we just create a thread, which reads a log file in a proprietary format.
            // *************************************************************************************************

            // ************************************************************************************************
            // If you like to create one of the built-in parsers, call:
            // PatterParser: You must provide a log source id, a logging framework Id and a pattern-string, here just an example
            string patternString = "%date [%thread] %-5level %logger %ndc - %message%newline";
            var patternParser = _receiver.CreatePatternParser(_filename, FrameworkId.Log4net, patternString);

            // Test:
            string testLine = "2007-12-23 17:47:22,359 [1] INFO  prosa.Log4View.LogEmiter.LogEmiterForm (null) - Thread1 started";
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(testLine));
            IMessageBlock messages = patternParser.Parse(_receiver.CreateInputBuffer(stream));

            // Json and XML parser only need a log source id
            var jsonParser = _receiver.CreateJsonParser(_filename);
            var xmlParser = _receiver.CreateXmlParser(_filename);
            // ************************************************************************************************

            // For this example, we create our own 'ContosoParser'
            _parser = new ContosoParser(_receiver, _filename, _contosoConfig.CustomLogFileId);

            _creatingThread = new Thread(ReadMessages) {Name = "ReadMessages", IsBackground = true};
            _creatingThread.Start();
        }

        private void ReadMessages()
        {
            using (Stream stream = File.OpenRead(_filename)) {
                IInputBuffer buffer = _receiver.CreateInputBuffer(stream);
                if (_receiver.ReceiveMessages) {
                    IMessageBlock messageBlock = _parser.Parse(buffer);
                    _receiver.AddNewMessages(messageBlock);
                }
            }
        }

        public void Dispose()
        {
            _terminateThread.Set();
            _creatingThread.Join(200);
            _terminateThread.Dispose();

            // *************************************************************************************************
            // Dispose all your other resources here
            // *************************************************************************************************
        }
    }
}
