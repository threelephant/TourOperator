using System.ComponentModel.DataAnnotations;
using TourOperator.Models;

namespace TourOperator.ViewModels
{
    public class AddHotel
    {
        public Hotel Hotel { get; set; }
        public Town Town { get; set; }
    }
}