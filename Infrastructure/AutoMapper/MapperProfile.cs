using AutoMapper;
using Domain.DTOs.CourseDto;
// using Domain.DTOs.GroupDto;
// using Domain.DTOs.MentorDto;
// using Domain.DTOs.MentorGroupDto;
// using Domain.DTOs.ProgressBookDto;
using Domain.DTOs.StudentDto;
// using Domain.DTOs.TimeTableDto;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Student, AddStudentDto>().ReverseMap();
        CreateMap<Student, GetStudentDto>().ReverseMap();
        CreateMap<Student, UpdateStudentDto>().ReverseMap();
        CreateMap<Course, AddCourseDto>().ReverseMap();
        CreateMap<Course, GetCourseDto>().ReverseMap();
        CreateMap<Course, UpdateCourseDto>().ReverseMap();
        // CreateMap<Group, AddGroupDto>().ReverseMap();
        // CreateMap<Group, GetGroupDto>().ReverseMap();
        // CreateMap<Group, UpdateGroupDto>().ReverseMap();
        // CreateMap<Mentor, AddMentorDto>().ReverseMap();
        // CreateMap<Mentor, GetMentorsDto>().ReverseMap();
        // CreateMap<Mentor, UpdateMentorsDto>().ReverseMap();
        // CreateMap<MentorGroup, AddMentorGroupDto>().ReverseMap();
        // CreateMap<MentorGroup, GetMentorGroupDto>().ReverseMap();
        // CreateMap<MentorGroup, UpdateMentorGroupDto>().ReverseMap();
        // CreateMap<ProgressBook, AddMentorGroupDto>().ReverseMap();
        // CreateMap<ProgressBook, GetProgressbookDto>().ReverseMap();
        // CreateMap<ProgressBook, UpdateProgressBookDto>().ReverseMap();
        // CreateMap<AddTimetableDto, TimeTable>().ForMember(en => en.FromTime,dto => dto.MapFrom(s => TimeSpan.Parse(s.FromTime)) )
        //                                         .ForMember(x => x.ToTime,opt => opt.MapFrom(x => TimeSpan.Parse(x.ToTime)));


    
        // CreateMap<TimeTable, GetTimeTableDto>().ReverseMap();
        // CreateMap<UpdateTimeTableDto, TimeTable>().ForMember(en => en.FromTime,dto => dto.MapFrom(s => TimeSpan.Parse(s.FromTime)) )
        //                                         .ForMember(x => x.ToTime,opt => opt.MapFrom(x => TimeSpan.Parse(x.ToTime)));
        
        
        // //ForMembers
        // CreateMap< Student,GetStudentDto>()
        //     .ForMember(sDto => sDto.FulName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"))
        //     .ForMember(sDto => sDto.EmailAddress, opt => opt.MapFrom(s =>s.Email));
        //
        // //Reverse map
        // CreateMap<BaseStudentDto,Student>().ReverseMap();
        //
        // // ignore
        // CreateMap<Student, AddStudentDto>()
        //     .ForMember(dest => dest.FirstName, opt => opt.Ignore());
    }   
}