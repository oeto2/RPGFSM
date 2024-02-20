using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyChasingState : EnermyBaseState
{
    public EnermyChasingState(EnermyStateMachine enermyStateMachine) : base(enermyStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 1;
        base.Enter();
        StartAnimation(stateMachine.Enermy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enermy.AnimationData.RunParamerterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enermy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enermy.AnimationData.RunParamerterHash);
    }

    public override void Update()
    {
        base.Update();

        if(!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
            return;
        }
        else if(IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }

    private bool IsInAttackRange()
    {
       // if (stateMachine.Target.IsDead) { return false; }
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enermy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Enermy.Data.AttackRange * stateMachine.Enermy.Data.AttackRange;
    }
}
