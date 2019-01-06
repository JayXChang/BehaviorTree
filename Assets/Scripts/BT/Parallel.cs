using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBehaviorTree
{
    /// <summary>
    /// 并行器：
    /// </summary>
    public class Parallel : Composite
    {
        /// <summary>
        /// 策略
        /// </summary>
        public enum Policy
        {
            RequireOne,
            RequireAll
        }

        protected Policy successPlicy;
        protected Policy failurePlicy;

        public Parallel(Policy success, Policy failure)
        {
            successPlicy = success;
            failurePlicy = failure;
        }

        public override Status Update()
        {
            var successCount = 0;
            var failureCount = 0;

            for (int i = 0; i < children.Count; i++)
            {
                var b = children[i];
                if (!b.IsTerminated) b.Tick();
                if (b.NowStatus == Status.SUSCCESS)
                {
                    successCount++;
                    if (successPlicy == Policy.RequireOne)
                        return Status.SUSCCESS;
                }
                if (b.NowStatus == Status.FAILURE)
                {
                    failureCount++;
                    if (failurePlicy == Policy.RequireOne)
                        return Status.FAILURE;
                }
            }

            if (failurePlicy == Policy.RequireAll && failureCount == children.Count)
                return Status.FAILURE;
            if (successPlicy == Policy.RequireAll && successCount == children.Count)
                return Status.SUSCCESS;
            
            return Status.FAILURE;
        }

        public override void OnTerminate(Status status)
        {
            base.OnTerminate(status);
            children.ForEach(b =>
            {
                if (b.IsRunning)
                    b.Abort();
            });
        }
    }
}