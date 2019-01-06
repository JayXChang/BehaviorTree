using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    /// <summary>
    /// 
    /// </summary>
    public class ActiveSelector : Selector
    {
        public override Status Update()
        {
            var prev = currentIndex;
            base.OnInitialize();
            Status result = base.Update();
            if (prev != children.Count && currentIndex != prev)
                children[prev].OnTerminate(Status.ABORTED);
            return result;
        }
    }
}