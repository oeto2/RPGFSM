using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovemnetSpeedModifier = 0;
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.AttackPramerterHash);
    }

    public override void Exit() 
    {
        base.Exit();

        StopAnimation(stateMachine.Player.AnimationData.AttackPramerterHash);
    }
}
