﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IApplicantPostRepository
    {
        void CreateApplicantPost(ApplicantPost p);
    }
}
