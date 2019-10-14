using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseTitleMustBeDifferentFromDescription(
       ErrorMessage = "Title must to be different from the description")]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a title.")]
        [MaxLength(100, ErrorMessage = "Title cannot have more than 100 characters")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot have more than 500 characters.")]
        public virtual string Description { get; set; }
    }
}
