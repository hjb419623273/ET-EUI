using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(FlyDamageValueViewComponent))]
    [FriendOfAttribute(typeof(ET.Client.FlyDamageValueViewComponent))]
    public static partial class FlyDamageValueViewComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.FlyDamageValueViewComponent self)
        {
            self.InitFlyDamageViewComponent().Coroutine();
        }
        [EntitySystem]
        private static void Destroy(this ET.Client.FlyDamageValueViewComponent self)
        {
            foreach (GameObject gameObject in self.FlyingDamageSet)
            {
                gameObject.transform.DOKill();
                GameObject.Destroy(gameObject);
            }
            self.FlyingDamageSet.Clear();
        }

        public static async ETTask InitFlyDamageViewComponent(this FlyDamageValueViewComponent self)
        {
            string assetsName = $"Assets/Bundles/Other/flyDamageValue.prefab";
            //GameObject bundleGameObject = await self.Root().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            await GameObjectPoolHelper.InitPoolWithPathAsync("flyDamageValue", assetsName, 3);
        }

        public static async ETTask SpawnFlyDamage(this FlyDamageValueViewComponent self, Vector3 startPos, long DamageValue)
        {
            GameObject flyDamageValueGameObject = GameObjectPoolHelper.GetObjectFromPool("flyDamageValue");
            flyDamageValueGameObject.transform.SetParent(self.Root().GetComponent<GlobalComponent>().Unit);
            self.FlyingDamageSet.Add(flyDamageValueGameObject);
            flyDamageValueGameObject.SetActive(true);

            flyDamageValueGameObject.GetComponentInChildren<TextMeshPro>().text = DamageValue <= 0 ? "Miss" : $"-{DamageValue}";
            flyDamageValueGameObject.transform.position = startPos;

            flyDamageValueGameObject.transform.DOMoveY(startPos.y + 1.5f, 0.8f).onComplete = () =>
            {
                flyDamageValueGameObject.SetActive(false);
                self.FlyingDamageSet.Remove(flyDamageValueGameObject);
                GameObjectPoolHelper.ReturnObjectToPool(flyDamageValueGameObject);
            };
            await ETTask.CompletedTask;
        }
    }
}