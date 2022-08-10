using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator.Models
{
    public class StoredProcedureModel
    {
        public StoredProcedureModel()
        {
            Parameters = new List<SpParameterModel>();
            Outputs = new List<SpOutputModel>();
        }
        public string Name { get; set; }
        public string SchemaName { get; set; }
        public List<SpParameterModel> Parameters { get; set; }
        public List<SpOutputModel> Outputs { get; set; }
    }
}
