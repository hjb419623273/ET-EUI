using System;

namespace ET.Client
{
    public static class ChatHelper
    {
        public static async ETTask<int> SendMessage(Scene scene, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return ErrorCode.ERR_ChatMessageEmpty;
            }

            Chat2C_SendChatInfo chat2CSendChatInfo = null;
            try
            {
                C2Chat_SendChatInfo c2ChatSendChatInfo = C2Chat_SendChatInfo.Create();
                c2ChatSendChatInfo.ChatMessage = message;
                chat2CSendChatInfo = await scene.GetComponent<ClientSenderComponent>().Call(c2ChatSendChatInfo) as Chat2C_SendChatInfo;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (chat2CSendChatInfo.Error != ErrorCode.ERR_Success)
            {
                return chat2CSendChatInfo.Error;
            }

            return ErrorCode.ERR_Success;
        }
    }
}