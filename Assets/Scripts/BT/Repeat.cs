using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    public class Repeat : Decorator
    {
        private readonly int limit;
        private int counter = 0;
        public Repeat(Behavior child, int limit) : base(child)
        {
            this.limit = limit;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            counter = 0;
        }

        public override Status Update()
        {
            while (true)
            {
                child.Tick();
                if (child.NowStatus == Status.RUNNING) break;
                if (child.NowStatus == Status.FAILURE) return Status.FAILURE;
                if (++counter == limit) return Status.SUSCCESS;
                child.Reset();
            }
            return Status.INVALID;
        }
    }
}
