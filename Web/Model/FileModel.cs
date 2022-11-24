using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class FileModel
    {
        [Display(Name = "File")]
        public IFormFile File;
    }
}
