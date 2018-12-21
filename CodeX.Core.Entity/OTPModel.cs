using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeX.Core.Entity
{
    public class OTPModel
    {
        [Required]
        [Display(Name = "MobileNumber")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "OTP")]
        public string OTP { get; set; }
    }
}
