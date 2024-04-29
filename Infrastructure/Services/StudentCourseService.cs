using AutoMapper;
using Domain.DTOs.StudentCourseDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrastructure.Services;

public class StudentCourseService : IStudentCourseService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public StudentCourseService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Response<string>> AddStudentCourseAsync(AddStudentCourseDto studentCourse)
    {
        try
        {
            var mapped = mapper.Map<StudentCourse>(studentCourse);

            await context.StudentCourses.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new StudentCourse");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteStudentCourseAsync(int id)
    {
        try
        {
            var studentCourses = await context.StudentCourses.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (studentCourses == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "StudentCourses not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetStudentCourseDto>> GetStudentCourseByIdAsync(int id)
    {
        try
        {
            var studentCourses = await context.StudentCourses.FirstOrDefaultAsync(x => x.Id == id);
            if (studentCourses == null)
                return new Response<GetStudentCourseDto>(HttpStatusCode.BadRequest, "StudentCourse not found");
            var mapped = mapper.Map<GetStudentCourseDto>(studentCourses);
            return new Response<GetStudentCourseDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetStudentCourseDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetStudentCourseDto>>> GetStudentCoursesAsync()
    {
        try
        {
            var studentCourses = context.StudentCourses.AsQueryable();

            var response = context.StudentCourses.Select(x => x);
            var totalRecord = studentCourses.Count();

            var mapped = mapper.Map<List<GetStudentCourseDto>>(response);
            return new PagedResponse<List<GetStudentCourseDto>>(mapped, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetStudentCourseDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetStudentCourseDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpadateStudentCourseAsync(UpdateStudentCourseDto studentCourse)
    {
        try
        {
            var mapped = mapper.Map<StudentCourse>(studentCourse);
            context.StudentCourses.Update(mapped);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "StudentCourses not found");
            return new Response<string>("StudentCourses updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
