using System;
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleReceiver
{
    class ContosoParser : MessageParser
    {
        private readonly string _fileId;

        public ContosoParser(ILogReceiver receiver, string sourceId, string fileId) : base(receiver, sourceId)
        {
            _fileId = fileId;
        }

        public override IMessageBlock Parse(IInputBuffer buffer, int? maxMessageCount = null)
        {
            var mb = new MessageBlock();
            double lineCount = buffer.Lines.Count;
            for (int i=0; i<lineCount; i++) {
                string line = buffer.Lines[i].Trim();

                if (i == 0) {
                    if (line != _fileId) {
                        // No valid file header found; file will not be parsed
                        break;
                    }
                } else {
                    string[] parts = line.Split('|');

                    if (parts.Length == 4) {
                        ILogMessage message = Receiver.CreateLogMessage();
                        message.Time = DateTimeOffset.Parse(parts[0]);
                        message.LogLevel = Receiver.Levels.Get(parts[1]);
                        message.Logger = parts[2];
                        message.Message = parts[3];
                        mb.Add(message);                        
                    }
                }
                NotifyProgress(i/lineCount);
            }

            return mb;
        }
    }
}
