using Demo.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        GetData nodeDB = new GetData();
        private readonly DemoEntities _context = new DemoEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            var nodes = _context.tblNodes.ToList();
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(tblNode node)
        {
            node.startDate = DateTime.Now;
            node.isActive = true;
            _context.tblNodes.Add(node);
            _context.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var Employee = _context.tblNodes.Find(ID);
            return Json(Employee, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(tblNode node)
        {
            _context.Entry(node).State = EntityState.Modified;
            _context.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            var node = _context.tblNodes.SingleOrDefault(x => x.nodeId == ID);
            _context.tblNodes.Remove(node);
            _context.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult gettreelist()
        {
            var oficio = new SqlParameter
            {
                ParameterName = "pOficio",
                Value = "0001"
            };
            var datas = _context.Database.SqlQuery<tblNode>("exec getAllNode @pOficio", oficio).ToList();
            var data = GetData.BuildTree(datas);
            
            var json = JsonConvert.SerializeObject(data);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        
        public class TreeNode
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<TreeNode> Children { get; set; }

            public TreeNode()
            {
                Children = new List<TreeNode>();
            }
        }

        public ActionResult About()
        {
            var oficio = new SqlParameter
            {
                ParameterName = "pOficio",
                Value = "0001"
            };
            var datas= _context.Database.SqlQuery<tblNode>("exec getAllNode @pOficio", oficio).ToList();
            var data = GetData.BuildTree(datas);
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}