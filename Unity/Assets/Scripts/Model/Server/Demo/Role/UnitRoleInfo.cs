namespace ET.Server
{
    [ComponentOf]
    public class UnitRoleInfo : Entity, IAwake, ITransfer, IUnitCache
    {
        public string Name;
        public string Account;
    }
}