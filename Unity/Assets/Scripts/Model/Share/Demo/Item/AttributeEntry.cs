namespace ET
{
    public enum EntryType
    {
        Common = 1,  //普通词条
        Special = 2, //特殊词条
    }
    
    [ChildOf]
    public class AttributeEntry: Entity,IAwake,IDestroy,ISerializeToEntity
    {
        //词条数值属性类型
        public int Key;

        //词条数值属性值
        public long Value;

        //词条类型
        public EntryType Type;
    }
}