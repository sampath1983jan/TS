﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


 

    public static class ObjectExtension
    {

    public static Object CopyTo<T>(this object objSource)

    {

        var serializedParent = Newtonsoft.Json.JsonConvert.SerializeObject(objSource);
        object ms = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedParent);
        return ms;
    }
    }

 
