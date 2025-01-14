using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using studentportal.Data;
using studentportal.Models;
using studentportal.Models.Entities;

namespace studentportal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentsController(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task< IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Gender = viewModel.Gender,
                Phonenumber = viewModel.Phonenumber,
                Subscribed = viewModel.Subscribed,
            };

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var students = await _dbContext.Students.ToListAsync();
            return View(students);


        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid Id)
        {
            var student = await _dbContext.Students.FindAsync(Id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student Viewmodel)
        {
            var student = await _dbContext.Students.FindAsync(Viewmodel.Id);
            if(student is not null)
            {
                student.Name = Viewmodel.Name;
                student.Email = Viewmodel.Email;
                student.Gender = Viewmodel.Gender;
                student.Phonenumber = Viewmodel.Phonenumber;
                student.Subscribed = Viewmodel.Subscribed;

                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");


        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student ViewModel)
        {
            var student = await _dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.Id ==ViewModel.Id);

            if(student is not null)
            {
                 _dbContext.Students.Remove(ViewModel);
                await _dbContext.SaveChangesAsync();


            }
            return RedirectToAction("List", "Students");

        }
    }
}
