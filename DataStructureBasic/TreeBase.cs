﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureBasic
{
    public class TreeBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LValue { get; set; }
        public int RValue { get; set; }
        public int? ParentId { get; set; }
        public int Depth { get; set; }
        public string ObjId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

    }



}
