
using System;

namespace Models
{
    /// <summary>
    /// Code By Tool 2012/8/13 11:34:59
    /// This is a Entity class	
    /// 
    /// </summary>
    [Serializable]
    public class CxDiscountOy : IModel
    {
        public Type zModelType
        {
            get { return typeof(CxDiscountOy); }
        }
        public string zTableName
        {
            get { return "CX_DISCOUNT_OY"; }
        }

        #region public - FieldsInfo
        public System.Int64 Id { get; set; }
        public System.String DiscountCode { get; set; }
        public System.Int32 BatchId { get; set; }
        public System.String DiscountStatus { get; set; }
        public System.Int32 SourceUserId { get; set; }
        public System.DateTime? SourceDatetime { get; set; }
        public System.String SourceText { get; set; }
        public System.Int32? UseUserId { get; set; }
        public System.DateTime? UseDatetime { get; set; }
        public System.String UseText { get; set; }
        public System.DateTime? CreateTime { get; set; }
        public System.DateTime? LastchangeTime { get; set; }
        public System.DateTime? OpGuid { get; set; }
        #endregion

        #region Const - FieldsInfo
        public const string iD = "Id";
        public const string dIscountCode = "DiscountCode";
        public const string bAtchId = "BatchId";
        public const string dIscountStatus = "DiscountStatus";
        public const string sOurceUserId = "SourceUserId";
        public const string sOurceDatetime = "SourceDatetime";
        public const string sOurceText = "SourceText";
        public const string uSeUserId = "UseUserId";
        public const string uSeDatetime = "UseDatetime";
        public const string uSeText = "UseText";
        public const string cReateTime = "CreateTime";
        public const string lAstchangeTime = "LastchangeTime";
        public const string oPGuid = "OpGuid";
        #endregion
    }
}