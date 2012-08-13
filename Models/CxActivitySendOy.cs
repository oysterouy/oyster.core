
using System;

namespace Models
{
    /// <summary>
    /// Code By Tool 2012/8/13 11:35:53
    /// This is a Entity class	
    /// 
    /// </summary>
    [Serializable]
    public class CxActivitySendOy : IModel
    {
        public Type zModelType
        {
            get { return typeof(CxActivitySendOy); }
        }
        public string zTableName
        {
            get { return "CX_ACTIVITY_SEND_OY"; }
        }

        #region public - FieldsInfo
        public System.Int64 Id { get; set; }
        public System.String ActivityType { get; set; }
        public System.String ActivityName { get; set; }
        public System.DateTime StartDatetime { get; set; }
        public System.DateTime EndDatetime { get; set; }
        public System.Decimal ActivityBudget { get; set; }
        public System.String CxRule { get; set; }
        public System.String CxDiscount { get; set; }
        public System.String Note { get; set; }
        public System.DateTime? CreateTime { get; set; }
        public System.DateTime? LastchangeTime { get; set; }
        public System.DateTime? OpGuid { get; set; }
        public System.Int32 Status { get; set; }
        #endregion

        #region Const - FieldsInfo
        public const string iD = "Id";
        public const string aCtivityType = "ActivityType";
        public const string aCtivityName = "ActivityName";
        public const string sTartDatetime = "StartDatetime";
        public const string eNdDatetime = "EndDatetime";
        public const string aCtivityBudget = "ActivityBudget";
        public const string cXRule = "CxRule";
        public const string cXDiscount = "CxDiscount";
        public const string nOte = "Note";
        public const string cReateTime = "CreateTime";
        public const string lAstchangeTime = "LastchangeTime";
        public const string oPGuid = "OpGuid";
        public const string sTatus = "Status";
        #endregion
    }
}