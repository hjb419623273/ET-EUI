using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(HeadHpViewComponent))]
    [FriendOfAttribute(typeof(ET.Client.HeadHpViewComponent))]
    public static partial class HeadHpViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.HeadHpViewComponent self)
        {
            GameObject gameObject = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject;
            self.HpBarGroup = gameObject.GetComponent<ReferenceCollector>().GetObject("HpBarGroup") as GameObject;
            //self.HpBarGroup = gameObject.Get<GameObject>("HpBarGroup");
            self.HpText = (gameObject.GetComponent<ReferenceCollector>().GetObject("HpText") as GameObject).GetComponent<TextMeshPro>();
            self.HpBar      = (gameObject.GetComponent<ReferenceCollector>().GetObject("HpBar") as GameObject).GetComponent<SpriteRenderer>();
        }
        [EntitySystem]
        private static void Destroy(this ET.Client.HeadHpViewComponent self)
        {
            
        }

        public static void SetVisible(this HeadHpViewComponent self, bool isVisible)
        {
            self.HpBarGroup?.SetActive(isVisible);
        }

        public static void SetHp(this HeadHpViewComponent self)
        {
            NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();

            int maxHp = numericComponent.GetAsInt(NumericType.MaxHp);
            int Hp = numericComponent.GetAsInt(NumericType.Hp);

            self.HpText.text = $"{Hp} / {maxHp}";
            self.HpBar.size = new Vector2((float)Hp / maxHp, self.HpBar.size.y);
        }
    }
}