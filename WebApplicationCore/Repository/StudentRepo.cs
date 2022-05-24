using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Context;
using WebApplicationCore.IRepository;
using WebApplicationCore.Models;

namespace WebApplicationCore.Repository
{
   
    public class StudentRepo : IStudentRepo
    {
        private readonly AppDbContext _context;
        public StudentRepo(AppDbContext context)
        {
            _context = context;

        }
        public void CreateStudent(Student student)
        {
            _context.Students.Add(student);
            SaveData();
        }
        void SaveData()
        {
            _context.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            if(student!=null)
            {
                _context.Students.Remove(student);
               
            }
            SaveData();
        }

        public void EditStudent(int id, Student student)
        {
            var obj = GetStudentById(id);
            if (obj != null)
            {
                foreach (var temp in _context.Students)
                {
                    if (temp.Id == id)
                    {
                        temp.Batch = student.Batch;
                        temp.Marks = student.Marks;
                        break;
                    }
                   

                }

                SaveData();
            }
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();

        }

        public Student GetStudentById(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            return student;
        }
    }
}
