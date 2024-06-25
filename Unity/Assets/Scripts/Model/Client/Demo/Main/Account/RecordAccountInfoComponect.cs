namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class RecordAccountInfoComponect : Entity, IAwake, IDestroy
    {
        public string Token;
        public long RealmKey;
        public string RealmAddress;
        public string Account;
        public long GateId;
    }
}

