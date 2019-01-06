using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    /// <summary>
    /// 特殊情况下拒绝执行子节点的行为树分支
    /// </summary>
    public class Filter : Sequence
    {
        public void AddCondition(BTCondition condition)
        {
            if (condition != null)
                children.Insert(0, condition);
        }

        public void AddAction(Behavior action)
        {
            if (action != null)
                children.Add(action);

        }
    }
}