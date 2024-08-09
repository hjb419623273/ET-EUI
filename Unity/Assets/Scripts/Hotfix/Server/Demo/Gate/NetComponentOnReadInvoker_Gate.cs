﻿using System;

namespace ET.Server
{
    [Invoke((long)SceneType.Gate)]
    public class NetComponentOnReadInvoker_Gate: AInvokeHandler<NetComponentOnRead>
    {
        public override void Handle(NetComponentOnRead args)
        {
            HandleAsync(args).Coroutine();
        }

        private async ETTask HandleAsync(NetComponentOnRead args)
        {
            Session session = args.Session;
            object message = args.Message;
            Scene root = args.Session.Root();
            // 根据消息接口判断是不是Actor消息，不同的接口做不同的处理,比如需要转发给Chat Scene，可以做一个IChatMessage接口
            switch (message)
            {
                case ISessionMessage:
                {
                    MessageSessionDispatcher.Instance.Handle(session, message);
                    break;
                }
                case FrameMessage frameMessage:
                {
                    Player player = session.GetComponent<SessionPlayerComponent>().Player;
                    ActorId roomActorId = player.GetComponent<PlayerRoomComponent>().RoomActorId;
                    frameMessage.PlayerId = player.Id;
                    root.GetComponent<MessageSender>().Send(roomActorId, frameMessage);
                    break;
                }
                case IRoomMessage actorRoom:
                {
                    Player player = session.GetComponent<SessionPlayerComponent>().Player;
                    ActorId roomActorId = player.GetComponent<PlayerRoomComponent>().RoomActorId;
                    actorRoom.PlayerId = player.Id;
                    root.GetComponent<MessageSender>().Send(roomActorId, actorRoom);
                    break;
                }
                case ILocationMessage actorLocationMessage:
                {
                    long unitId = session.GetComponent<SessionPlayerComponent>().Player.Id;
                    root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Send(unitId, actorLocationMessage);
                    break;
                }
                case ILocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                {
                    long unitId = session.GetComponent<SessionPlayerComponent>().Player.Id;
                    int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                    long instanceId = session.InstanceId;
                    IResponse iResponse = await root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Call(unitId, actorLocationRequest);
                    iResponse.RpcId = rpcId;
                    // session可能已经断开了，所以这里需要判断
                    if (session.InstanceId == instanceId)
                    {
                        session.Send(iResponse);
                    }
                    break;
                }
                case IActorRankInfoMessage actorRankInfoMessage:
                {
                    ActorId rankActorId = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), "Rank").ActorId;
                    //gate转发给排行榜服
                    session.Root().GetComponent<MessageSender>().Send(rankActorId, actorRankInfoMessage);
                    break;
                }
                case IActorRankInfoRequest actorRankInfoRequest:
                {
                    ActorId rankInstanceId = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), "Rank").ActorId;
                    int rpcId = actorRankInfoRequest.RpcId;
                    long instanceId = session.InstanceId;
                    IResponse iResponse = await session.Root().GetComponent<MessageSender>().Call(rankInstanceId, actorRankInfoRequest);
                    iResponse.RpcId = rpcId;
                    // session可能已经断开了，所以这里需要判断
                    if (session.InstanceId == instanceId)
                    {
                        session.Send(iResponse);
                    }
                    break;
                }
                case IActorChatInfoRequest actorChatInfoRequest:
                {
                    Player player = session.GetComponent<SessionPlayerComponent>().Player;
                    if (player == null || player.IsDisposed)
                    {
                        break;
                    }

                    int rpcId = actorChatInfoRequest.RpcId;  //这里要保存客户端的rpcId
                    long instanceId = session.InstanceId;
                    Log.Warning($"chatInfoPlayer Component, form : {player.ChatInfoActorId} ChatUnit.InstanceId:{player.ChatInfoActorId.InstanceId}");
                    IResponse iResponse = await session.Root().GetComponent<MessageSender>().Call(player.ChatInfoActorId, actorChatInfoRequest);
                    iResponse.RpcId = rpcId;
                    if (session.InstanceId == instanceId)
                    {
                        session.Send(iResponse);
                    }
                    break;
                }
                case IActorChatInfoMessage actorChatInfoMessage:
                {
                    Player player = session.GetComponent<SessionPlayerComponent>().Player;
                    if (player == null || player.IsDisposed)
                    {
                        break;
                    }
                    session.Root().GetComponent<MessageSender>().Send(player.ChatInfoActorId, actorChatInfoMessage);
                    break;
                }
                case IRequest actorRequest:  // 分发IActorRequest消息，目前没有用到，需要的自己添加
                {
                    break;
                }
                case IMessage actorMessage:  // 分发IActorMessage消息，目前没有用到，需要的自己添加
                {
                    break;
                }
				
                default:
                {
                    throw new Exception($"not found handler: {message}");
                }
            }
        }
    }
}