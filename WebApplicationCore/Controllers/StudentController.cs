using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.IRepository;
using WebApplicationCore.Models;

namespace WebApplicationCore.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo _repo;
        public StudentController(IStudentRepo repo)
        {
            _repo = repo;

        }
        public IActionResult Index()
        {
            return View(_repo.GetAllStudents());
        }
        public IActionResult Details(int id)
        {
            return View(_repo.GetStudentById(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Student student)
        {
            _repo.CreateStudent(student);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Student student = _repo.GetStudentById(id);
            return View(student);
        }
        [HttpPost]

        public IActionResult Delete(int id, Student student)
        {
            _repo.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Student student = _repo.GetStudentById(id);
            return View(student);
        }


        [HttpPost]

        public IActionResult Edit(int id, Student student)
        {
            _repo.EditStudent(id, student);
            return RedirectToAction("Index");
        }


    }
}
