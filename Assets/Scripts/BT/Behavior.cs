using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    public enum Status
    {
        /// <summary>
        /// 无效的行为
        /// </summary>
        INVALID,

        /// <summary>
        /// 成功
        /// </summary>
        SUSCCESS,

        /// <summary>
        /// 运行中
        /// </summary>
        RUNNING,

        /// <summary>
        /// 执行返回失败
        /// </summary>
        FAILURE,

        /// <summary>
        /// 中断
        /// </summary>
        ABORTED
    }

    public interface IBehavior
    {
        /// <summary>
        /// 在Update调用之前调用一次
        /// </summary>
        void OnInitialize();

        /// <summary>
        /// 在每次行为树更新时被调用且仅被调用一次，知道返回状态表示该状态已经停止
        /// </summary>
        Status Update();

        /// <summary>
        /// 当刚刚更新的行为不在处于运行状态时，立即调用一次
        /// </summary>
        /// <param name="status"></param>
        void OnTerminate(Status status);
    }

    public class Behavior : IBehavior
    {
        public Blackboard Blackboard { get; set; }

        public Status NowStatus { get; set; } = Status.INVALID;

        public bool IsTerminated { get { return NowStatus == Status.SUSCCESS || NowStatus == Status.FAILURE; } }

        public bool IsRunning { get { return NowStatus == Status.RUNNING; } }

        public virtual void OnInitialize() { }

        public virtual Status Update() { return default(Status); }

        public virtual void OnTerminate(Status status) { }

        public Behavior() { }

        public Status Tick()
        {
            if (NowStatus != Status.RUNNING) OnInitialize();
            NowStatus = Update();
            if (NowStatus != Status.RUNNING) OnTerminate(NowStatus);
            return NowStatus;

        }
        public void Reset()
        {
            NowStatus = Status.INVALID;
        }

        public void Abort()
        {
            OnTerminate(Status.ABORTED);
            NowStatus = Status.ABORTED;
        }
    }


    public class Blackboard
    {
        private Dictionary<string, IItemBlackboardData> datas = new Dictionary<string, IItemBlackboardData>();

        public void AddData(string key, IItemBlackboardData data)
        {
            datas.Add(key, data);
        }

        public void UpdateData<T>(string key, T data)
        {
            IItemBlackboardData itemData = GetItemData(key);
            if (itemData == null)
            {
                Debug.LogWarning($"黑板上不存在名字为{key}的数据--------");
                return;
            }
            IItemBlackboardData<T> itemTypeData = itemData as IItemBlackboardData<T>;
            itemTypeData.Data = data;

        }

        public T GetData<T>(string key)
        {
            IItemBlackboardData itemData = GetItemData(key);

            var typeData = itemData as IItemBlackboardData<T>;
            if (typeData != null)
                return typeData.Data;

            Debug.LogWarning($"返回了默认值--------");
            return default(T);
        }

        private IItemBlackboardData GetItemData(string key)
        {
            IItemBlackboardData data;
            if (!datas.TryGetValue(key, out data))
            {
                Debug.LogWarning($"没有key为 {key} 的黑板数据");
            }

            return data;
        }

        public bool RemoveData(string key)
        {
            return datas.Remove(key);
        }

        public void Clear()
        {
            datas.Clear();
        }
    }

    public interface IItemBlackboardData
    {
    }

    public interface IItemBlackboardData<T> : IItemBlackboardData
    {
        T Data { get; set; }
    }

    public class FloatBlackboardData : IItemBlackboardData<float>
    {
        public float Data { get; set; }

        public FloatBlackboardData(float data)
        {
            Data = data;
        }
    }

    public class BoolBlackboardData : IItemBlackboardData<bool>
    {
        public bool Data { get; set; }

        public BoolBlackboardData(bool data)
        {
            Data = data;
        }
    }
}