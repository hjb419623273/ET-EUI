namespace ET.Server
{
    [ChildOf]
    public class ChatInfoUnit: Entity,IAwake,IDestroy
    {
        public ActorId PlayerSessionComponentActorId;
        public string Name;
    }
}