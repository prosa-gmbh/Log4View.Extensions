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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleReceiver {
    public class ContosoReceiverConfigVm : ICustomReceiverConfigurator, INotifyPropertyChanged {

        private readonly ContosoConfig _config;
        private string _filename;
        private string _logFileId = "::Unique Contoso Log Identifier::";

        public ContosoReceiverConfigVm(ICustomReceiverConfig config) {
            _config = (ContosoConfig) config.CustomConfig;
            Filename = _config.Filename;
            CustomLogFileId = _config.CustomLogFileId;
        }

        public string CustomLogFileId {
            get => _logFileId;
            set {
                _logFileId = value;
                IsModified = true;
                OnPropertyChanged();
            }
        }

        public string Filename {
            get => _filename;
            set {
                _filename = value;
                IsModified = true;
                OnPropertyChanged();
            }
        }

        public bool IsValid() {
            return !string.IsNullOrWhiteSpace(_logFileId) && !string.IsNullOrEmpty(Filename);
        }

        public bool IsModified { get; private set; }

        /// <summary>
        /// Return true, if a configuration dialog should be shown.
        /// </summary>
        public bool ShowDialog => true;

        public void WriteConfiguration()
        {
            _config.CustomLogFileId = CustomLogFileId;
            _config.Filename = Filename;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
