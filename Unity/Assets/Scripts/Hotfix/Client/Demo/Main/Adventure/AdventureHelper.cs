using System;
namespace ET.Client
{
    public static class AdventureHelper
    {
        public static async ETTask<int> RequestStartGameLevel(Scene scene, int levelId)
        {
            M2C_StartGameLevel m2CStartGameLevel = null;
            try
            {
                C2M_StartGameLevel c2MStartGameLevel = C2M_StartGameLevel.Create();
                c2MStartGameLevel.LevelId = levelId;
                m2CStartGameLevel = (M2C_StartGameLevel) await scene.Root().GetComponent<ClientSenderComponent>().Call(c2MStartGameLevel);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            
            if (m2CStartGameLevel.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2CStartGameLevel.Error.ToString());
                return m2CStartGameLevel.Error;
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> RequestEndGameLevel(Scene scene, BattleRoundResult battleRoundResult, int round)
        {

            M2C_EndGameLevel m2CEndGameLevel = null;
            try
            {
                C2M_EndGameLevel c2MEndGameLevel = C2M_EndGameLevel.Create();
                c2MEndGameLevel.Round = round;
                c2MEndGameLevel.BattleResult = (int)battleRoundResult;
                
                m2CEndGameLevel = (M2C_EndGameLevel)await scene.Root().GetComponent<ClientSenderComponent>().Call(c2MEndGameLevel);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            if (m2CEndGameLevel.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2CEndGameLevel.Error.ToString());
                return m2CEndGameLevel.Error;
            }

            return ErrorCode.ERR_Success;
        }
    }
}