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
using System.Threading;
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleReceiver {
    public class ContosoReceiverPlugin : IReceiverPlugin {
        private readonly string _filename;
        private readonly AutoResetEvent _terminateThread;
        private readonly ContosoConfig _config;
        private readonly ICustomReceiver _receiver;
        private Thread _creatingThread;
        private IMessageParser _parser;

        public ContosoReceiverPlugin(ICustomReceiver core, ICustomReceiverConfig config)
        {
            _receiver = core;
            _terminateThread = new AutoResetEvent(false);

            _config = (ContosoConfig) config.CustomConfig;
            _filename = _config.Filename;

            // Important: This tells Log4View, that this receiver is ready to run.
            _receiver.IsInitiated = true;
        }

        public void BeginReceive() {
            _receiver.ReceiveMessages = true;

            // *************************************************************************************************
            // Here comes your receiver specific code, e.g parsing a file or listining to another logging source
            // In this sample, we just create a thread, which regularily creates log messages.
            // *************************************************************************************************

            // If you like to create one of the built-in parsers, just call:
            // var parser = _receiver.CreateParser(_filename);

            // Here, we create our own 'ContosoParser'
            _parser = new ContosoParser(_receiver, _config.Filename, _config.CustomLogFileId);

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
