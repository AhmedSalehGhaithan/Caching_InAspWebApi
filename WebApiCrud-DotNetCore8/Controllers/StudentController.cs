using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCrud_DotNetCore8.Data;
using WebApiCrud_DotNetCore8.Models.Entities;
using WebApiCrud_DotNetCore8.Services;

namespace WebApiCrud_DotNetCore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IMyCacheService _cacheService, ApplicationDbContext _applicationDbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            
            var cacheDriver = _cacheService.GetData<IEnumerable<Student>>("Driver");
            if(cacheDriver != null && cacheDriver.Count() > 0)
            {
                return Ok(cacheDriver);
            }
            await Task.Delay(5000);
            var students = await _applicationDbContext.Students.AsNoTracking().ToListAsync();

            var expireTime = DateTimeOffset.Now.AddHours(2);

            _cacheService.SetData<IEnumerable<Student>>("Driver", students, expireTime);
            
            return Ok(students);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudent(Guid id) {
            var student = await _applicationDbContext.Students.FindAsync(id);
            if (student == null) {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentDTO addStudentDTO) {
            var student = new Student() {
                StudentName = addStudentDTO.StudentName,
                StudentEmail = addStudentDTO.StudentEmail,
                StudentAge = addStudentDTO.StudentAge,
                StudentDepartment = addStudentDTO.StudentDepartment,
            };
            _applicationDbContext.Students.Add(student);
            await _applicationDbContext.SaveChangesAsync();

            // if you add to the database reset the cache 
            _cacheService.RemoveData("Driver");
            return Ok(student);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateStudent(Guid id, StudentDTO updateStudentDTO) {
            var student = _applicationDbContext.Students.Find(id);
            if (student == null) {
                return NotFound();
            }

            student.StudentName = updateStudentDTO.StudentName;
            student.StudentEmail = updateStudentDTO.StudentEmail;
            student.StudentAge = updateStudentDTO.StudentAge;
            student.StudentDepartment = updateStudentDTO.StudentDepartment;
            _applicationDbContext.SaveChanges();
            // if you update to the database reset the cache 
            _cacheService.RemoveData("Driver");
            return Ok(student);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteStudent(Guid id) {
            var student = _applicationDbContext.Students.Find(id);
            if (student == null) {
                return NotFound();
            }
            _applicationDbContext.Students.Remove(student);
            _applicationDbContext.SaveChanges();
            // if you delete to the database reset the cache 
            _cacheService.RemoveData("Driver");
            return Ok(student);
        }
    }
}
