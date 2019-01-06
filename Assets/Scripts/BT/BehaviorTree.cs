using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace CBehaviorTree
{
    public class BehaviorTree
    {
        public Behavior Root { get; set; }

        public Blackboard Blackboard { get; private set; }

        private bool isLog;

        public BehaviorTree(Behavior root) : base()
        {
            this.Root = root;
        }

        public BehaviorTree()
        {
            Blackboard = new Blackboard();
        }

        public void Tick()
        {
            if (Root == null)
            {
                throw new Exception("行为树根节点不能为空");
            }
            if (Root.IsTerminated)
            {
                if (isLog)
                {
                    isLog = false;
                    Debug.Log($"行为树执行完成,状态为{Root.NowStatus.ToString()}");
                }
                return;
            }
            isLog = true;
            //Debug.Log($"行为树当前状态为{Root.NowStatus.ToString()}");
            Root.Tick();
        }
    }
}