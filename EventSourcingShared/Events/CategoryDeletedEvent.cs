﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingShared.Events
{
    public class CategoryDeletedEvent:IEvent
    {
        public Guid Id { get; set; }
    }
}
