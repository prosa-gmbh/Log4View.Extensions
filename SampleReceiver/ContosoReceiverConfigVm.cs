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
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleReceiver {
    public class ContosoReceiverConfigVm : CustomReceiverConfigVm, INotifyPropertyChanged {

        private readonly ContosoConfig _contosoConfig;
        private string _filename;
        private string _logFileId;
        private string _customTag;

        public ContosoReceiverConfigVm(CustomReceiverFactory factory, ICustomReceiverConfig config, bool edit)
        : base(factory, config, edit)
        {
            _contosoConfig = (ContosoConfig)config.CustomConfigData;
            Filename = _contosoConfig?.Filename;
            CustomLogFileId = _contosoConfig?.CustomLogFileId;
            CustomTag = _contosoConfig?.CustomTag;
        }

        public string CustomLogFileId {
            get => _logFileId;
            set {
                _logFileId = value;
                RaisePropertyChanged();
            }
        }

        public string CustomTag {
            get => _customTag;
            set {
                _customTag = value;
                RaisePropertyChanged();
            }
        }

        public string Filename {
            get => _filename;
            set {
                _filename = value;
                RaisePropertyChanged();
            }
        }

        public override bool IsValid() {
            return !string.IsNullOrEmpty(Filename);
        }

        /// <summary>
        /// Return true, if a configuration dialog should be shown.
        /// </summary>
        public override bool ShowConfigDialog => true;

        /// <summary>Writes the configuration.</summary>
        public override void WriteConfiguration()
        {
            _contosoConfig.CustomLogFileId = CustomLogFileId;
            _contosoConfig.CustomTag = CustomTag;
            _contosoConfig.Filename = Filename;
            Configuration.CustomConfigData = _contosoConfig;
        }
    }
}
