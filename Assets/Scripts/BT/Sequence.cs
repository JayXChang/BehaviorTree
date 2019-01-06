using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    /// <summary>
    /// 顺序器依次执行每一个子行为，直到所有子节点都成功或者有一个子节点失败为止
    /// </summary>
    public class Sequence : Composite
    {
        private int currentIndex = 0;

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
                if (s != Status.SUSCCESS) return s;
                if (++currentIndex == children.Count) return Status.SUSCCESS;
            }
        }

        public override void OnTerminate(Status status)
        {
            base.OnTerminate(status);
        }
    }
}
