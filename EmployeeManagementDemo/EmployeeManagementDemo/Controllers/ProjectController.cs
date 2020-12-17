using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagementDemo.Models;

namespace EmployeeManagementDemo.Controllers
{
    public class ProjectController : Controller
    {
        private ITCompanyDBEntities db = new ITCompanyDBEntities();

        // GET: Project
        public ActionResult Index()
        {
            return View(db.ProjectDetailsTables.ToList());
        }

        // GET: Project/Mapping/5
        public ActionResult Mapping(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectDetailsTable projectDetailsTable = db.ProjectDetailsTables.Find(id);
            if (projectDetailsTable == null)
            {
                return HttpNotFound();
            }
            ProjectMappingModel projectMappingModel = new ProjectMappingModel();
            projectMappingModel.ProjectEmployees = GetProjectEmployees(id);
            projectMappingModel.NonProjectEmployees = GetNonProjectEmployees(projectMappingModel.ProjectEmployees);
            projectMappingModel.Project = projectDetailsTable;
            return View(projectMappingModel);
        }

        private IEnumerable<EmployeeDetailsTable> GetProjectEmployees(int? id)
        {
            string commandText = "SELECT * FROM EmployeeProjectMapping WHERE ProjectID = @ID";
            SqlDataReader dr;
            List<int> ls = new List<int>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters["@ID"].Value = id;

                try
                {
                    connection.Open();
                    dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ls.Add(Convert.ToInt32(dr["EmployeeID"]));
                        }
                        List<EmployeeDetailsTable> employeeDetailsTable = db.EmployeeDetailsTables.ToList();
                        List<EmployeeDetailsTable> employees = new List<EmployeeDetailsTable>();
                        foreach(EmployeeDetailsTable employee in employeeDetailsTable)
                        {
                            foreach(int empId in ls)
                            {
                                if(empId == employee.EmployeeID)
                                {
                                    employees.Add(employee);
                                }
                            }
                        }
                        return employees;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    //Log
                }
            }
            return null;
        }

        private IEnumerable<EmployeeDetailsTable> GetNonProjectEmployees(IEnumerable<EmployeeDetailsTable> projectEmployees)
        {
            List<EmployeeDetailsTable> employeeDetailsTable = db.EmployeeDetailsTables.ToList();
            if (projectEmployees != null)
            {
                IEnumerable<EmployeeDetailsTable> nonProjectEmployees = employeeDetailsTable.Except(projectEmployees);
                return nonProjectEmployees;
            }
            else
            {
                return employeeDetailsTable;
            }
        }
        
        public ActionResult MapProject(int? id, int? pId)
        {
            if (id == null || pId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string commandText = "INSERT INTO EmployeeProjectMapping(EmployeeID, ProjectID) VALUES(@empId, @proId)";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@empId", SqlDbType.Int);
                command.Parameters["@empId"].Value = id;
                command.Parameters.Add("@proId", SqlDbType.Int);
                command.Parameters["@proId"].Value = pId;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //Log
                }
            }
            return RedirectToAction("Mapping", "Project", new { id = pId });
        }
        
        public ActionResult UnMapProject(int? id, int? pId)
        {
            if (id == null || pId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string commandText = "DELETE FROM EmployeeProjectMapping WHERE EmployeeID = @empId AND ProjectID = @proId";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@empId", SqlDbType.Int);
                command.Parameters["@empId"].Value = id;
                command.Parameters.Add("@proId", SqlDbType.Int);
                command.Parameters["@proId"].Value = pId;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //Log
                }
            }
            return RedirectToAction("Mapping", "Project", new { id = pId });
        }

        // GET: Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectDetailsTable projectDetailsTable = db.ProjectDetailsTables.Find(id);
            if (projectDetailsTable == null)
            {
                return HttpNotFound();
            }
            return View(projectDetailsTable);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,Name")] ProjectDetailsTable projectDetailsTable)
        {
            if (ModelState.IsValid)
            {
                db.ProjectDetailsTables.Add(projectDetailsTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectDetailsTable);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectDetailsTable projectDetailsTable = db.ProjectDetailsTables.Find(id);
            if (projectDetailsTable == null)
            {
                return HttpNotFound();
            }
            return View(projectDetailsTable);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,Name")] ProjectDetailsTable projectDetailsTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectDetailsTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectDetailsTable);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectDetailsTable projectDetailsTable = db.ProjectDetailsTables.Find(id);
            if (projectDetailsTable == null)
            {
                return HttpNotFound();
            }
            return View(projectDetailsTable);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectDetailsTable projectDetailsTable = db.ProjectDetailsTables.Find(id);
            db.ProjectDetailsTables.Remove(projectDetailsTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
