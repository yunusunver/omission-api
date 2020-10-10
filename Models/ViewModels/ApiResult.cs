using System;
using System.Collections.Generic;

namespace omission.api.Models.ViewModels
{

    public class ApiResult<T> { 

        public T Data { get; set; }
        public int Count { get; set; }

    }

}