using CommandLine;

namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_AddAttributePointHandler : MessageLocationHandler<Unit, C2M_AddAttributePoint, M2C_AddAttributePoint>
    {
        protected override async ETTask Run(Unit unit, C2M_AddAttributePoint request, M2C_AddAttributePoint response)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int targetNumericType = request.NumericType;

            if (!PlayerNumericConfigCategory.Instance.Contain(targetNumericType))
            {
                response.Error = ErrorCode.ERR_NumericTypeNotExist;
                return;
            }

            PlayerNumericConfig config = PlayerNumericConfigCategory.Instance.Get(targetNumericType);
            if (config.isAddPoint == 0)
            {
                response.Error = ErrorCode.ERR_NumericTypeNotAddPoint;
                return;
            }

            int attributePointCount = numericComponent.GetAsInt(NumericType.AttributePoint);
            if (attributePointCount <= 0)
            {
                response.Error = ErrorCode.ERR_AddPointNotEnough;
                return;
            }

            --attributePointCount;
            //这里set方法最终会发送 M2C_NoticeUnitNumeric 更新字段Z
            numericComponent.Set(NumericType.AttributePoint, attributePointCount);
            
            int targetAttributeCount = numericComponent.GetAsInt(targetNumericType) + 1;
            numericComponent.Set(targetNumericType,targetAttributeCount);
            
            await ETTask.CompletedTask;
        }
    }
}