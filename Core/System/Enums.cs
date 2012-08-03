
namespace System
{
    /// <summary>
    /// OyEnums 条件操作
    /// </summary>
    public enum ConditionOperator
    {
        Equal = 1,

        NotEqual = 2,

        GreaterThanOrEqual = 3,

        LessThanOrEqual = 4,

        Greater = 5,

        Less = 6,

        Like = 7,

        LeftLike = 8,

        RightLike = 9,

        And = 10,

        Or = 11,

        IsNull = 12,

        NotIsNull = 13,

        In = 14,

        NotIn = 15,

        Exists = 16,

        NotLike = 17,
    }

    public class NameDesc : Attribute
    {
        public NameDesc(string name = "", string desc = "")
        {
            Name = name;
            Desc = desc;
        }

        public string Name;
        public string Desc;
    }
}
