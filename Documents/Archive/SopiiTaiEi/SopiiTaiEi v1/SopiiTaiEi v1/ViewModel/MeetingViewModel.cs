using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SopiiTaiEi1.ViewModel
{
    public class MeetingViewModel
    {
        [DisplayName("Starting Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Starting Date is required ")]
        //[Range(typeof(DateTime), DateTime.Now.ToString(), "3/4/2004", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime StartingDate { get; set; } = DateTime.Now.Date;

        [DisplayName("Ending Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ending Date is required ")]
        public DateTime EndingDate { get; set; } = DateTime.Now.Date;

        public string Title { get; set; }

        //public MeetingViewModel()
        //{
        //    StartingDate = DateTime.Now;
        //    EndingDate = DateTime.Now;
        //}
        public bool StartingDateValidation(DateTime startingDate)
        {
            if (startingDate < DateTime.Now.Date)
            {
                return false;
            }
            return true;
        }
    }
}
