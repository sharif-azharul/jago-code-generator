using System;
using System.Collections.Generic;
using System.Text;

namespace AmarCodeGenerator
{
    public class TableModel
    {
        public string ObjectName { get; set; }
        public string MethodSaveName { get; set; }
        public string MethodGetByKeyName { get; set; }
        public string MethodGetByAllName { get; set; }
        public string MethodGetBySearchName { get; set; }
        public string MethodDeleteName { get; set; }

        public string SPSaveName { get; set; }
        public string SPGetByKeyName { get; set; }
        public string SPGetByAllName { get; set; }
        public string SPGetBySearchName { get; set; }
        public string SPDeleteName { get; set; }

        public string OriginalTableName { get; set; }
        public string DotNetModelName { get; set; }
        public string TableNameAsTitle { get; set; }
        public string DotNetInterfaceName { get; set; }
        public string DotNetBLLName { get; set; }
        public string DotNetIBLLIntName { get; set; }
        public string DotNetDataContextName { get; set; }
        public string RepositoryName { get; set; }
        public string RepositoryInterfaceName { get; set; }

        public string IndexViewPageName{ get; set; }
        public string EditViewPageName{ get; set; }
        public string CreateViewPageName{ get; set; }

        
        public string ControllerName { get; set; }
        public string APIControllerName { get; set; }

        public string DisplayName { get; set; }

        public string TableSchemaName { get; set; }
        public Boolean HasCompositeKey { get; set; }
        public List<ColumnModel> PropetyList { get; set; }

        public List<TableModel> ChildTableModelList{ get; set; }

        public Boolean IsParentTable{ get; set; }
        public Boolean IsChildTable { get; set; }

        //XHR
        public string UpdateConstructorName { get; set; }
        public string CommandVMName { get; set; }

        public string QueriesVMName { get; set; }
        public string GetListMethodName { get; set; }
        public string GetMethodName { get; set; }

        public string GetQueryHandlerName { get; set; }
        public string GetListQueryHandlerName { get; set; }
        public string GetDBFunctionName { get; set; }
        public string GetListDBFunctionName { get; set; }

        //Commands Name

        public string CreateCommandName { get; set; }
        public string UpdateCommandName { get; set; }
        public string DeleteCommandName { get; set; }

        // ASP net ZERO
        public string ZeroCreateInputDtoName { get; set; }
        public string ZeroFilterInputDtoName { get; set; }
        public string ZeroOutputDtoName { get; set; }
        public string ZeroUpdateInputDtoName { get; set; }
        public string ZeroServiceInterfaceName { get; set; }
        public string ZeroServiceName { get; set; }
        public string ZeroControllerName { get; set; }
        public string ZeroFolderName { get; set; }
        public string ZeroRepositoryVariableName { get; set; }
        public string ZeroServiceVariableName { get; set; }
        public string ZeroRouteRootName { get; set; }
    }
}
