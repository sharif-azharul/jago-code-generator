using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    public class XHRCommand
    {

        public void GenerateCommandFromTemplateXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\" + @"Commands");
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRCommandTemplate.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void GenerateCommandVMFromTemplateXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\Models\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\Models\" +
                        string.Format("\\{0}CommandVM", pTable.OriginalTableName));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRCommandVMTemplate.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //VM

        public void GenerateCreateCommandFromTemplateXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\" +
                        string.Format("\\CreateCommandHandler"));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRCreateCommandHandler.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //Create Command Handler
        public void GenerateUpdateCommandFromTemplateXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\" +
                         string.Format("\\UpdateCommandHandler"));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRUpdateCommandHandler.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // Mark as deleted
        public void GenerateMarkAsDeleteCommandHandlerFromTemplateXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\" + @"Commands\" +
                         string.Format("\\MarkAsDeleteCommandHandler"));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRMarkAsDeleteCommandHandler.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        //----------------------------------------  QUERIES -----------------------------------------

        //Model
        public void GenerateQueriesModelXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Queries\Models\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Queries\Models\"
                            + string.Format("\\{0}VM", pTable.OriginalTableName));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRQueriesModelVM.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //Queries
        public void GenerateQueriesXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Queries\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Queries"
                            + string.Format("\\Queries"));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRQueries.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //Get QuerY Handler
        public void GenerateQueryHandlerXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Queries\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Queries"
                            + string.Format("\\Get{0}QueryHandler", pTable.OriginalTableName));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRGetQueryHandler.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //Get QuerY Handler
        public void GenerateListQueryHandlerXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Queries\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Queries"
                            + string.Format("\\Get{0}ListQueryHandler", pTable.OriginalTableName));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRGetListQueryHandler.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //--------------------------------------------------- Controller---------------------------
        public void GenerateControllerXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Controller\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Controller"
                            + string.Format("\\{0}Controller", pTable.OriginalTableName));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRController.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //--------------------------------------------------- Configurations---------------------------
        public void GenerateConfigurationXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Configurations\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\Configurations"
                            + string.Format("\\{0}Configuration", pTable.OriginalTableName));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.AppendLine("using EmployeeEnrollment.Core.Entities;");
                    sb.AppendLine("using Microsoft.EntityFrameworkCore;");
                    sb.AppendLine("using Microsoft.EntityFrameworkCore.Metadata.Builders;");
                    sb.AppendLine("");

                    sb.AppendLine("namespace EmployeeEnrollment.Persistence.Configurations");
                    sb.AppendLine("{");
                    sb.AppendLine("");

                    sb.AppendLine(string.Format("public class {0}Configuration : IEntityTypeConfiguration<{1}>", pTable.OriginalTableName, pTable.OriginalTableName));
                    sb.AppendLine("{");
                    sb.AppendLine("");

                    sb.AppendLine(string.Format("public void Configure(EntityTypeBuilder<{0}> builder)", pTable.OriginalTableName));
                    sb.AppendLine("{");
                    //logic
                    foreach (var oProp in pTable.PropetyList)
                    {
                        if (oProp.SYSType == "string")
                        {
                            sb.AppendLine(string.Format("builder.Property(hr => hr.{0}).HasMaxLength({1});", oProp.SYSName, oProp.DBLength));
                        }
                    }
                    sb.AppendLine("");
                    sb.AppendLine("");

                    //end logic
                    sb.AppendLine("}");
                    sb.AppendLine("}");
                    sb.AppendLine("}");

                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //--------------------------------------------------- Configurations---------------------------

        //--------------------------------------------------- PermissionProvider---------------------------
        public void GeneratePermissionProviderXHR(List<TableModel> oTableList)
        {
            const string consTemp = @"""";
            CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\PermissionProvider\");
            StreamWriter sw = null;
            System.Text.StringBuilder sb = null;
            //Stream myStream = null;

            #region Create Empty cs file
            sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\PermissionProvider"
                    + string.Format("\\StandardPermissionProvider"));
            // sb = new System.Text.StringBuilder(lstrTableName);
            sb.Append(".cs");
            FileInfo lobjFileInfo = new FileInfo(sb.ToString());
            sw = lobjFileInfo.CreateText();

            sb = new System.Text.StringBuilder();
            #endregion
            foreach (var pTable in oTableList)
            {


                try
                {
                    sb.AppendFormat("//{0}", pTable.OriginalTableName.ToUpper());
                    sb.AppendLine("");
                    //logic
                    sb.AppendFormat("public static readonly PermissionRecord View{1} = new PermissionRecord {2} Name = {0}View {1}{0}, SystemName = {0}Permissions.View{1}{0}, Category = {0}{1}{0} {3};", consTemp, pTable.OriginalTableName, "{", "}");
                    sb.AppendLine(" ");
                    sb.AppendFormat("public static readonly PermissionRecord Create{1} = new PermissionRecord {2} Name = {0}Create {1}{0}, SystemName = {0}Permissions.Create{1}{0}, Category = {0}{1}{0} {3};     ", consTemp, pTable.OriginalTableName, "{", "}");
                    sb.AppendLine(" ");
                    sb.AppendFormat("public static readonly PermissionRecord Edit{1} = new PermissionRecord {2} Name = {0}Edit {1}{0}, SystemName = {0}Permissions.Edit{1}{0}, Category = {0}{1}{0} {3};           ", consTemp, pTable.OriginalTableName, "{", "}");
                    sb.AppendLine(" ");
                    sb.AppendFormat("public static readonly PermissionRecord Delete{1} = new PermissionRecord {2} Name = {0}Delete {1}{0}, SystemName = {0}Permissions.Delete{1}{0}, Category = {0}{1}{0} {3};     ", consTemp, pTable.OriginalTableName, "{", "}");


                    sb.AppendLine(" ");
                    sb.AppendLine(" ");
                    sb.AppendFormat("public const string View{1}Permission = {0}Permissions.View{1}{0}; ", consTemp, pTable.OriginalTableName);
                    sb.AppendLine(" ");
                    sb.AppendFormat("public const string Create{1}Permission = {0}Permissions.Create{1}{0};", consTemp, pTable.OriginalTableName);
                    sb.AppendLine(" ");
                    sb.AppendFormat("public const string Edit{1}Permission = {0}Permissions.Edit{1}{0}; ", consTemp, pTable.OriginalTableName);
                    sb.AppendLine(" ");
                    sb.AppendFormat("public const string Delete{1}Permission = {0}Permissions.Delete{1}{0};", consTemp, pTable.OriginalTableName);
                    sb.AppendLine(" ");
                    sb.AppendFormat("");
                    sb.AppendLine("");

                    //end logic
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            sb.AppendLine("");
            sb.AppendLine("public override IEnumerable<PermissionRecord> GetPermissions()");
            sb.AppendLine("{");
            sb.AppendLine("return new[]");
            sb.AppendLine("{");

            foreach (var pTable in oTableList)
            {


                try
                {
                    sb.AppendFormat("//{0}", pTable.OriginalTableName.ToUpper());
                    sb.AppendLine("");
                    sb.AppendFormat("View{0},", pTable.OriginalTableName);
                    sb.AppendLine(" ");
                    sb.AppendFormat("Create{0},", pTable.OriginalTableName);
                    sb.AppendLine(" ");
                    sb.AppendFormat("Edit{0},", pTable.OriginalTableName);
                    sb.AppendLine(" ");
                    sb.AppendFormat("Delete{0},", pTable.OriginalTableName);

                    sb.AppendLine(" ");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            sb.AppendLine(" };");
            sb.AppendLine(" }");

            sw.WriteLine(sb.ToString());
            #region Close file
            if (sw != null)
            {
                sw.Close();
            }
            #endregion
        }

        //--------------------------------------------------- PermissionProvider---------------------------
        public void GenerateGetAllFunctionXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\DBFunctions\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + pTable.OriginalTableName + @"\DBFunctions"
                            + string.Format("\\Get{0}", pTable.OriginalTableName));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".sql");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.AppendFormat("CREATE OR REPLACE FUNCTION {0}.Get{1}()", "employee", pTable.OriginalTableName);
                    sb.AppendLine("");
                    sb.AppendLine("RETURNS TABLE (");
                    // loop for output params
                    foreach (var oProp in pTable.PropetyList)
                    {
                        if (oProp.DBType.ToLower().Contains("varchar"))
                        {
                            sb.AppendLine(string.Format("\t\t\"{0}\" character varying({1}) ,", oProp.SYSName, oProp.DBLength));
                        }
                        else if (oProp.OriginalDBType.ToLower().Contains("uniq"))
                        {
                            sb.AppendLine(string.Format("\t\t\"{0}\" uuid ,", oProp.SYSName));
                        }
                        else
                        {
                            sb.AppendLine(string.Format("\t\t\"{0}\" {1} ,", oProp.SYSName, oProp.DBType));
                        }
                    }

                    sb.AppendLine(")");

                    sb.AppendLine("AS $BODY$");

                    sb.AppendLine("\tBEGIN");
                    sb.AppendLine("\tRETURN QUERY");


                    sb.AppendLine("\t\tSELECT ");
                    foreach (var oProp in pTable.PropetyList)
                    {
                        sb.AppendLine(string.Format("\t\t\t{0}.\"{1}\" , ", pTable.OriginalTableName, oProp.DBName));

                    }

                    sb.AppendLine(string.Format("\t\t FROM {0}.\"{1}\" AS {2} ", pTable.TableSchemaName, pTable.OriginalTableName, pTable.OriginalTableName));
                    sb.AppendLine(string.Format("\t\t WHERE {0}.\"IsDeleted\" = False ;", pTable.OriginalTableName));




                    sb.AppendLine("\tEND;");
                    sb.AppendLine("$BODY$");
                    sb.AppendLine("LANGUAGE plpgsql;");



                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // -------------------------- ANGULAR 
        public void GenerateAngularModelXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\Models\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\Models\"
                            + string.Format("\\{0}", pTable.OriginalTableName.ToLower()));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".ts");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContentWithTemplatePath(pTable, "Model.html", "Angular"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public void GenerateAngularServiceXHR(TableModel pTable)
        {
            //-------------------------
            string rawString = @"import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { [[MODEL]] } from '../../models';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class [[MODEL]]Service extends BaseService{
  
  private [[model]]BaseUrl = this.Base_API_URL +'[[MODEL]]';
  constructor(private http:HttpClient) {super(); }

  getAll[[MODEL]]ByCompanyId(companyId):Observable<[[MODEL]][]>{
    return this.http.get<[[MODEL]][]>(this.[[model]]BaseUrl+'/company/'+companyId);
   }
 
   get[[MODEL]]ById([[model]]Id: number): Observable<any[]> {
     return this.http.get<any[]>(this.[[model]]BaseUrl  + [[model]]Id);
   }

  create[[MODEL]]([[model]]: [[MODEL]]) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.[[model]]BaseUrl, [[model]], requestHeader);
  }

  edit[[MODEL]]([[model]]: [[MODEL]]) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.[[model]]BaseUrl, [[model]], requestHeader);
  }

  delete[[MODEL]]ById([[model]]Id: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.[[model]]BaseUrl + '/' + [[model]]Id, requestHeader);
  }

  delete[[MODEL]]([[model]]: any) {
    let httpParams=new HttpParams().set('id',[[model]].id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body:[[model]]
    };
    return this.http.delete(this.[[model]]BaseUrl , requestHeader);
  }
  
}";
            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\Services\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName  + @"\Angular\Services"
                            + string.Format("\\{0}.service", pTable.OriginalTableName.ToLower()));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".ts");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);

                    var serviceContent = rawString.Replace("[[MODEL]]", pTable.OriginalTableName).Replace("[[model]]", pTable.OriginalTableName.ToLower());

                    sw.WriteLine(serviceContent);
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
