﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        UserBLL bll = new UserBLL();
        // GET: Admin/Login
        public ActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }
        [HttpPost]
        public ActionResult Index(UserDTO model)
        {
            if (model.Username != null && model.Password != null)
            {
                UserDTO user = new UserDTO();
                user = bll.GetUserWithUsernameAndPassword(model);
                if (user != null && user.ID != 0)
                {
                    UserStatic.UserID = user.ID;
                    UserStatic.Imagepath = user.Imagepath;
                    UserStatic.Namesurname = user.Name;
                    UserStatic.isAdmin = user.isAdmin;
                    LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 12);
                    return RedirectToAction("PostList", "Post");
                }
            }
            return View(model);
        }
    }
}