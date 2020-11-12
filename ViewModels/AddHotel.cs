using System.ComponentModel.DataAnnotations;
using TourOperator.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TourOperator.ViewModels
{
    public class AddHotel
    {
        public Hotel Hotel { get; set; }
        public IEnumerable<SelectListItem> Towns { get; set; }
    }
}