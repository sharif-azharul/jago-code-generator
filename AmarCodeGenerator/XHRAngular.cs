using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    public class XHRAngular
    {
        public void ListComponent(TableModel pTable)
        {
            //-------------------------
            string rawString = @"import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { [[MODEL]] } from 'src/app/shared/models/company/[[model]]';
import { Create[[MODEL]]Component } from './create-[[model]]/create-[[model]].component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, [[MODEL]]Service } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';


@Component({
  selector: 'app-[[model]]',
  templateUrl: './[[model]].component.html',
  styleUrls: ['./[[model]].component.css']
})
export class [[MODEL]]Component extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('[[model]]-list-section') blockUI: NgBlockUI
  [[model]]s: [[MODEL]][];
  [[model]]Id: any;
  [[model]]FilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;

  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private [[model]]Service: [[MODEL]]Service,
    private companyService: CompanyService,
    private injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }

  buildForm() {
    this.[[model]]FilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }

  get f() { return this.[[model]]FilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAll[[MODEL]]ByCompanyId();
    }
  }
  get[[MODEL]]() {
    this.submitted = true;
    if (this.[[model]]FilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAll[[MODEL]]ByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  create[[MODEL]]() {
    const create[[MODEL]]DialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    create[[MODEL]]DialogConfig.disableClose = true;
    create[[MODEL]]DialogConfig.autoFocus = true;
    create[[MODEL]]DialogConfig.panelClass = 'xHR-dialog';
    create[[MODEL]]DialogConfig.width = '50%';
            var [[model]] = new [[MODEL]]();
            [[model]].companyId = this.companyId;
            create[[MODEL]]DialogConfig.data = [[model]];
            const create[[MODEL]]Dialog = this.dialog.open(Create[[MODEL]]Component, create[[MODEL]]DialogConfig);
            const successfullCreate = create[[MODEL]]Dialog.componentInstance.on[[MODEL]]CreateEvent.subscribe((data) => {
                this.getAll[[MODEL]]ByCompanyId();
            });
            create[[MODEL]]Dialog.afterClosed().subscribe(() => {
            });
        }
  edit[[MODEL]]([[model]]: [[MODEL]])
        {

            const editDialogConfig = new MatDialogConfig();
            // Setting different dialog configurations
            editDialogConfig.disableClose = true;
            editDialogConfig.autoFocus = true;
            editDialogConfig.data = [[model]]
    editDialogConfig.panelClass = 'xHR-dialog';
            editDialogConfig.width = '50%';

            const outletEditDialog = this.dialog.open(Create[[MODEL]]Component, editDialogConfig);
            const successFullEdit = outletEditDialog.componentInstance.on[[MODEL]]EditEvent.subscribe((data) => {
                this.getAll[[MODEL]]ByCompanyId();
            });
            outletEditDialog.afterClosed().subscribe(() => {
            });
        }

  onDelete[[MODEL]]([[model]]: [[MODEL]]): void {
    const confirmationDialogConfig = new MatDialogConfig();
        // Setting different dialog configurations
        confirmationDialogConfig.data = 'Are you sure to delete the [[MODEL]] ' + [[model]].[[MODEL]]Name;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

        confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.delete[[MODEL]]([[model]]);
    }
});
  }
  delete[[MODEL]]([[model]]: [[MODEL]])
{
    this.[[model]]Service.delete[[MODEL]]([[model]]).subscribe((data) => {
        this.getAll[[MODEL]]ByCompanyId();
    },
      (error) => {
          console.log(error);
      });
}

  getAll[[MODEL]]ById(brnchId)
{
    this.[[model]]Service.get[[MODEL]]ById(brnchId).subscribe(result => {
        this.[[model]]s = result;
    })
  }

  getAll[[MODEL]]ByCompanyId()
{
    this.blockUI.start();
    this.[[model]]Service.getAll[[MODEL]]ByCompanyId(this.companyId).subscribe(result => {
        this.[[model]]s = result;

        this.totalItems = this.[[model]]s.length;
        this.generateTotalItemsText();

        this.blockUI.stop();
    }, error => {
        this.blockUI.stop();
    })

  }
}
";
            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower()
                            + string.Format("\\{0}.component", pTable.OriginalTableName.ToLower()));
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

        public void ListComponentSpec(TableModel pTable)
        {
            //-------------------------
            string rawString = @"import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { [[MODEL]]Component } from './[[model]].component';

describe('[[MODEL]]Component', () => {
  let component: [[MODEL]]Component;
  let fixture: ComponentFixture<[[MODEL]]Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ [[MODEL]]Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent([[MODEL]]Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
";
            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower()
                            + string.Format("\\{0}.component.spec", pTable.OriginalTableName.ToLower()));
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

        public void ListComponentHTML(TableModel pTable)
        {

            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower()
                            + string.Format("\\{0}.component", pTable.OriginalTableName.ToLower()));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".html");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);

                    //-------------------------
                    sb.AppendLine("<div class=\"row d-flex justify-content-between mt-3 mb-3\"> ");
                    sb.AppendLine(" <div class=\"col-sm-12 col-md-4\"> ");
                    sb.AppendLine("    <h1 id = \"tableLabel\" >{{'COMPANY.[[MODEL]].INDEX.TITLE' | translate }}</h1>");
                    sb.AppendLine("  </div> ");
                    sb.AppendLine("  <div class=\"col-lg-2 col-md-3 col-sm-10\"> ");
                    sb.AppendLine("      <button mat-raised-button (click)=\"create[[Model]]()\" class=\"btn btn-success btn-block text-center\" ");
                    sb.AppendLine("          matTooltip=\"{{'COMPANY.[[MODEL]].INDEX.CREATE[[MODEL]]BUTTONTEXT' | translate }}\"> ");
                    sb.AppendLine("          <mat-icon class=\"create-icon-button\">add</mat-icon> ");
                    sb.AppendLine("          {{'COMPANY.[[MODEL]].INDEX.CREATE[[MODEL]]BUTTONTEXT' | translate }} ");
                    sb.AppendLine("      </button> ");
                    sb.AppendLine("  </div> ");
                    sb.AppendLine("</div> ");
                    sb.AppendLine(" ");
                    sb.AppendLine("<form[formGroup]=\"[[model]]FilterFormGroup\" (ngSubmit)=\"get[[Model]]()\"> ");
                    sb.AppendLine("  <div class=\"row\"> ");
                    sb.AppendLine("    <div class=\"col-sm-6 col-md-4\"> ");
                    sb.AppendLine("      <mat-form-field appearance = \"outline\" > ");
                    sb.AppendLine("        <mat-label >{{'COMMON.SELECT.COMPANYID.PLACEHOLDER' | translate }}</mat-label> ");
                    sb.AppendLine("        <mat-select placeholder = \"{{'COMMON.SELECT.COMPANYID.PLACEHOLDER' | translate }}\" formControlName=\"companyId\" (selectionChange)=\"onChangeCompany($event.value)\"> ");
                    sb.AppendLine("          <mat-option *ngFor = \"let item of companies\"[value] = \"item.id\" > ");
                    sb.AppendLine("            {{item.companyName}} ");
                    sb.AppendLine("          </mat-option> ");
                    sb.AppendLine("        </mat-select> ");
                    sb.AppendLine("        <div *ngIf = \"submitted && f.companyId.errors\" > ");
                    sb.AppendLine("          <mat-error *ngIf = \"f.companyId.errors.required\" > Company is required</mat-error> ");
                    sb.AppendLine("        </div> ");
                    sb.AppendLine("      </mat-form-field > ");
                    sb.AppendLine("    </div>");

                    sb.AppendLine("  </div> ");
                    sb.AppendLine(" ");
                    sb.AppendLine("</form> ");
                    sb.AppendLine("<section *blockUI \"'[[model]]-list-section'\">");
                    sb.AppendLine("  <table class=\"table table-striped\" aria-labelledby=\"tableLabel\">");
                    sb.AppendLine("    <thead>");
                    sb.AppendLine("      <tr>");

                    sb.AppendLine("        <th class=\"text-center\">{{'COMPANY.[[MODEL]].INDEX.TABLE.HEADER.ACTIONS' | translate }}</th>");

                    foreach (var oProp in pTable.PropetyList)
                    {
                        if (!oProp.DBType.ToLower().Contains("uniq"))
                            sb.AppendLine(string.Format("        <th>{{{{'COMPANY.{0}.INDEX.TABLE.HEADER.{1}' | translate }}}}</th>", pTable.OriginalTableName.ToUpper(), oProp.DBName.ToUpper()));
                    }
                    sb.AppendLine("</tr>");
                    sb.AppendLine("</thead> ");
                    sb.AppendLine("<tbody> ");
                    sb.AppendLine("<tr *ngFor = \"let item of [[model]]s | paginate: {id:'pagination', itemsPerPage: pageSize, currentPage: currentPage, totalItems: totalItems  }; let $index = index\"> ");
                    sb.AppendLine("        <td class=\"pt-0 text-center\"> ");
                    sb.AppendLine("          <button mat-icon-button (click)=\"edit[[Model]](item)\"> ");
                    sb.AppendLine("            <mat-icon>create</mat-icon> ");
                    sb.AppendLine("          </button> ");

                    sb.AppendLine("          <button mat-icon-button (click)=\"onDelete[[Model]](item)\"> ");
                    sb.AppendLine("            <mat-icon class=\"mat-icon-delete\">delete</mat-icon> ");
                    sb.AppendLine("          </button> ");
                    sb.AppendLine("        </td>");
                    foreach (var oProp in pTable.PropetyList)
                    {
                        if (!oProp.DBType.ToLower().Contains("uniq"))
                            sb.AppendLine(string.Format("        <td>{{{{item.{0}}}}}</td> ", oProp.ParameterFormatName));
                    }
                    sb.AppendLine("         ");
                    sb.AppendLine("      </tr> ");
                    sb.AppendLine("    </tbody> ");
                    sb.AppendLine("  </table> ");
                    sb.AppendLine(" ");
                    sb.AppendLine("  <div class=\"row justify-content-between\" *ngIf = \"totalItems > 0\">");
                    sb.AppendLine("    <div class=\"col-md-5 col-sm-5 text-left\"> ");
                    sb.AppendLine("      {{totalItemsText}} ");
                    sb.AppendLine("    </div> ");

                    sb.AppendLine("    <div class=\"col-md-5 col-sm-5 text-left\"> ");
                    sb.AppendLine("      <pagination-controls id = \"pagination\" *ngIf = \"totalItems > 0\"(pageChange) = \"pageChanged($event)\" ");
                    sb.AppendLine("        directionLinks=\"true\" previousLabel=\"\" nextLabel=\"\"> ");
                    sb.AppendLine("      </pagination-controls> ");
                    sb.AppendLine("    </div> ");

                    sb.AppendLine("    <div class=\"col-md-2 col-sm-2 text-left\"> ");
                    sb.AppendLine("      <mat-form-field> ");
                    sb.AppendLine("        <mat-select [(ngModel)]=\"pageSize\" (selectionChange)=\"onChangePaginationPerPage()\" placeholder=\"Items Per Page\"> ");
                    sb.AppendLine("          <mat-option *ngFor = \"let item of paginationPageNumbers\"[value] = \"item.value\" >");
                    sb.AppendLine("            {{item.text}} ");
                    sb.AppendLine("          </mat-option> ");
                    sb.AppendLine("        </mat-select> ");
                    sb.AppendLine("   ");
                    sb.AppendLine("      </mat-form-field> ");
                    sb.AppendLine("    </div> ");
                    sb.AppendLine("  </div> ");
                    sb.AppendLine("</section> ");





                    var serviceContent = sb.ToString().Replace("[[Model]]", pTable.OriginalTableName)
                        .Replace("[[model]]", pTable.OriginalTableName.ToLower())
                        .Replace("[[MODEL]]", pTable.OriginalTableName.ToUpper());

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

        public void ListComponentCSS(TableModel pTable)
        {
            //-------------------------

            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower()
                            + string.Format("\\{0}.component", pTable.OriginalTableName.ToLower()));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".css");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);

                    var serviceContent = "";

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

        //------ Create component
        public void CreateComponent(TableModel pTable)
        {
            //-------------------------
            string rawString = @"import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
                            import { FormBuilder, Validators, FormGroup } from '@angular/forms';
                            import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
                            import { [[Model]] } from 'src/app/shared/models';
                            import { CompanyService ,[[Model]]Service} from 'src/app/shared/services';
                            import { AlertService } from '../../../shared/services/alert.service';

                            @Component({
                              selector: 'app-create-[[model]]',
                              templateUrl: './create-[[model]].component.html',
                              styleUrls: ['./create-[[model]].component.css']
                            })
                            export class Create[[Model]]Component implements OnInit {
                              on[[Model]]CreateEvent: EventEmitter<any> = new EventEmitter();
                              on[[Model]]EditEvent: EventEmitter<any> = new EventEmitter();

                              [[model]]CreateForm: FormGroup
                              submitted = false;
                              companyId: any;
                              companies: any;
                              [[model]]: [[Model]]=new [[Model]]();
                              [[model]]Id: number;
                              isEditMode = false;

                              constructor(
                                private companyService: CompanyService,
                                private alertService:AlertService,
                                private dialogRef: MatDialogRef<Create[[Model]]Component>,
                                private formBuilder: FormBuilder,
                                private [[model]]service: [[Model]]Service,
                                @Inject(MAT_DIALOG_DATA) data) {
                                this.[[model]] = new [[Model]]();
                                if (isNaN(data)) {
      
                                  console.log(new [[Model]](data));
                                  this.[[model]] = new [[Model]](data);
                                  this.companyId = this.[[model]].companyId;

                                } else {

                                }
                                this.buildForm();
                                this.getAllCompanies();
                              }

                              ngOnInit() {
                                this.getAllCompanies();
                              }

                              getAllCompanies() {
                                this.companyService.getAllCompanies().subscribe((result: any) => {
                                  this.companies = result;
                                })
                              }

                              onChangeCompany() {
                                this.companyId = this.f.companyId.value;
                              }

                              buildForm() {
                                this.[[model]]CreateForm = this.formBuilder.group({
                                    [[CREATE_FORM]]
                                  
                                });
                              }

                              onSubmit() {
                                this.submitted = true;
                                // stop here if form is invalid
                                if (this.[[model]]CreateForm.invalid) {
                                  return;
                                }
                                if (this.[[model]].id === undefined) {
      
                                  this.create[[Model]]();
                                }
                                else {
                                  this.edit[[Model]]();
                                }
                                this.dialogRef.close();
                              }

                              create[[Model]]() {
                                this.[[model]] = new [[Model]](this.[[model]]CreateForm.value);
                                this.[[model]]service.create[[Model]](this.[[model]]).subscribe((data: any) => {
                                  this.on[[Model]]CreateEvent.emit(this.[[model]].id);
                                  this.alertService.success('[[Model]] added successfully');
                                }, (error: any) => {
                                  this.alertService.error(error);
                                });
                              }


                              edit[[Model]]()
                            {
                                let newData = new [[Model]](this.[[model]]CreateForm.value);
                                if (this.[[model]] !== null)
                                {
                                    [[EDIT_HMMM]]

                                        this.[[model]]service.edit[[Model]](this.[[model]]).subscribe((data: any) => {
                                        this.on[[Model]]EditEvent.emit(this.[[model]].id)
                                      this.alertService.success('[[Model]] updated successfully');
                                    }, (error: any) => {
                                        this.alertService.error(error);
                                    });
                                }
                            }

                              close()
                            {
                                this.dialogRef.close();
                            }
                              showErrorMessage(error: any)
                            {
                                console.log(error);

                            }
                            get f() { return this.[[model]]CreateForm.controls; }
                            }

                            ";

            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\" + "create-" + pTable.OriginalTableName.ToLower() + "\\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\" + "create-" + pTable.OriginalTableName.ToLower() + "\\"
                            + string.Format("\\create-{0}.component", pTable.OriginalTableName.ToLower()));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".ts");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);

                    var serviceContent = rawString.Replace("[[Model]]", pTable.OriginalTableName).Replace("[[model]]", pTable.OriginalTableName.ToLower());
                    //---------------------------
                    // loop for output params
                    foreach (var oProp in pTable.PropetyList)
                    {
                        if (oProp.DBType.ToLower().Contains("varchar"))
                        {
                            if (oProp.IsNullable)
                                sb.AppendLine(string.Format("\t\t{0}: [this.{1}.{2},[Validators.maxLength({3})]] ,", oProp.ParameterFormatName, pTable.OriginalTableName, oProp.ParameterFormatName, oProp.DBLength));
                            else
                                sb.AppendLine(string.Format("\t\t{0}: [this.{1}.{2},[Validators.required,Validators.maxLength({3})]] ,", oProp.ParameterFormatName, pTable.OriginalTableName, oProp.ParameterFormatName, oProp.DBLength));
                        }

                        else
                        {
                            sb.AppendLine(string.Format("\t\t{0}: [this.{1}.{2}] ,", oProp.ParameterFormatName, pTable.OriginalTableName, oProp.ParameterFormatName));
                        }
                    }
                    serviceContent = serviceContent.Replace("[[CREATE_FORM]]", sb.ToString());

                    //--------------------------- [[EDIT_HMMM]]
                    // loop for output params
                    sb.Length = 0;
                    foreach (var oProp in pTable.PropetyList)
                    {
                        sb.AppendLine(string.Format("\t\t this.{0}.{1} = newData.{2};", pTable.OriginalTableName, oProp.ParameterFormatName, oProp.ParameterFormatName));
                    }
                    serviceContent = serviceContent.Replace("[[EDIT_HMMM]]", sb.ToString());
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

        public void CreateComponentSpec(TableModel pTable)
        {
            //-------------------------
            string rawString = @"import { async, ComponentFixture, TestBed } from '@angular/core/testing';
                                import { Create[[Model]]Component } from './create-[[model]].component';
                                describe('Create[[Model]]Component', () => {
                                  let component: Create[[Model]]Component;
                                  let fixture: ComponentFixture<Create[[Model]]Component>;
                                  beforeEach(async(() => {
                                    TestBed.configureTestingModule({
                                      declarations: [ Create[[Model]]Component ]
                                    })
                                    .compileComponents();
                                  }));

                                  beforeEach(() => {
                                    fixture = TestBed.createComponent(Create[[Model]]Component);
                                    component = fixture.componentInstance;
                                    fixture.detectChanges();
                                  });

                                  it('should create', () => {
                                    expect(component).toBeTruthy();
                                  });
                                });

                            ";
            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\" + "create-" + pTable.OriginalTableName.ToLower() + "\\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\" + "create-" + pTable.OriginalTableName.ToLower() + "\\"
                            + string.Format("\\create-{0}.component.spec", pTable.OriginalTableName.ToLower()));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".ts");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);

                    var serviceContent = rawString.Replace("[[Model]]", pTable.OriginalTableName).Replace("[[model]]", pTable.OriginalTableName.ToLower());

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

        public void CreateComponentHTML(TableModel pTable)
        {

            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\" + "create-" + pTable.OriginalTableName.ToLower() + "\\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\" + "create-" + pTable.OriginalTableName.ToLower() + "\\"
                            + string.Format("\\create-{0}.component", pTable.OriginalTableName.ToLower()));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".html");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    sb.AppendLine(" <h5 mat-dialog-title *ngIf = \"!isEditMode\" >{{'COMPANY.[[MODEL]].CREATE.CREATE_TITLE' | translate}}</h5 >");

                    sb.AppendLine(" <h5 mat-dialog-title *ngIf = \"isEditMode\" >{{'COMPANY.[[MODEL]].CREATE.EDIT_TITLE' | translate}}</h5>");

                    sb.AppendLine("  ");
                    sb.AppendLine(" <form class=\"[[model]]-from\" [formGroup]=\"[[model]]CreateForm\" enctype=\"multipart/form-data\"> ");
                    sb.AppendLine("<mat-dialog-content class=\"mat-typography\">");
                    sb.AppendLine("  ");
                    sb.AppendLine("<div class=\"form-group\">");
                    sb.AppendLine(" <mat-form-field appearance = \"outline\">");
                    sb.AppendLine("         <mat-label>{{ 'COMMON.SELECT.COMPANYID.PLACEHOLDER' | translate }}</mat-label>");

                    sb.AppendLine("         <mat-select matInput formControlName=\"companyId\" placeholder=\"{{'COMMON.SELECT.COMPANYID.PLACEHOLDER' | translate }}\" [(value)]=\"[[model]].companyId\" (selectionChange)=\"onChangeCompany()\">");
                    sb.AppendLine("           <ng-container *ngFor = \"let company of companies\" > ");
                    sb.AppendLine("             <mat-option value=\"{{company.id}}\">{{company.companyName}}</mat-option> ");
                    sb.AppendLine("           </ng-container> ");
                    sb.AppendLine("         </mat-select> ");
                    sb.AppendLine("         <mat-error *ngIf = \"submitted && f.companyId.errors && f.companyId.errors.required\" >{{'COMMON.SELECT.COMPANYID.REQUIERED_ERROR_TEXT' | translate }}</mat-error> ");
                    sb.AppendLine("       </mat-form-field> ");
                    sb.AppendLine("     </div> ");
                    sb.AppendLine("  ");

                    foreach (var oProp in pTable.PropetyList)
                    {
                        if (oProp.DBName.ToUpper() != "ID")
                        {
                            sb.AppendLine("     <div class=\"form-group\">");
                            sb.AppendLine("       <mat-form-field appearance = \"outline\"> ");
                            sb.AppendLine(string.Format("         <mat-label>{{{{ 'COMPANY.{0}.CREATE.CREATE_FORM.{1}.PLACEHOLDER' | translate }}}}</mat-label> "
                                , pTable.OriginalTableName.ToUpper(), oProp.DBName.ToUpper()));
                            sb.AppendLine(string.Format("         <input matInput formControlName=\"{0}\" placeholder=\"{{{{'COMPANY.{1}.CREATE.CREATE_FORM.{2}.PLACEHOLDER' | translate }}}}\" [ngClass]=\"{{'is-invalid': submitted && f.{3}.errors}}\"/>"
                                , oProp.ParameterFormatName, pTable.OriginalTableName.ToUpper(), oProp.DBName.ToUpper(), oProp.ParameterFormatName));
                            sb.AppendLine(string.Format("<mat-error *ngIf = \"submitted && f.{0}.errors && f.{1}.errors.required\" >{{{{ 'COMPANY.{2}.CREATE.CREATE_FORM.{3}.REQUIERED_ERROR_TEXT' | translate }}}}</mat-error> "
                                , oProp.ParameterFormatName, oProp.ParameterFormatName, pTable.OriginalTableName.ToUpper(), oProp.DBName.ToUpper()));
                            sb.AppendLine(string.Format("         <mat-error *ngIf = \"submitted && f.{0}.errors && f.{1}.errors.maxlength\" >{{{{'COMPANY.{2}.CREATE.CREATE_FORM.{3}.MAXLENGTH_ERROR_TEXT' | translate }}}}</mat-error>"
                                , oProp.ParameterFormatName, oProp.ParameterFormatName, pTable.OriginalTableName.ToUpper(), oProp.DBName.ToUpper()));
                            sb.AppendLine("       </mat-form-field> ");
                            sb.AppendLine("     </div> ");
                            sb.AppendLine("  ");
                        }
                    }
                    sb.AppendLine("  ");
                    sb.AppendLine("   </mat-dialog-content> ");
                    sb.AppendLine("  ");
                    sb.AppendLine("   <mat-dialog-actions> ");
                    sb.AppendLine("     <button mat-raised-button mat-dialog-close (click)=\"close()\">{{'COMMON.CANCEL_BUTTON_TEXT' | translate }}</button> ");
                    sb.AppendLine("     <button mat-raised-button (click)=\"onSubmit()\" class=\"btn btn-success\">{{ 'COMMON.SAVE_BUTTON_TEXT' | translate }}</button> ");
                    sb.AppendLine("   </mat-dialog-actions> ");
                    sb.AppendLine(" </form>");

                    var serviceContent = sb.ToString().Replace("[[Model]]", pTable.OriginalTableName)
                        .Replace("[[model]]", pTable.OriginalTableName.ToLower())
                        .Replace("[[MODEL]]", pTable.OriginalTableName.ToUpper());

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


        public void CreateComponentCSS(TableModel pTable)
        {
            //================================
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\" + "create-" + pTable.OriginalTableName.ToLower() + "\\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.RootFolderName + @"\Angular\" + pTable.OriginalTableName.ToLower() + "\\" + "create-" + pTable.OriginalTableName.ToLower() + "\\"
                            + string.Format("\\create-{0}.component", pTable.OriginalTableName.ToLower()));
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".css");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    var serviceContent = sb.ToString().Replace("[[Model]]", pTable.OriginalTableName)
                        .Replace("[[model]]", pTable.OriginalTableName.ToLower())
                        .Replace("[[MODEL]]", pTable.OriginalTableName.ToUpper());

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
