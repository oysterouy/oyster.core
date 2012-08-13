
using System;

namespace Models
{
    /// <summary>
    /// Code By Tool 2012/8/13 11:34:35
    /// This is a Entity class	
    /// 
    /// </summary>
    [Serializable]
    public class CxSysFacevalueOy : IModel
    {
        public Type zModelType
        {
            get { return typeof(CxSysFacevalueOy); }
        }
        public string zTableName
        {
            get { return "CX_SYS_FACEVALUE_OY"; }
        }

        #region public - FieldsInfo
        public System.Int64 Id { get; set; }
        public System.Int32 FaceValue { get; set; }
        public System.Boolean? Isvalid { get; set; }
        public System.DateTime? CreateTime { get; set; }
        public System.DateTime? LastchangeTime { get; set; }
        public System.String OpGuid { get; set; }
        #endregion

        #region Const - FieldsInfo
        public const string iD = "Id";
        public const string fAceValue = "FaceValue";
        public const string iSvalid = "Isvalid";
        public const string cReateTime = "CreateTime";
        public const string lAstchangeTime = "LastchangeTime";
        public const string oPGuid = "OpGuid";
        #endregion
    }
}