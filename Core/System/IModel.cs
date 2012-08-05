using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 使用本框架的Model层，建议使用CodeSmith自动生成，示例：
    /// database table :
    /// user(id(long)*,name(varchar(50)),age(int),create_time(datatime)*,lastchange_time(datetime)*
    /// ,op_guid(varchar(100))*);带*号的字段为本框架必须，且id必须是主键。
    /// 
    /// Model:
    /// public class User:IModel{
    ///    public Type zModelType
    ///    {
    ///        get { return typeof(User); }
    ///    }
    ///    public string zTableName
    ///    {
    ///        get { return "user"; }
    ///    }
    ///
    ///    #region public - FieldsInfo
    ///    public System.Int64 Id;
    ///    public System.String Name;
    ///    public System.Int32 Age;
    ///    public System.DateTime CreateTime;
    ///    public System.DateTime LastchangeTime;
    ///    public System.String OpGuid;
    ///    #endregion
    ///    
    ///    #region Const - FieldsInfo
    ///    public const string iD = "Id";
    ///    public const string nAme = "Name";
    ///    public const string aGe = "Age";
    ///    public const string cReateTime = "CreateTime";
    ///    public const string lAstchangeTime = "LastchangeTime";
    ///    public const string oPGuid = "OpGuid";
    ///    #endregion
    /// 
    /// }
    /// 
    /// 在使用OyCondtion,OyValue,等时传递的第一个参数为属性或字段名，如： new OyCondition(User.iD,1);
    /// 
    /// </summary>
    public interface IModel
    {
        Type zModelType { get; }
        string zTableName { get; }
    }
}
