using AmarSomoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmarSomoy.Models.ViewModel;
using Telerik.Web.Mvc;
using System.Data.Entity;
using UPASS.EmailSender;
using System.Text;
using AmarSomoy.Infrastructure.Alerts;

namespace AmarSomoy.Controllers
{
    public class TaskController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private int pCode;

        // GET: Project
        public ActionResult Index()
        {
            PrepareViewBag("", "");
            return View();
        }

        // GET: Project/Details/5
        public ActionResult Details(int pId)
        {
            var com = db.Tasks.FirstOrDefault(co => co.TaskId == pCode);
            return View(com);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            var profile = db.Profiles.FirstOrDefault(p => p.UserId == SessionUtility.SessionContainer.USER_ID);
            PrepareViewBag(profile.CompanyCode, "");
            TaskModel task = new TaskModel();
            task.TaskOwner = profile.FirstName + " " + profile.LastName;
            task.TaskOwnerId = profile.UserId;
            return View(task);
        }

        private void PrepareViewBag(string pCompanyCode, string pProjectCode)
        {
            var CompList = db.Companies.Where(com => com.IsDeleted == false);

            ViewBag.CompanyList = new SelectList(CompList.OrderBy(k => k.CompanyName), "CompanyCode", "CompanyName", pCompanyCode);

            var ProjectList = db.Projects.Where(com => com.IsDeleted == false);

            ViewBag.ProjectList = new SelectList(ProjectList.OrderBy(k => k.ProjectName), "ProjectCode", "ProjectName", pProjectCode);
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(TaskModel pTask)
        {
            PrepareViewBag(pTask.CompanyCode, pTask.ProjectCode);
            if (ModelState.IsValid)
            {
                if (!ValidateTimeOverlap(false, pTask))
                {
                    try
                    {
                        pTask.IsNew = true;
                        base.SetObjectStatus(pTask);
                        db.Tasks.Add(pTask);
                        db.SaveChanges();
                        return Json(new { STATUS = "Success", MESSAGE = "Data saved successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { STATUS = "Exception", MESSAGE = ex.Message }, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return RedirectToAction<TaskController>(r => r.Create(pTask)).WithSuccess("Time overlapped");
                }
            }
            return View(pTask).WithError("Invalid Model");

        }

        private bool ValidateTimeOverlap(Boolean pIsUpdate, TaskModel pTask)
        {
            Boolean IsOverlap = false;
            if (!pIsUpdate)
            {
                var tasks = (from tDB in db.Tasks
                             where ((tDB.StartTime >= pTask.StartTime && tDB.StartTime != null)
                             && (tDB.StartTime <= pTask.EndTime && tDB.StartTime != null))
                            || ((tDB.EndTime >= pTask.StartTime && tDB.EndTime != null)
                             && (tDB.EndTime <= pTask.EndTime && tDB.EndTime != null))
                             && tDB.TaskOwnerId == pTask.TaskOwnerId && tDB.IsDeleted == false
                             select new TaskViewModel()
                             {
                                 TaskId = tDB.TaskId,
                             }).ToList();
                if (tasks != null && tasks.Count() > 0)
                    IsOverlap = true;

            }
            return IsOverlap;
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int pId)
        {
            var task = db.Tasks.FirstOrDefault(co => co.TaskId == pId);
            PrepareViewBag(task.CompanyCode, task.ProjectCode);
            var profile = db.Profiles.FirstOrDefault(p => p.UserId == SessionUtility.SessionContainer.USER_ID);
            task.TaskOwner = profile.FirstName + " " + profile.LastName;
            task.TaskOwnerId = profile.UserId;
            return View(task);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(int pId, TaskModel pTask)
        {
            try
            {
                var task = db.Tasks.FirstOrDefault(co => co.TaskId == pId);
                task.StartTime = pTask.StartTime;
                task.EndTime = pTask.EndTime;
                task.TaskDescription = pTask.TaskDescription;
                task.IsNew = false;
                base.SetObjectStatus(task);
                db.SaveChanges();

                return RedirectToAction<TaskController>(r => r.Index()).WithSuccess(string.Format("Task : '{0}' updated", pTask.TaskDescription));
            }
            catch
            {
                return View();
            }
        }


        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [GridAction(EnableCustomBinding = true)]
        public virtual ActionResult ListTaskAjax(GridCommand command, SearchTaskModel search, Boolean IsFistLoad)
        {
            int TotalRecord = 0;
            int StartRecordIndex = (command.Page - 1) * command.PageSize;
            int EndRecordIndex = StartRecordIndex + command.PageSize;
            if (IsFistLoad)
                search.EndTime = DateTime.Now;
            var gridModel = new GridModel();
            var tasks = (from task in db.Tasks
                         join projects in db.Projects
                         on task.ProjectCode equals projects.ProjectCode
                         where ((DbFunctions.TruncateTime(task.StartTime) >= DbFunctions.TruncateTime(search.StartTime) && task.StartTime != null) || search.StartTime == null)
                         && ((DbFunctions.TruncateTime(task.StartTime) <= DbFunctions.TruncateTime(search.EndTime) && task.StartTime != null) || search.StartTime == null)
                         && (task.ProjectCode == search.ProjectCode || search.ProjectCode == null || search.ProjectCode == "")
                         select new TaskViewModel()
                         {
                             TaskId = task.TaskId,
                             TaskDescription = task.TaskDescription,
                             StartTime = task.StartTime,
                             EndTime = task.EndTime,
                             ProjectName = projects.ProjectName
                         }).ToList().OrderByDescending(or => or.StartTime);
            TotalRecord = tasks.ToList().Count();
            var GridTasks = tasks.ToList().Skip(StartRecordIndex).Take(EndRecordIndex);
            return View(new GridModel
            {
                Data = GridTasks,
                Total = TotalRecord
            });
        }

        //================================REPORT============================

        public ActionResult Report()
        {
            PrepareViewBag("", "");
            return View();
        }

        [GridAction(EnableCustomBinding = true)]
        public virtual ActionResult ReportTaskAjax(GridCommand command, SearchTaskModel search, Boolean IsFistLoad)
        {
            int TotalRecord = 0;
            int StartRecordIndex = (command.Page - 1) * command.PageSize;
            int EndRecordIndex = StartRecordIndex + command.PageSize;
            if (IsFistLoad)
                search.EndTime = DateTime.Now;
            var gridModel = new GridModel();
            var tasks = (from task in db.Tasks
                         join projects in db.Projects
                         on task.ProjectCode equals projects.ProjectCode
                         where ((DbFunctions.TruncateTime(task.StartTime) >= DbFunctions.TruncateTime(search.StartTime) && task.StartTime != null) || search.StartTime == null)
                         && ((DbFunctions.TruncateTime(task.StartTime) <= DbFunctions.TruncateTime(search.EndTime) && task.StartTime != null) || search.StartTime == null)
                         && (task.ProjectCode == search.ProjectCode || search.ProjectCode == null || search.ProjectCode == "")
                         select new TaskViewModel()
                         {
                             TaskId = task.TaskId,
                             TaskDescription = task.TaskDescription,
                             StartTime = task.StartTime,
                             EndTime = task.EndTime,
                             ProjectName = projects.ProjectName
                         }).ToList().OrderByDescending(or => or.StartTime);
            TotalRecord = tasks.ToList().Count();
            var GridTasks = tasks.ToList().Skip(StartRecordIndex).Take(EndRecordIndex);

            SessionUtility.SessionContainer.SetObject<List<TaskViewModel>>("TASKS", tasks.ToList());
            return View(new GridModel
            {
                Data = GridTasks,
                Total = TotalRecord
            });
        }

        public ActionResult UpdateSession(int TaskId, Boolean IsSelected)
        {
            var tasks = SessionUtility.SessionContainer.GetObject<List<TaskViewModel>>("TASKS");
            var task = tasks.FirstOrDefault(t => t.TaskId == TaskId).IsSelectedReport = IsSelected;

            var tasks2Report = SessionUtility.SessionContainer.GetObject<List<TaskViewModel>>("TASKS2REPORT");
            if (tasks2Report == null)
                tasks2Report = new List<TaskViewModel>();
            var task2Report = tasks2Report.FirstOrDefault(t2 => t2.TaskId == TaskId);
            if (task2Report == null)
            {
                tasks2Report.Add(task2Report);
            }
            else if (!IsSelected)
            {
                tasks2Report.Remove(task2Report);
            }
            SessionUtility.SessionContainer.SetObject<List<TaskViewModel>>("TASKS2REPORT", tasks2Report.ToList());

            return Json(new { data = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendReport()
        {
            TimeSpan ts = new TimeSpan();
            TaskReportViewModel taskReport = new TaskReportViewModel();
            var tasks = SessionUtility.SessionContainer.GetObject<List<TaskViewModel>>("TASKS2REPORT");
            taskReport.profile = db.Profiles.FirstOrDefault(pr => pr.UserId == SessionUtility.SessionContainer.USER_ID);
            taskReport.tasks = tasks.OrderByDescending(or => or.StartTime).ToList();

            SetColorClass(taskReport.tasks, ts);
            taskReport.TotalWorkingHour = ts.ToString("h'h 'm'm 's's'");
            string EmailContent = HttpUtility.HtmlDecode(PrepareMailContent(taskReport, "TaskReport.html"));

            EmailContent = ReplaceColorClass(EmailContent);

            UPASS.EmailSender.EmailService em = new UPASS.EmailSender.EmailService();

            em.SendEmail("Daily status report- " + DateTime.Now.ToString("dd/MM/yyyy"), EmailContent, "ssarwarbd@gmail.com", true);

            //SessionUtility.SessionContainer.SetObject<List<TaskViewModel>>("TASKS", tasks.ToList());

            return Json(new { data = "Success" }, JsonRequestBehavior.AllowGet);
        }

        private string ReplaceColorClass(string emailContent)
        {
            string cl = emailContent.Replace("[[grey]]", "'background-color: darkgray !important;'")
                     .Replace("[[default]]", "'background-color: white !important;'");
            return cl;
        }

        private void SetColorClass(List<TaskViewModel> tasks, TimeSpan ts)
        {
            int i = 0;
            string trColor = "[[grey]]";
            DateTime PreviousDate = DateTime.Now;
            foreach (var task in tasks)
            {
                if (task.StartTime.Date == PreviousDate.Date || i == 0)
                {
                    if (trColor == "[[grey]]")
                    {
                        task.trColor = "[[grey]]";
                        trColor = "[[grey]]";
                    }
                    else
                    {
                        task.trColor = "[[default]]";
                        trColor = "[[default]]";
                    }
                }
                else
                {
                    if (trColor == "[[grey]]")
                    {
                        task.trColor = "[[default]]";
                        trColor = "[[default]]";
                    }
                    else
                    {
                        task.trColor = "[[grey]]";
                        trColor = "[[grey]]";
                    }
                }

                ts += task.TSWorkingHour;
                PreviousDate = task.StartTime;
            }
        }

        public ActionResult WeeklyReport()
        {
            SearchTaskModel search = new SearchTaskModel();
            PrepareViewBag("", "");
            DateTime date = DateTime.Now;
            search.StartTime = date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
            search.EndTime = search.StartTime.AddDays(6);
            return View(search);
        }

        [GridAction(EnableCustomBinding = true)]
        public virtual ActionResult WeeklyReportTaskAjax(GridCommand command, SearchTaskModel search, Boolean IsFistLoad)
        {
            int TotalRecord = 0;
            int StartRecordIndex = (command.Page - 1) * command.PageSize;
            int EndRecordIndex = StartRecordIndex + command.PageSize;
            if (IsFistLoad)
            {

                DateTime date = DateTime.Now;
                search.StartTime = date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
                search.EndTime = search.StartTime.AddDays(6);
            }
            var gridModel = new GridModel();
            var tasks = (from task in db.Tasks
                         join projects in db.Projects
                         on task.ProjectCode equals projects.ProjectCode
                         where ((DbFunctions.TruncateTime(task.StartTime) >= DbFunctions.TruncateTime(search.StartTime) && task.StartTime != null) || search.StartTime == null)
                         && ((DbFunctions.TruncateTime(task.StartTime) <= DbFunctions.TruncateTime(search.EndTime) && task.StartTime != null) || search.StartTime == null)
                         && (task.ProjectCode == search.ProjectCode || search.ProjectCode == null || search.ProjectCode == "")
                         select new TaskViewModel()
                         {
                             TaskId = task.TaskId,
                             TaskDescription = task.TaskDescription,
                             StartTime = task.StartTime,
                             EndTime = task.EndTime,
                             ProjectName = projects.ProjectName
                         }).ToList().OrderByDescending(or => or.StartTime);
            TotalRecord = tasks.ToList().Count();
            var GridTasks = tasks.ToList().Skip(StartRecordIndex).Take(EndRecordIndex);

            SessionUtility.SessionContainer.SetObject<List<TaskViewModel>>("TASKS2REPORT", tasks.ToList());
            return View(new GridModel
            {
                Data = GridTasks,
                Total = TotalRecord
            });
        }

        public ActionResult MonthlyReport()
        {
            SearchTaskModel search = new SearchTaskModel();
            PrepareViewBag("", "");
            DateTime date = DateTime.Now;
            search.StartTime = new DateTime(date.Year, date.Month, 1);
            search.EndTime = search.StartTime.AddMonths(1).AddDays(-1);
            return View(search);
        }

        [GridAction(EnableCustomBinding = true)]
        public virtual ActionResult MonthlyReportTaskAjax(GridCommand command, SearchTaskModel search, Boolean IsFistLoad)
        {
            int TotalRecord = 0;
            int StartRecordIndex = (command.Page - 1) * command.PageSize;
            int EndRecordIndex = StartRecordIndex + command.PageSize;
            if (IsFistLoad)
            {

                DateTime date = DateTime.Now;
                search.StartTime = new DateTime(date.Year, date.Month, 1);
                search.EndTime = search.StartTime.AddMonths(1).AddDays(-1);
            }
            var gridModel = new GridModel();
            var tasks = (from task in db.Tasks
                         join projects in db.Projects
                         on task.ProjectCode equals projects.ProjectCode
                         where ((DbFunctions.TruncateTime(task.StartTime) >= DbFunctions.TruncateTime(search.StartTime) && task.StartTime != null) || search.StartTime == null)
                         && ((DbFunctions.TruncateTime(task.StartTime) <= DbFunctions.TruncateTime(search.EndTime) && task.StartTime != null) || search.StartTime == null)
                         && (task.ProjectCode == search.ProjectCode || search.ProjectCode == null || search.ProjectCode == "")
                         select new TaskViewModel()
                         {
                             TaskId = task.TaskId,
                             TaskDescription = task.TaskDescription,
                             StartTime = task.StartTime,
                             EndTime = task.EndTime,
                             ProjectName = projects.ProjectName
                         }).ToList().OrderByDescending(or => or.StartTime);
            TotalRecord = tasks.ToList().Count();
            var GridTasks = tasks.ToList().Skip(StartRecordIndex).Take(EndRecordIndex);

            SessionUtility.SessionContainer.SetObject<List<TaskViewModel>>("TASKS2REPORT", tasks.ToList());
            return View(new GridModel
            {
                Data = GridTasks,
                Total = TotalRecord
            });
        }

    }
}
