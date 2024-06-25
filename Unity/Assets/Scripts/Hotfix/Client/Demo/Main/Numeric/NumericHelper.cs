using System;
using CommandLine;

namespace ET.Client
{
    public static class NumericHelper
    {

        public static async ETTask<int> TestUpdateNumeric(Scene scene)
        {
            M2C_TestUnitNumeric m2CTestUnitNumeric = null;
            try
            {
                m2CTestUnitNumeric  =  (M2C_TestUnitNumeric) await scene.GetComponent<ClientSenderComponent>().Call(C2M_TestUnitNumeric.Create());
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CTestUnitNumeric.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2CTestUnitNumeric.Error.ToString());
                return m2CTestUnitNumeric.Error;
            }

            return ErrorCode.ERR_Success;
        }
    }
}