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
    public class AdminController : Controller
    {
        private ITCompanyDBEntities db = new ITCompanyDBEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.EmployeeDetailsTables.ToList());
        }

        public ActionResult Technologies(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetailsTable employeeDetailsTable = db.EmployeeDetailsTables.Find(id);
            if (employeeDetailsTable == null)
            {
                return HttpNotFound();
            }
            TechnologyManagementModel technologyManagementModel = new TechnologyManagementModel();
            technologyManagementModel.employee = employeeDetailsTable;
            technologyManagementModel.techKnown = GetKnownSkills(id);
            technologyManagementModel.techUnKnown = GetUnKnownSkills(technologyManagementModel.techKnown);
            return View(technologyManagementModel);
        }

        private IEnumerable<TechnologyDetailsTable> GetKnownSkills(int? id)
        {
            string commandText = "SELECT * FROM EmployeeTechnologyMapping WHERE EmployeeID = @ID";
            SqlDataReader dr;
            List<int> ls = new List<int>();
            List<EmployeeDetailsTable> employeeDetails = new List<EmployeeDetailsTable>();
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
                            ls.Add(Convert.ToInt32(dr["TechnologyID"]));
                        }
                        List<TechnologyDetailsTable> technologyDetails = db.TechnologyDetailsTables.ToList();
                        List<TechnologyDetailsTable> knownTech = new List<TechnologyDetailsTable>();
                        foreach (TechnologyDetailsTable tech in technologyDetails)
                        {
                            foreach (int known in ls)
                            {
                                if (tech.TechnologyID == known)
                                {
                                    knownTech.Add(tech);
                                }
                            }
                        }
                        return knownTech;
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

        private IEnumerable<TechnologyDetailsTable> GetUnKnownSkills(IEnumerable<TechnologyDetailsTable> techKnown)
        {
            List<TechnologyDetailsTable> technologies = db.TechnologyDetailsTables.ToList();
            if (techKnown != null)
            {
                IEnumerable<TechnologyDetailsTable> techUnKnown = technologies.Except(techKnown);
                return techUnKnown;
            }
            else
            {
                return technologies;
            }
        }

        public ActionResult AddSkill(int? id, int? techId)
        {
            if (id == null || techId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string commandText = "INSERT INTO EmployeeTechnologyMapping(EmployeeID, TechnologyID) VALUES(@empId, @techId)";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@empId", SqlDbType.Int);
                command.Parameters["@empId"].Value = id;
                command.Parameters.Add("@techId", SqlDbType.Int);
                command.Parameters["@techId"].Value = techId;
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
            return RedirectToAction("Technologies", "Admin", new { @id = id });
        }

        public ActionResult RemoveSkill(int? id, int? techId)
        {
            if (id == null || techId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string commandText = "DELETE FROM EmployeeTechnologyMapping WHERE EmployeeID = @empId AND TechnologyID = @techId";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString()))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@empId", SqlDbType.Int);
                command.Parameters["@empId"].Value = id;
                command.Parameters.Add("@techId", SqlDbType.Int);
                command.Parameters["@techId"].Value = techId;
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
            return RedirectToAction("Technologies", "Admin", new { @id = id });
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetailsTable employeeDetailsTable = db.EmployeeDetailsTables.Find(id);
            if (employeeDetailsTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeDetailsTable);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,Employee_FirstName,Employee_LastName,Employee_Age,Employee_Gender,Employee_Address,Employee_Phone,Employee_State,Employee_Designation")] EmployeeDetailsTable employeeDetailsTable)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeDetailsTables.Add(employeeDetailsTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeDetailsTable);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetailsTable employeeDetailsTable = db.EmployeeDetailsTables.Find(id);
            if (employeeDetailsTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeDetailsTable);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,Employee_FirstName,Employee_LastName,Employee_Age,Employee_Gender,Employee_Address,Employee_Phone,Employee_State,Employee_Designation")] EmployeeDetailsTable employeeDetailsTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeDetailsTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeDetailsTable);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetailsTable employeeDetailsTable = db.EmployeeDetailsTables.Find(id);
            if (employeeDetailsTable == null)
            {
                return HttpNotFound();
            }
            return View(employeeDetailsTable);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeDetailsTable employeeDetailsTable = db.EmployeeDetailsTables.Find(id);
            db.EmployeeDetailsTables.Remove(employeeDetailsTable);
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
