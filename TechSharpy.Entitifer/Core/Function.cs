using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSharpy.Entitifier.Core
{
    public class Function
    {
        public string Name;
        public int FunctionID;
        public List<Param> Inputparam;
        public OutputParam Outputparam;
        public List<string> Steps;
        public Data.Function dataFunction;
        public Function() {

        }
        public Function(string name, int functionID, List<Param> inputparam, OutputParam outputparam, List<string> steps)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FunctionID = functionID;
            Inputparam = inputparam ?? throw new ArgumentNullException(nameof(inputparam));
            Outputparam = outputparam ?? throw new ArgumentNullException(nameof(outputparam));
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
            dataFunction=new Data.Function();
        }

        public Function(int functionID)
        {
            FunctionID = functionID;
            Inputparam = new List<Param>();
            Outputparam = new OutputParam();
            Steps = new List<string>();
            dataFunction = new Data.Function();
            
        }
        public  List<Function> GetFunctionList() {
            DataTable dt = new DataTable();
            dataFunction.getFunction();
           return dt.AsEnumerable().Select(g => new Function
            {
                FunctionID = g.IsNull("FunctionID") ? 0 : g.Field<int>("FunctionID"),
                Name = g.IsNull("Name") ? "" : g.Field<string>("Name"),
                Inputparam = g.IsNull("Inputparam") ? new List<Param>()  : Newtonsoft.Json.JsonConvert.DeserializeObject<List<Param>>(g.Field<string>("Inputparam")),
                Steps = g.IsNull("Steps") ? new List<string>() : g.Field<string>("Steps").Split(',').ToList(),
                Outputparam = g.IsNull("Outputparam") ? new OutputParam() : Newtonsoft.Json.JsonConvert.DeserializeObject<OutputParam>("Outputparam")               
            }).ToList();
        }
        internal protected bool Save() {
            string ip = Newtonsoft.Json.JsonConvert.SerializeObject(Inputparam);
            string op = Newtonsoft.Json.JsonConvert.SerializeObject(Outputparam);
            if (FunctionID <= 0)
            {             
                if (dataFunction.Save(Name, ip, op, string.Join(",", Steps.ToArray())) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {               
                if (dataFunction.Save(FunctionID,Name, ip, op, string.Join(",", Steps.ToArray())))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
        public bool Delete() {
            if (dataFunction.Delete(FunctionID))
            {
                return true;
            }
            else {
                return false;
            }
        }

    }
    public class Param 
    {
        public string Value;
        public string Key;
        public EntityFieldType FieldType;
        public Param(string value, string key, EntityFieldType fieldType)
        {
            this.Value = value;
            Key = key;
            this.FieldType = fieldType;
        }
        public Param() {

        }
    }
    public class OutputParam:Param
    {
        public object Result;

        public OutputParam() {
            Result = new object();
        }
        public OutputParam(string value, string key, EntityFieldType fieldType) {
            this.Value = value;
            Key = key;
            this.FieldType = fieldType;
        }
    }
}
