using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    public class Moniter : Parallel
    {
        public Moniter(Policy success, Policy failure) : base(success, failure)
        {
        }

        public Moniter() : base(Policy.RequireAll, Policy.RequireAll)
        {
        }

        public void AddCondition(BTCondition condition)
        {
            if (condition != null)
                children.Insert(0, condition);
        }

        public void AddAction(BTAction action)
        {
            if (action != null)
                children.Add(action);

        }
    }
}