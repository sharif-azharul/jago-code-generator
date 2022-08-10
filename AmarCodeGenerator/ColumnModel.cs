using System;
using System.Collections.Generic;
using System.Text;

namespace AmarCodeGenerator
{
   public class ColumnModel
    {
        public string DBName { get; set; }
        public string SYSName { get; set; }
        public string DBType { get; set; }
        public string DisplayName { get; set; }
        public string OriginalDBType { get; set; }
        public string SYSType { get; set; }

        public string DBLength { get; set; }
        public Boolean IsDeleteColumn { get; set; }
        public Boolean IsDeleteSupportColumn { get; set; }
        public Boolean IsUpdateSupportColumn { get; set; }
        public Boolean IsInsertSupportColumn { get; set; }
        public Boolean IsPrimayKey { get; set; }
        public Boolean IsForeignKey { get; set; }

        public Boolean IsSkippable { get; set; }

        public Boolean IsNullable{ get; set; }

        public string UIControlName{ get; set; }

        public string ParameterFormatName { get; set; }

        public Dictionary<string,string> PigListValues { get; set; }

        public LookUpDropdownModel LookUpDropdown { get; set; }

    }

    public class LookUpDropdownModel {
        public string DDLDataValue { get; set; }
        public string DDLTextValue { get; set; }
        public string MappingTableName { get; set; }
    }
}
