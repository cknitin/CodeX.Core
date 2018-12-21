using System;
using System.Collections.Generic;
using System.Text;

namespace CodeX.Core.Entity
{
    /// <summary>
    /// This class is used for managing user OTP for Get and Verify
    /// </summary>
    public class UserOTP
    {
        public long UserOTPId { get; set; }
        public string MobileNumber { get; set; }
        public string OTP { get; set; }
        public DateTime CreatedDate { get; set; }
        public  string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
