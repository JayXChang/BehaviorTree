using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    public class Decorator : Behavior
    {
        protected Behavior child;

        public Decorator(Behavior child)
        {
            this.child = child;
        }
    }
}