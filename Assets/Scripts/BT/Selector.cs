using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    /// <summary>
    /// 选择器：选择其子节点的某一个执行
    /// </summary>
    public class Selector : Composite
    {
        protected int currentIndex = 0;

        public override void OnInitialize()
        {
            base.OnInitialize();
            currentIndex = 0;
        }

        public override Status Update()
        {
            while (true)
            {
                var currentChild = children[currentIndex];
                var s = currentChild.Tick();
                if (s != Status.FAILURE) return s;
                if (++currentIndex == children.Count) return Status.FAILURE;
            }
        }
    }
}