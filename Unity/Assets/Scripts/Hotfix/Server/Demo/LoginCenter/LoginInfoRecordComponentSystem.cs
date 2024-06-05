namespace ET.Server
{
    // LoginInfoRecordComponent 的扩展函数类
    [EntitySystemOf(typeof(LoginInfoRecordComponent))]
    [FriendOfAttribute(typeof(ET.Server.LoginInfoRecordComponent))]
    public static partial class LoginInfoRecordComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.LoginInfoRecordComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Server.LoginInfoRecordComponent self)
        {
            self.AccountLoginInfoDict.Clear();
        }

        //字典中添加值
        public static void Add(this LoginInfoRecordComponent self, long key, int value)
        {
            if (self.AccountLoginInfoDict.ContainsKey(key))
            {
                //更新
                self.AccountLoginInfoDict[key] = value;
                return;
            }
            //赋值
            self.AccountLoginInfoDict.Add(key,value);
        }
        
        public static void Remove(this LoginInfoRecordComponent self, long key)
        {
            if (self.AccountLoginInfoDict.ContainsKey(key))
            {
                self.AccountLoginInfoDict.Remove(key);
            }
        }

        public static int Get(this LoginInfoRecordComponent self, long key)
        {
            if (!self.AccountLoginInfoDict.TryGetValue(key, out int value))
            {
                return -1;
            }

            return value;
        }

        public static bool IsExist(this LoginInfoRecordComponent self, long key)
        {
            return self.AccountLoginInfoDict.ContainsKey(key);
        }
    }
}