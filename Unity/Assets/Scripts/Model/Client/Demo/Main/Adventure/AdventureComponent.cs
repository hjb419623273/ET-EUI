using System.Collections.Generic;
using TrueSync;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class AdventureComponent : Entity, IAwake,IDestroy
    {
        public long BattleTimer = 0;
        public int Round = 0;
        public List<long> EnemyIdList = new List<long>();
        public List<long> AliveEnemyIdList = new List<long>();
        public TSRandom Random = null;
    }
}
