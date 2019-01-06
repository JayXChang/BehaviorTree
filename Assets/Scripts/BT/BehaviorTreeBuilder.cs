using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CBehaviorTree.Parallel;

namespace CBehaviorTree
{
    public class BehaviorTreeBuilder
    {
        public BehaviorTree BehaviorTree { get; private set; }

        public BehaviorTreeBuilder()
        {
            BehaviorTree = new BehaviorTree();
        }

        public BehaviorTree SetRoot(Behavior root)
        {
            if (BehaviorTree.Root == null)
            {
                BehaviorTree.Root = root;
            }
            return BehaviorTree;
        }

        public ActiveSelector CreateAtiveSelector()
        {
            ActiveSelector activeSelector = new ActiveSelector();
            SetBlackboard(activeSelector);
            return activeSelector;
        }

        public Selector CreateSelector()
        {
            Selector selector = new Selector();
            SetBlackboard(selector);
            return selector;
        }

        public Decorator CreateDecorator(Behavior child)
        {
            Decorator decorator = new Decorator(child);
            SetBlackboard(decorator);
            return decorator;
        }

        public Sequence CreateSequence()
        {
            Sequence sequence = new Sequence();
            SetBlackboard(sequence);
            return sequence;
        }

        public Filter CreateFilter()
        {
            Filter filter = new Filter();
            SetBlackboard(filter);
            return filter;
        }

        public Parallel CreateParallel(Policy success = Policy.RequireAll, Policy failure = Policy.RequireOne)
        {
            Parallel parallel = new Parallel(success, failure);
            SetBlackboard(parallel);
            return parallel;
        }

        public Moniter CreateMoniter(Policy success = Policy.RequireAll, Policy failure = Policy.RequireOne)
        {
            Moniter moniter = new Moniter(success, failure);
            SetBlackboard(moniter);
            return moniter;
        }

        public Repeat CreateRepeat(Behavior child, int limit)
        {
            Repeat repeat = new Repeat(child, limit);
            SetBlackboard(repeat);
            return repeat;
        }

        public BTCondition CreateCondition<T>() where T : BTCondition, new()
        {
            BTCondition condition = new T();
            SetBlackboard(condition);
            return condition;
        }

        public BTCondition CreateCondition(Func<Blackboard, bool> match)
        {
            FuncBTCondition condition = new FuncBTCondition(match);
            SetBlackboard(condition);
            return condition;
        }

        public BTCondition CreateCondition(bool match)
        {
            return CreateCondition((blackboard) => match);
        }

        public BTAction CreateAction<T>() where T : BTAction, new()
        {
            BTAction action = new T();
            SetBlackboard(action);
            return action;
        }

        public BTAction CreateAction(Action<Blackboard> doAction)
        {
            DoAction action = new DoAction(doAction);
            SetBlackboard(action);
            return action;
        }

        void SetBlackboard(Behavior behavior)
        {
            behavior.Blackboard = BehaviorTree.Blackboard;
        }
    }
}
