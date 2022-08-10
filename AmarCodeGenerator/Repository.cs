using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    public class Repository
    {
        public void GenerateRepository(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RepsitoryFolder);
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RepsitoryFolder + pTable.RepositoryName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "Repository.html"));


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

        public void GenerateRepositoryInterface(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RepsitoryInterfaceFolder);
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RepsitoryInterfaceFolder + pTable.RepositoryInterfaceName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "IReposotories.html"));


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
    }
}
