using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace ET
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IdStruct
    {
        public short Process;  // 14bit
        public uint Time;    // 30bit
        public uint Value;   // 20bit

        public long ToLong()
        {
            ulong result = 0;
            result |= (ushort) this.Process;
            result <<= 30;
            result |= this.Time;
            result <<= 20;
            result |= this.Value;
            return (long) result;
        }

        public IdStruct(uint time, short process, uint value)
        {
            this.Process = process;
            this.Time = time;
            this.Value = value;
        }

        public IdStruct(long id)
        {
            ulong result = (ulong) id; 
            this.Value = (uint) (result & IdGenerater.Mask20bit);
            result >>= 20;
            this.Time = (uint) result & IdGenerater.Mask30bit;
            result >>= 30;
            this.Process = (short) (result & IdGenerater.Mask14bit);
        }

        public override string ToString()
        {
            return $"process: {this.Process}, time: {this.Time}, value: {this.Value}";
        }
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct InstanceIdStruct
    {
        public uint Time;  // 32bit
        public uint Value; // 32bit

        public long ToLong()
        {
            ulong result = 0;
            result |= this.Time;
            result <<= 32;
            result |= this.Value;
            return (long) result;
        }

        public InstanceIdStruct(uint time, uint value)
        {
            this.Time = time;
            this.Value = value;
        }

        public InstanceIdStruct(long id)
        {
            ulong result = (ulong) id; 
            this.Value = (uint)(result & uint.MaxValue);
            result >>= 32;
            this.Time = (uint)(result & uint.MaxValue);
        }

        public override string ToString()
        {
            return $"time: {this.Time}, value: {this.Value}";
        }
    }
    
      
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UnitIdStruct
    {
        public uint Time;        // 30bit 34年
        public ushort Zone;      // 10bit 1024个区
        public byte ProcessMode; // 8bit  Process % 256  一个区最多256个进程
        public ushort Value;     // 16bit 每秒每个进程最大16K个Unit

        public long ToLong()
        {
            ulong result = 0;
            result |= this.Value;
            result |= (uint)this.ProcessMode << 16;
            result |= (ulong) this.Zone << 24;
            result |= (ulong) this.Time << 34;
            return (long) result;
        }

        public UnitIdStruct(int zone, int process, uint time, ushort value)
        {
            this.Time = time;
            this.ProcessMode = (byte)(process % 256);
            this.Value = value;
            this.Zone = (ushort)zone;
        }
        
        public UnitIdStruct(long id)
        {
            ulong result = (ulong) id;
            this.Value = (ushort)(result & ushort.MaxValue);
            result >>= 16;
            this.ProcessMode = (byte)(result & byte.MaxValue);
            result >>= 8;
            this.Zone = (ushort)(result & 0x03ff);
            result >>= 10;
            this.Time = (uint)result;
        }
                        
        public override string ToString()
        {
            return $"ProcessMode: {this.ProcessMode}, value: {this.Value} time: {this.Time}";
        }
        
        public static int GetUnitZone(long unitId)
        {
            int v = (int) ((unitId >> 24) & 0x03ff); // 取出10bit
            return v;
        }
    }

    public class IdGenerater: Singleton<IdGenerater>, ISingletonAwake
    {
        public const int MaxZone = 1024;
        
        public const int Mask14bit = 0x3fff;
        public const int Mask30bit = 0x3fffffff;
        public const int Mask20bit = 0xfffff;
        
        private long epoch2022;
        
        private int value;
        private int instanceIdValue;
        
        private ushort unitIdValue;
        private uint lastIdTime;
        private uint lastUnitIdTime;
        
        public void Awake()
        {
            long epoch1970tick = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks / 10000;
            this.epoch2022 = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks / 10000 - epoch1970tick;
        }

        private uint TimeSince2022()
        {
            uint a = (uint)((TimeInfo.Instance.FrameTime - this.epoch2022) / 1000);
            return a;
        }
        
        public long GenerateId()
        {
            uint time = TimeSince2022();
            int v = 0;
            // 这里必须加锁
            lock (this)
            {
                if (++this.value > Mask20bit - 1)
                {
                    this.value = 0;
                }
                v = this.value;
            }
            
            IdStruct idStruct = new(time, (short)Options.Instance.Process, (uint)v);
            return idStruct.ToLong();
        }
        
        public long GenerateInstanceId()
        {
            uint time = this.TimeSince2022();
            uint v = (uint)Interlocked.Add(ref this.instanceIdValue, 1);
            InstanceIdStruct instanceIdStruct = new(time, v);
            return instanceIdStruct.ToLong();
        }
        
        public long GenerateUnitId(int zone)
        {
            if (zone > MaxZone)
            {
                throw new Exception($"zone > MaxZone: {zone}");
            }
            uint time = TimeSince2022();

            if (time > this.lastUnitIdTime)
            {
                this.lastUnitIdTime = time;
                this.unitIdValue = 0;
            }
            else
            {
                ++this.unitIdValue;
                
                if (this.unitIdValue > ushort.MaxValue - 1)
                {
                    this.unitIdValue = 0;
                    ++this.lastUnitIdTime; // 借用下一秒
                    Log.Error($"unitid count per sec overflow: {time} {this.lastUnitIdTime}");
                }
            }

            UnitIdStruct unitIdStruct = new UnitIdStruct(zone, Options.Instance.Process, this.lastUnitIdTime, this.unitIdValue);
            return unitIdStruct.ToLong();
        }
    }
}