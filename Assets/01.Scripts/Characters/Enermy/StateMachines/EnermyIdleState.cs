using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyIdleState : EnermyBaseState
{
    public EnermyIdleState(EnermyStateMachine enermyStateMachine) : base(enermyStateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;

        base.Enter();
        StartAnimation(stateMachine.Enermy.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Enermy.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enermy.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Enermy.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        if(IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }
}
