using Newtonsoft.Json;
using PagedList;
using ShardaMotorsWeb_App.App_Data.BAL;
using ShardaMotorsWeb_App.App_Data.BEL;
using ShardaMotorsWeb_App.Models;
using ShardaMotorsWebAPI.Controllers;
using ShardaMotorsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace ShardaMotorsWeb_App.Controllers
{
    public class SMHomeController : Controller
    {
        public ActionResult Index(int? page)//sp kaha se call kiya hai

        {
            try
            {

                BAL_ShardaMotorsDetails _BalSMController = new BAL_ShardaMotorsDetails();
                _BalSMController._LstShardaMotorsDtls = _BalSMController.SelectSMDetails().ToList();
                foreach (var items in _BalSMController._LstShardaMotorsDtls)
                {
                    _BalSMController.Updated_DateTime = items.Updated_DateTime;
                    break;
                }
                Session["SMDataFromDate"] = _BalSMController.Updated_DateTime;
                int pageSize = 100;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<BAL_ShardaMotorsDetails> _lstPage = null;
                _lstPage = _BalSMController._LstShardaMotorsDtls.ToList().ToPagedList(pageIndex, pageSize);
                ViewBag.SMDataList = _lstPage;
                ViewData["pageIndex"] = pageIndex;
                //return View(_BalSMController._LstShardaMotorsDtls);
                return View();
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        public JsonResult AllShardaMotors_Details(string _SelectedFromDate = "", string _SelectedToDate = "", int pageNumber = 1, int Size = 100)
        {
            //if (Session["FrontUserId"] != null)
            //{
            try
            {
                 pageNumber = Convert.ToInt32(Request.Form.GetValues("start").FirstOrDefault());
                int length = Convert.ToInt32(Request.Form.GetValues("length").FirstOrDefault());
                pageNumber = (pageNumber / length);
                int _fromId = (int)Session["FrontUserId"];
                BAL_ShardaMotorsDetails _BalSMController = new BAL_ShardaMotorsDetails();
                string lastupdatedDate = Session["SMDataFromDate"].ToString();
                DateTime dateTimeUpdated = DateTime.Parse(lastupdatedDate);

                if (_SelectedFromDate == "" && _SelectedToDate == "")
                {
                    var resultData = _BalSMController.SelectSMDetailsDateWise(dateTimeUpdated);
                    var datalst = from s in resultData.bal_ShardaMotorsDetails.ToList() select new { s.RowNumber, s.Updated_DateTime, s.Time, s.Part_Name, s.DPF, s.LNT, s.Serial, s.DPF_Cat, s.LNT_Cat, s.DPF_Weight, s.LNT_Weight, s.DPF_BW, s.LNT_BW, s.DPF_Out_dia, s.LNT_Out_dia, s.DPF_Sizing_Data, s.LNT_Sizing_Data, s.DPF_Target_GBD, s.LNT_Target_GBD, s.DPF_Gap, s.LNT_Gap, s.Machine_ID};
                    return Json(new { draw = datalst, recordsFiltered = resultData.TotalRecord, recordsTotal = resultData.TotalRecord, data = datalst }, JsonRequestBehavior.AllowGet);
                    //return Json(datalst, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    DateTime dateTime123 = DateTime.Parse(_SelectedFromDate);
                    DateTime dateTime124 = DateTime.Parse(_SelectedToDate);//page number abhi bhi 1 hi aa raha hai check kar le baki ka ho gaya
                    var BalData = _BalSMController.SelectSMDetailsPageWise(pageNumber, Size, dateTime123, dateTime124);
                    var datalst = from s in BalData.bal_ShardaMotorsDetails select new { s.RowNumber, s.Updated_DateTime, s.Time, s.Part_Name, s.DPF, s.LNT, s.Serial, s.DPF_Cat, s.LNT_Cat, s.DPF_Weight, s.LNT_Weight, s.DPF_BW, s.LNT_BW, s.DPF_Out_dia, s.LNT_Out_dia, s.DPF_Sizing_Data, s.LNT_Sizing_Data, s.DPF_Target_GBD, s.LNT_Target_GBD, s.DPF_Gap, s.LNT_Gap, s.Machine_ID };
                    return Json(new { draw = datalst, recordsFiltered = BalData.TotalRecord, recordsTotal = BalData.TotalRecord, data = datalst }, JsonRequestBehavior.AllowGet);
                    //return Json(datalst, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                //throw ex;

                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AllShardaMotors_DateWiseDetails(string _SelectedFromDate, string _SelectedToDate, string page)
        {
            int startRecord = 0;
            int endRecord = 0;
            try
            {
                int _fromId = (int)Session["FrontUserId"];

                BAL_ShardaMotorsDetails _BalSMController = new BAL_ShardaMotorsDetails();
                DateTime date1 = DateTime.Parse(_SelectedFromDate);
                DateTime date2 = DateTime.Parse(_SelectedToDate);
                _BalSMController._LstShardaMotorsDtls = _BalSMController.SelectSMDetails().Where(s => s.Updated_DateTime >= DateTime.Parse(_SelectedFromDate) && s.Updated_DateTime <= DateTime.Parse(_SelectedToDate)).ToList();//.Where(m => m.Updated_DateTime == _SelectedDate)

                //where (s.Updated_DateTime >= DateTime.Parse(_SelectedFromDate) && s.Updated_DateTime <= DateTime.Parse(_SelectedToDate))

                var datalst = from s in _BalSMController.SelectSMDetails().ToList()
                              select new { s.Id, s.Updated_DateTime, s.Time, s.Part_Name, s.DPF, s.LNT, s.Serial, s.DPF_Cat, s.LNT_Cat, s.DPF_Weight, s.LNT_Weight, s.DPF_BW, s.LNT_BW, s.DPF_Out_dia, s.LNT_Out_dia, s.DPF_Sizing_Data, s.LNT_Sizing_Data, s.DPF_Target_GBD, s.LNT_Target_GBD, s.DPF_Gap, s.LNT_Gap, s.Machine_ID };
                //return Json(new { data = datalst }, JsonRequestBehavior.AllowGet);whare condition sql me laga ok  ek new sp me kar ye wala sp alredy bht jagah pe use hai na?ha ok aise rehne dde ...condition sp me dal 
                int pageSize = 100;
                int pageIndex = 1;
                int? pagee = Convert.ToInt32(page);
                pageIndex = pagee.HasValue ? Convert.ToInt32(pagee) : 1;
                IPagedList<BAL_ShardaMotorsDetails> _lstPage = null;
                _lstPage = _BalSMController._LstShardaMotorsDtls.ToList().Take(1150).ToPagedList(pageIndex, pageSize);
                ViewBag.SMDataList = _lstPage;
                var jsonResult = Json(datalst.ToList().ToPagedList(pageIndex, pageSize), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                //throw ex;
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Login()
        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users _user)
        {
            BAL_Users _balUser = new BAL_Users();
            try
            {
                _balUser._lstLoginUser = _balUser.SelectUserById(1);
                string _password = "";
                string _UserName = "";
                int _id = 0;
                foreach (var item in _balUser._lstLoginUser)
                {
                    _password = item.Password;
                    _UserName = item.UserName;
                    _id = item.Id;
                }
                if (_user.UserName == _UserName && _user.Password == _password)
                {

                    Session["FrontUserId"] = _id;
                }
                else
                {
                    TempData["LoginMessage"] = "Please Enter Valid Login ID And Password.";
                    return View();
                }
                TempData["LoginMessage"] = "Login Successfully.";
                return RedirectToAction("Index", "SMHome");
            }
            catch (Exception ex)
            {
                TempData["LoginMessage"] = "Something Went To Wrong.";
                return View();
            }

        }
        public ActionResult UserLogout()
        {
            try
            {
                Session["FrontUserId"] = null;
                Session.RemoveAll();
            }
            catch (Exception)
            {

            }
            TempData["UserLogout"] = "User Logout Successfully.";
            return RedirectToAction("Index", "SMHome");
        }

        public ActionResult ChangePassword()
        {
            try
            {
                BAL_Users _balUser = new BAL_Users();
                int UserId = Convert.ToInt32(Session["FrontUserId"]);
                _balUser._lstLoginUser = _balUser.SelectUserById(UserId);
                foreach (var items in _balUser._lstLoginUser)
                {
                    TempData["OldPassword"] = items.Password;
                }
                return View();
            }
            catch (Exception)
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(Users _user)
        {
            try
            {
                BAL_Users _balUser = new BAL_Users();
                BEL_Users _bElUser = new BEL_Users();
                _bElUser.Password = _user.Password;
                int UserId = Convert.ToInt32(Session["FrontUserId"]);
                _bElUser.Id = UserId;
                bool IsUpdate = _balUser.ChangePassword(_bElUser);
            }
            catch (Exception)
            {

            }
            TempData["ChangePassword"] = "Password changed Successfully.";
            return RedirectToAction("Index", "SMHome");
        }
    }
}
