using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    public class BTAction : Behavior
    {
        public BTAction() : base() { }
    }

    public class DoAction : BTAction
    {
        readonly Action<Blackboard> action;

        public DoAction(Action<Blackboard> action) : base()
        {
            this.action = action;
        }

        public override Status Update()
        {
            action?.Invoke(this.Blackboard);
            return Status.SUSCCESS;
        }
    }
}