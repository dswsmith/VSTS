﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshDeskReporting.Entities
{
    class Groups : List<Group>
    {
        public bool Save()
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Save();
            }
            return true;
        }
    }
}
