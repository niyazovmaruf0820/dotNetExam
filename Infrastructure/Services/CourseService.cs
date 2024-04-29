using AutoMapper;
using Domain.DTOs.CourseDto;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;

namespace Infrastructure.Services;

public class CourseService : ICourseService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public CourseService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<Response<string>> AddCourseAsync(AddCourseDto course)
    {
        try
        {
            var existingCourse = await context.Courses.FirstOrDefaultAsync(x => x.Title == course.Title);
            if (existingCourse != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Course already exists");
            var mapped = mapper.Map<Course>(course);

            await context.Courses.AddAsync(mapped);
            await context.SaveChangesAsync();

            return new Response<string>("Successfully created a new course");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCourseAsync(int id)
    {
        try
        {
            var courses = await context.Courses.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (courses == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Courses not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCourseDto>> GetCourseByIdAsync(int id)
    {
        try
        {
            var courses = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (courses == null)
                return new Response<GetCourseDto>(HttpStatusCode.BadRequest, "Course not found");
            var mapped = mapper.Map<GetCourseDto>(courses);
            return new Response<GetCourseDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetCourseDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetCourseDto>>> GetCoursesAsync(CourseFilter filter)
    {
        try
        {
            var courses = context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                courses = courses.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower()));
            if (!string.IsNullOrEmpty(filter.Instructor))
                courses = courses.Where(x => x.Instructor!.ToLower().Contains(filter.Instructor.ToLower()));

            var response = await courses
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = courses.Count();

            var mapped = mapper.Map<List<GetCourseDto>>(response);
            return new PagedResponse<List<GetCourseDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetCourseDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpadateCourseAsync(UpdateCourseDto course)
    {
        try
        {
            var mapped = mapper.Map<Course>(course);
            context.Courses.Update(mapped);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(HttpStatusCode.BadRequest, "Courses not found");
            return new Response<string>("Courses updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
