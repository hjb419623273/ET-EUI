using System.Collections.Generic;

namespace ET.Client
{
    public static class WindowItemRes
    {
        [StaticField]
        public static Dictionary<WindowID, List<string>> WindowItemResDictionary = new Dictionary<WindowID, List<string>>()
        {
			{ WindowID.WindowID_Server, new List<string>(){"Item_ServerInfo",}},
			{ WindowID.WindowID_Role, new List<string>(){"Item_role",}},
			{ WindowID.WindowID_RoleInfo, new List<string>(){"Item_attribute",}},
			{ WindowID.WindowID_Bag, new List<string>(){"Item_bagItem",}},
        };
    }
}
