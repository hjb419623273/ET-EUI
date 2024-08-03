namespace ET
{
    //装备装配部位
    public enum EquipPosition
    {
        None    = 0, //不可装备
        Head    = 1, //头盔
        Clothes = 2, //衣服
        Shoes   = 3, //鞋子
        Ring    = 4, //戒指
        Weapon  = 5, //武器
        Shield  = 6, //盾牌
    }
    
    //装备操作指令
    public enum EquipOp
    {
        Load,   //穿戴
        Unload, //卸下
    }
}