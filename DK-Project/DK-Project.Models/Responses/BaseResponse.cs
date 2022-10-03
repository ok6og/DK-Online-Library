﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DK_Project.Models.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; init; }
        public string Message { get; set; }
    }
}
