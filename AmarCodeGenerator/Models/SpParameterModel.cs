using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator.Models
{
    public class SpParameterModel
    {
        public string ParameterName { get; set; }
        public string DataType { get; set; }
        public int Length { get; set; }
        public int Prec { get; set; }
        public int? Scale { get; set; }
        public int ParameterOrder { get; set; }
        public bool IsOutput { get; set; }

    }
}
