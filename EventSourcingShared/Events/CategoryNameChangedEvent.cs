﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingShared.Events
{
    public class CategoryNameChangedEvent:IEvent
    {
        public Guid Id { get; set; }
        public string ChangedName { get; set; }
    }
}
