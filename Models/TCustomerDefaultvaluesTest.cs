
using System;

namespace Models
{
    /// <summary>
    /// Code By Tool 2012/8/9 14:28:22
    /// This is a Entity class	
    /// 
    /// </summary>
    [Serializable]
    public class TCustomerDefaultvaluesTest : IModel
    {
        public Type zModelType
        {
            get { return typeof(TCustomerDefaultvaluesTest); }
        }
        public string zTableName
        {
            get { return "T_Customer_DefaultValues_TEST"; }
        }

        #region public - FieldsInfo
        public System.Int64 Id;
        public System.String Customerno;
        public System.String Typename;
        public System.String Typecode;
        public System.DateTime? CreateTime;
        public System.DateTime? LastchangeTime;
        public System.String OpGuid;
        #endregion

        #region Const - FieldsInfo
        public const string iD = "Id";
        public const string cUstomerno = "Customerno";
        public const string tYpename = "Typename";
        public const string tYpecode = "Typecode";
        public const string cReateTime = "CreateTime";
        public const string lAstchangeTime = "LastchangeTime";
        public const string oPGuid = "OpGuid";
        #endregion
    }
}