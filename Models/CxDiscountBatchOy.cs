
using System;

namespace Models
{
    /// <summary>
    /// Code By Tool 2012/8/13 11:35:16
    /// This is a Entity class	
    /// 
    /// </summary>
    [Serializable]
    public class CxDiscountBatchOy : IModel
    {
        public Type zModelType
        {
            get { return typeof(CxDiscountBatchOy); }
        }
        public string zTableName
        {
            get { return "CX_DISCOUNT_BATCH_OY"; }
        }

        #region public - FieldsInfo
        public System.Int64 Id { get; set; }
        public System.String BatchNo { get; set; }
        public System.Int64? ActivitySendId { get; set; }
        public System.Int32 FaceValue { get; set; }
        public System.Int32 Count { get; set; }
        public System.Int32 TakeCount { get; set; }
        public System.String UseArea { get; set; }
        public System.DateTime ValidStartDate { get; set; }
        public System.DateTime ValidEndDate { get; set; }
        public System.String CreateUser { get; set; }
        public System.DateTime? CreateTime { get; set; }
        public System.DateTime? LastchangeTime { get; set; }
        public System.DateTime? OpGuid { get; set; }
        #endregion

        #region Const - FieldsInfo
        public const string iD = "Id";
        public const string bAtchNo = "BatchNo";
        public const string aCtivitySendId = "ActivitySendId";
        public const string fAceValue = "FaceValue";
        public const string cOunt = "Count";
        public const string tAkeCount = "TakeCount";
        public const string uSeArea = "UseArea";
        public const string vAlidStartDate = "ValidStartDate";
        public const string vAlidEndDate = "ValidEndDate";
        public const string cReateUser = "CreateUser";
        public const string cReateTime = "CreateTime";
        public const string lAstchangeTime = "LastchangeTime";
        public const string oPGuid = "OpGuid";
        #endregion
    }
}