using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CBehaviorTree
{
    public class Composite : Behavior
    {
        protected List<Behavior> children = new List<Behavior>();

        public override void OnInitialize()
        {
            base.OnInitialize();
        }

        public override void OnTerminate(Status status)
        {
            base.OnTerminate(status);
        }

        public void AddChild(Behavior behavior)
        {
            if (behavior != null)
                children.Add(behavior);
        }

        public void RemoveChild(Behavior behavior)
        {
            children.Remove(behavior);
        }

        public void ClearChild()
        {
            children.Clear();
        }
    }
}
