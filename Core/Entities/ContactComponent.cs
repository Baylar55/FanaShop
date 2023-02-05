﻿using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ContactComponent:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconCode { get; set; }
    }
}
