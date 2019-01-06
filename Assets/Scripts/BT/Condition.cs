using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CBehaviorTree
{
    public class BTCondition : Behavior
    {
        public BTCondition() : base() { }
    }

    public class FuncBTCondition : BTCondition
    {
        readonly Func<Blackboard, bool> match;

        public FuncBTCondition(Func<Blackboard, bool> match) : base()
        {
            this.match = match;
        }

        public override Status Update()
        {
            if (match(this.Blackboard))
                return Status.SUSCCESS;
            return Status.FAILURE;
        }
    }
}