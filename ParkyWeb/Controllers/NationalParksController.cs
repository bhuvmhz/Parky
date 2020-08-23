using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;

namespace ParkyWeb.Controllers
{
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _npRepo;

        public NationalParksController(INationalParkRepository npRepo)
        {
            _npRepo = npRepo;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var obj = await _npRepo.GetAllAsync(SD.NationalParkAPIPath);
            return View(obj);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark obj = new NationalPark();

            //for insert/ create
            if (id == null)
            {
                return View(obj);
            }

            //for updates
            obj = await _npRepo.GetAsync(SD.NationalParkAPIPath, id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark nationalPark)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    nationalPark.Picture = p1;
                }
                else
                {
                    var objFromDb = await _npRepo.GetAsync(SD.NationalParkAPIPath, nationalPark.Id);
                    nationalPark.Picture = objFromDb.Picture;
                }

                if (nationalPark.Id == 0)
                {
                    await _npRepo.CreateAsync(SD.NationalParkAPIPath, nationalPark);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _npRepo.UpdateAsync(SD.NationalParkAPIPath + nationalPark.Id, nationalPark);
                }
            }

            return View(nationalPark);
        }
    }
}
