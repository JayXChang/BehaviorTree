# BehaviorTree

## 项目介绍

Unity超简单的行为树框架(Unity2018.2)
读完《Game AI Pro- Collected Wisdom of Game AI Professionals》第六篇 The Behavior Tree Starter Kit一章的Unity实现版本。

## 控制节点

一般来说，常用的控制节点有以下三种

- 选择（Selector）：选择其子节点的某一个执行
- 序列（Sequence）：将其所有子节点依次执行，也就是说当前一个返回“完成”状态后，再运行先一个子节点
- 并行（Parallel）：将其所有子节点都运行一遍

## 基本要素

- 行为(Behavior)
- 装饰器(Decorator)
- 选择器(Selector)
- 主动选择器(ActiveSelector)
- 序列器(Sequence)
- 过滤器(Filter)
- 并行器(Parallel)
- 监视器(Moniter)
- 动作(BTAction)
- 条件(BTCondition)
- 黑板数据(Blackboard)
- 行为树建造器(BehaviorTreeBuilder)

## 例子

```C#
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
        playerDis = Blackboard.GetData<float>("PlayerDis");
        nowTime = 0;
    }
    public override Status Update()
    {
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
    }
}
```
