using Dsgn.APIs.Common;
using Dsgn.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dsgn.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CourseFindManyArgs : FindManyInput<Course, CourseWhereInput> { }
