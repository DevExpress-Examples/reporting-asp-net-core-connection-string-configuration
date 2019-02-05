using System;
using System.Collections.Generic;

namespace DXCustomConnectionStringsConfiguration.Models {
    public class DesignerModel {
        public string ReportID { get; set; }
        public Action<Dictionary<string, object>> DataSources { get; set; }
    }
}
