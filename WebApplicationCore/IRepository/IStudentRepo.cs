using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Models;

namespace WebApplicationCore.IRepository
{
    public interface IStudentRepo
    {
        List<Student> GetAllStudents( );
        Student GetStudentById(int id);
        void CreateStudent(Student student);
        void EditStudent(int id, Student student);
        void DeleteStudent(int id);
    }
}
