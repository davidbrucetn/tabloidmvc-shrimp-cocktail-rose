﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }
        // GET: UserProfileController
        public ActionResult Index()
        {
            var users = _userProfileRepository.GetAll();
            return View(users);
        }

        public ActionResult DeactivatedIndex()
        {
            var deactivatedUsers = _userProfileRepository.GetDeactivated();
            return View(deactivatedUsers);
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            var user = _userProfileRepository.GetById(id);
            return View(user);
        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Reactivate/5
        public ActionResult Reactivate(int id)
        {
            UserProfile user = _userProfileRepository.GetById(id);
            return View(user);
        }

        // POST: UserProfileController/Reactivate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(int id, UserProfile user)
        {
            try
            {
                _userProfileRepository.ReactivateUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(user);
            }
        }

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            UserProfile user = _userProfileRepository.GetById(id);
            return View(user);
        }

        // POST Soft delete, moves User to a "Deactivated" group
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserProfile user)
        {
            try
            {
                _userProfileRepository.DeleteUser(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(user);
            }
        }
    }
}
