using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSharpy.Entitifier;
using TechSharpy.Entitifier.Core;

namespace TechSharpy.Entitifier.Core
{
    class Action
    {            
            public enum ActionType : int
            {
                NONE = 0,
                APPLYFILTER = 1,
                FORMULA = 2,
                SORTING = 3,
                RENAME = 4,
                DELETE = 5,
                DUPLICATE = 6,
                ADDCOLUMN = 7,
                MERGEDATASET = 8,
                JOINDATASET = 9,
                PIVOT = 10,
                FINDREPLACE = 11,
                STRINGCONVERTION = 12,
                ROUNDTOINT = 13,
                CHANGEDATATYPE = 14,
                CONVERTDATETONUMBER = 15,
                // RESHAPE = 16
                SPLIT = 16
            }
            public int ClientID { get; set; }
           // public int UserID { get; set; }
            public int Entitykey { get; set; }
            public int ActionID { get; set; }
            public ActionType Type { get; set; }
            public string ActionName { get; set; }
            public string ActionSchema { get; set; }
            public DateTime ActionCreatedOn { get; set; }
            public bool IsInclude { get; set; }
            public int ActionOrder { get; set; }
            private TechSharpy.Entitifier.Data.Action dAction;
            public Action()
            {
                Type = ActionType.NONE;
                ActionName = "";
                ActionSchema = "";
                ActionCreatedOn = DateTime.Now;
                IsInclude = true;
                ActionID = -1;
                Entitykey = -1;
                //UserID = -1;
                ClientID = -1;
                dAction = new TechSharpy.Entitifier.Data.Action();
            }
            public Action(int pClientID, int pCubeID, int pActionID)
            {
                this.ActionID = pActionID;
                this.Entitykey = pCubeID;
                this.ClientID = pClientID;
                dAction = new TechSharpy.Entitifier.Data.Action();
              //  LoadAction();
            }
            public Action(int pClientID, int pCubeID)
            {
                this.Entitykey = pCubeID;
                this.ClientID = pClientID;
                this.ActionID = -1;
                dAction = new TechSharpy.Entitifier.Data.Action();
            }
            public Action(int pClientID, int pCubeID, string pActionName,
                string pActionSchema, int pActionOrder, Action.ActionType pActionType, bool pIsInclude)
            {
                this.Entitykey = pCubeID;
                this.ClientID = pClientID;
                this.ActionID = -1;
                this.ActionName = pActionName;
                this.ActionSchema = pActionSchema;
                this.ActionOrder = pActionOrder;
                this.Type = pActionType;
                this.IsInclude = pIsInclude;
                dAction = new TechSharpy.Entitifier.Data.Action();
            }

        public bool SaveAction()
        {
     
            dAction = new TechSharpy.Entitifier.Data.Action();
           // SMRHRT.Services.Collections.ErrorCollection.ErrorInfo err = new SMRHRT.Services.Collections.ErrorCollection.ErrorInfo();
            try
            {
                if (this.ActionID > 0)
                {
                    //if (dAction.UpdateAction(this.Entitykey, this.ActionID, this.ActionSchema, this.IsInclude))
                    //{
                    //    //err = new SMRHRT.Services.Collections.ErrorCollection.ErrorInfo("Action Saved", SMRHRT.Services.Collections.ErrorCollection.ErrorInfo.ErrorType.NO_ERROR);
                    //    //audit = new Bix360.Global.Audit(this.ClientID, this.Entitykey, "Action Updated- " + this.ActionName, this.ActionSchema, Global.Audit.ActionCategory.CUBE, Global.Audit.ActionType.UPDATE, this.UserID);
                    //    //audit.Save();
                    //}
                  //  else
                       // err = new SMRHRT.Services.Collections.ErrorCollection.ErrorInfo("Unable to update action", SMRHRT.Services.Collections.ErrorCollection.ErrorInfo.ErrorType.ERR_CRITICAL);
                }
                else
                {
                    this.ActionID = dAction.SaveAction(this.Entitykey, (int)Type, this.IsInclude, this.ActionName, this.ActionSchema);
                    if (this.ActionID > 0)
                    {
                        //err = new SMRHRT.Services.Collections.ErrorCollection.ErrorInfo("Action Saved", SMRHRT.Services.Collections.ErrorCollection.ErrorInfo.ErrorType.NO_ERROR);

                        //audit = new Bix360.Global.Audit(this.ClientID, this.Entitykey, "Action Created-" + this.ActionName, this.ActionSchema, Global.Audit.ActionCategory.CUBE, Global.Audit.ActionType.INSERT, this.UserID);
                        //audit.Save();
                    }
                   // else
                       // err = new SMRHRT.Services.Collections.ErrorCollection.ErrorInfo("Unable to save action", SMRHRT.Services.Collections.ErrorCollection.ErrorInfo.ErrorType.ERR_CRITICAL);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
            //return err;
        }

        //public bool DeleteAction()
        //{
        //    dAction = new TechSharpy.Entitifier.Data.Action();
        //    try
        //    {
        //        if (dAction.DeleteAction(this.ClientID, this.Entitykey, this.ActionID) == true)
        //        {
        //            //Bix360.Global.Audit audit;
        //            //audit = new Bix360.Global.Audit(this.ClientID, this.Entitykey, "Action removed-" + this.ActionName, this.ActionSchema, Global.Audit.ActionCategory.CUBE, Global.Audit.ActionType.UPDATE, this.UserID);
        //            //audit.Save();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return false;
        //}



    }


    public class Rename
    {
        public int CubeID;
        public string FieldName;
        public string FieldRename;
        public Rename()
        {
            CubeID = -1;
            FieldName = "";
            FieldRename = "";
        }
    }

    public class Duplicate
    {
        public int CubeID;
        public EntityField BaseField;
        public string FieldName;
        public Duplicate()
        {
            CubeID = -1;
            FieldName = "";
            BaseField = new EntityField();
        }
    }

    public class Visibility
    {
        public enum ColumnState : int
        {
            _REMOVE = 0,
            _KEEP = 1
        }
        public int CubeID;
        public string FieldName;
        public ColumnState State;
        public Visibility()
        {
            CubeID = -1;
            FieldName = "";
            State = ColumnState._KEEP;
        }
    }

    public class Formula
    {
        public string FormulaName;
        public string formula;
        public int CubeID;
        public Formula()
        {
            CubeID = -1;
            this.formula = "";
            this.FormulaName = "";
        }
    }

    public enum ParsingType : int
    {
        _NONE = 0,
        _DATE = 1,
        _HOUR = 2,
        _MIN = 3,
        _SEC = 4,
        _DAY = 5,
        _MONTH = 6,
        _QUARTER = 7,
        _YEAR = 8,
        _WEEK = 9,
        _DDMMYYYY = 10,
        _MMDDYYYY = 11,
        _MMMDDYYYY = 12,
        _DDMMMYYYY = 13
    }

    public class DateParse
    {
        public int CubeID;
        public string FieldName;
        public ParsingType ParseType;
        public int trancateIndex;
        public DateParse()
        {
            CubeID = -1;
            FieldName = "";
            ParseType = ParsingType._DATE;
            trancateIndex = -1;
        }
    }

    public enum StringParseType : int
    {
        _NONE = 0,
        _UPPERCASE = 1,
        _LOWERCASE = 2,
        _NORMALCASE = 3,
        _CAPITALIZE = 4,
        _TRANCATE = 5
    }

    public class StringParse
    {
        public int CubeID;
        public string FieldName;
        public StringParseType ParseType;
        public int trancateIndex;
        public StringParse()
        {
            CubeID = -1;
            FieldName = "";
            ParseType = StringParseType._NORMALCASE;
            trancateIndex = -1;
        }
    }

    public class StringReplace
    {
        public int CubeID;
        public string FieldName;
        public string AliasFieldName;
        public List<Replace> Items;
    }

    public class Replace
    {
        public string OldValue;
        public string NewValue;
        public Replace()
        {
            OldValue = "";
            NewValue = "";
        }
    }

    public class Join
    {
        public enum JoiningType : int
        {
            _DIRECTJOIN = 0,
            _LEFTJOIN = 1,
            _RIGHTJOIN = 2
        }
        public int CubeID;
        public int JoinCube;
        public string FieldName;
        public string JoinField;
        public JoiningType JoinType;
        public string JoinCubeFields;

        public Join()
        {
            CubeID = -1;
            JoinCube = -1;
            FieldName = "";
            JoinField = "";
            JoinType = JoiningType._DIRECTJOIN;
            JoinCubeFields = "";
        }
    }

    public class Filter
    {
        public enum FilterAction : int
        {
            _INCLUDE = 1,
            _EXCLUDE = 0
        }
        public enum FilterType : int
        {
            _EQUAL = 0,
            _IN = 1,
            _CONTAIN = 2,
            _BETWEEN = 3,
            _EMPTY = 4
        }
        public enum Condition : int
        {
            _AND = 0,
            _OR = 1
        }
        public enum Mode : int
        {
            _SINGLE = 0,
            _MULTIPLE = 1,
            _ALL = 2
        }

        public int CubeID;
        public List<string> SearchField;
        public FilterType SearchType;
        public FilterAction SearchAction;
        public string Value;
        public string toValue;
        public Condition SearchCondition;
        public Mode ColumnMode;
        public LinkedList<Range> Range;
        public Filter()
        {
        }
    }
    public class Range
    {
        public string FromValue { get; set; }
        public string ToValue { get; set; }
    }
    public class Merge
    {
        public int CubeID;
        public bool IsMergeCube;
        public bool IsForceMerge;
        public List<MergeField> MergeCubeFields { get; set; }
        public string DerivingCubes { get; set; }
        public Merge()
        {
        }
    }

    public class MergeField
    {
        public int CubeID;
        public string SourceField;
        public string Mergingfield;
    }

    public class Split
    {
        public string Splitter;
        public int CubeID;
        public string ColNamePrefix;
        public string FieldName;
    }

}
