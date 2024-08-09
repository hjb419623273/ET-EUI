namespace ET.Client
{
    [ChildOf]
    public class ChatInfo : Entity, IAwake, IDestroy
    {
        public string Name;
        public string Message;
    }
}