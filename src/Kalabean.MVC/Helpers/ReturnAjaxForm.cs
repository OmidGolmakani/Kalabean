using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.Helpers
{
    public class ReturnAjaxForm
    {
        public string html { get; set; }
        public ResultType ResultType { get; set; }
        public string Message { get; set; }

        public object ResultData { get; set; }
    }
    public enum ResultType
    {
        Success,
        Failure,
        Update,
        Redirect,
    }
}
