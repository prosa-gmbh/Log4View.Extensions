#region Copyright PROSA GmbH

// ////////////////////////////////////////////////////////////////////////////////////////////////////
// // Copyright © 2019 by PROSA GmbH, All rights reserved.
// //
// // The information contained herein is confidential, proprietary to PROSA GmbH,
// // and considered a trade secret. Use of this information by anyone other than
// // authorized employees of PROSA GmbH is granted only under a written nondisclosure
// // agreement, expressly prescribing the the scope and manner of such use.
// //
// ////////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleWorkspace {
    public class SampleViewVm : CustomViewVm {
        private SolidColorBrush _colorBrush;
        private string _message;
        private int _messageCount;
        private int _receiverCount;

        public SampleViewVm() {
            ColorBrushes = new List<SolidColorBrush> {
                new SolidColorBrush(Colors.Black),
                new SolidColorBrush(Colors.Red),
                new SolidColorBrush(Colors.Blue)
            };
            _colorBrush = ColorBrushes.First();

        }

        public int ReceiverCount {
            get => _receiverCount;
            set {
                if (_receiverCount != value) {
                    _receiverCount = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int MessageCount {
            get => _messageCount;
            set {
                if (_messageCount != value) {
                    _messageCount = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Message {
            get => _message;
            set {
                if (_message != value) {
                    _message = value;
                    RaisePropertyChanged();
                }
            }
        }

        public SolidColorBrush TextColor {
            get => _colorBrush;
            set {
                if (_colorBrush != null && !_colorBrush.Equals(value)) {
                    _colorBrush = value;
                    RaisePropertyChanged();
                }
            }
        }

        public IEnumerable<SolidColorBrush> ColorBrushes { get; }

        public void RefreshMsgCount() {
            MessageCount = ExtensionApi.MessageViews.Sum(mv => mv.VisibleMessageCount);
        }

        public void Action() {
            ExtensionApi.ShowMessage("Action", "An action has happened", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        protected override void OnConfigurationChanged(object sender, EventArgs eventArgs) {
            ReceiverCount = ExtensionApi.Receivers.Count();
        }

        protected override void OnCurrentMessageChanged(object sender, CurrentMessageChangedArgs args) {
            Message = args.CurrentMessage?.Message;
        }
    }
}
