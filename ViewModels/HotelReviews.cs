using System.ComponentModel.DataAnnotations;
using TourOperator.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TourOperator.ViewModels
{
    public class HotelReviews
    {
        public Hotel Hotel { get; set; }
        public Review Review { get; set; }
    }
}