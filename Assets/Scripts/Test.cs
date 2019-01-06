using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CBehaviorTree;
using System;

public class Test : MonoBehaviour
{
    BehaviorTreeBuilder treeBuilder;

    // Use this for initialization
    void Start()
    {
        treeBuilder = new BehaviorTreeBuilder();
        treeBuilder.BehaviorTree.Blackboard.AddData("PlayerDis", new FloatBlackboardData(5));
        Debug.Log($"当前距离玩家 {5}");

        var activeSelector = treeBuilder.CreateAtiveSelector();
        treeBuilder.SetRoot(activeSelector);

        var sequence = treeBuilder.CreateSequence();
        sequence.AddChild(treeBuilder.CreateCondition(IsPlayerInRange));

        var repeat = treeBuilder.CreateRepeat(treeBuilder.CreateAction((blackboard) => Debug.Log("向玩家射击")), 3);
        sequence.AddChild(repeat);

        activeSelector.AddChild(sequence);
        activeSelector.AddChild(treeBuilder.CreateAction<Action_Walk>());
    }

    private bool IsPlayerInRange(Blackboard blackboard)
    {
        var playerDis = blackboard.GetData<float>("PlayerDis");
        if (playerDis <= 2)
        {
            Debug.Log("玩家到达范围内");
        }
        else
        {
            //Debug.Log("玩家不在范围内");
        }
        return playerDis < 3;
    }

    // Update is called once per frame
    void Update()
    {
        treeBuilder.BehaviorTree.Tick();
    }
}

public class Action_Walk : BTAction
{
    float nowTime = 0;
    float playerDis = 0;

    public override void OnInitialize()
    {
        base.OnInitialize();
        //Debug.Log($" Action_Walk OnInitialize");
        playerDis = Blackboard.GetData<float>("PlayerDis");
        nowTime = 0;
    }
    public override Status Update()
    {
        //Debug.Log($"Action_Walk {NowStatus.ToString()}");
        if (nowTime < 1f)
        {
            nowTime += Time.deltaTime;
            return Status.RUNNING;
        }
        nowTime = 0;
        playerDis -= 1f;

        Debug.Log($"向玩家移动,当前距离为 {playerDis}");
        Blackboard.UpdateData("PlayerDis", playerDis);
        return Status.RUNNING;
    }

    public override void OnTerminate(Status status)
    {
        base.OnTerminate(status);
        //Debug.Log($"Action_Walk OnTerminate status = {status.ToString() }");
    }
}