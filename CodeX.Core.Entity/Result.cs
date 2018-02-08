using System;
using System.Collections.Generic;
using System.Text;

namespace CodeX.Core.Entity
{
    public class Result
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
