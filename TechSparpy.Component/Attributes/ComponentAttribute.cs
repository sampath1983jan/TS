﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Component.Attributes
{
public class ComponentAttribute
    {
        public string Name;
        public string Type;
        public bool IsKey;
        public bool IsUnique;
        public bool IsRequired;
        public bool IsCore;
        public bool IsEncripted;
        public string ComponentKey;
        public string Value;
        public string DefaultValue;
        public int DisplayOrder;
        public bool IsContentLimit;
        public string Min;
        public string Max;
        public int MaxLength;
        public string DisplayName;
        public bool IsAutoGenerated;       
    }
}