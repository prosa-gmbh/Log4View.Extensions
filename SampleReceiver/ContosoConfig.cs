#region Copyright PROSA GmbH
// /////////////////////////////////////////////////////////////////////////////// 
// // Copyright © 2008 by PROSA GmbH, All rights reserved. 
// // 
// // The information contained herein is confidential, proprietary to PROSA GmbH, 
// // and considered a trade secret. Use of this information by anyone other than 
// // authorized employees of PROSA GmbH is granted only under a written nondisclosure
// // agreement, expressly prescribing the the scope and manner of such use.
// //
// /////////////////////////////////////////////////////////////////////////////// 
#endregion

using System;
using System.Runtime.Serialization;
using Prosa.Log4View.SDK;

namespace Prosa.Log4View.SampleReceiver
{
    [DataContract, Serializable]
    public class ContosoConfig : ICustomConfigData
    {
        public ContosoConfig(string filename)
        {
            Filename = filename;
            CustomLogFileId = "::Unique Contoso Log Identifier::";
            CustomTag = string.Empty;
        }

        [DataMember]
        public string Filename { get; set; }

        [DataMember]
        public string CustomLogFileId { get; set; }

        [DataMember]
        public string CustomTag { get; set; }

        public string SourceDescription => Filename;
        public string ReceiverTypeId => "ContosoReceiver";
    }
}