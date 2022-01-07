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

using System;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;
using Prism.Modularity;
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleWorkspace {
    [Module(ModuleName = TypeId), UsedImplicitly]
    public class SampleViewFactory : CustomViewFactory {
        private const string TypeId = "Prosa.SampleWorkspace";

        public SampleViewFactory() {
            MenuCaption = "Sample Workspace";
            ControlId = TypeId;

            Glyph = new BitmapImage(GetImageUri("glyph"));
            LargeGlyph = new BitmapImage(GetImageUri("largeGlyph"));
        }

        private Uri GetImageUri(string imageName) {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().FullName;
            return new Uri($"pack://application:,,,/{assemblyName};component/Resources/{imageName}.png", UriKind.Absolute);
        }

        /// <summary>
        /// Creates the custom workspace.
        /// </summary>
        /// <returns>Custom Workspace</returns>
        public override UserControl CreateCustomView() {
            return new SampleViewControl();
        }
    }
}
