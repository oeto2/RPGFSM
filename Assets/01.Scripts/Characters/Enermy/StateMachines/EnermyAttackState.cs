using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAttackState : EnermyBaseState
{
    public EnermyAttackState(EnermyStateMachine enermyStateMachine) : base(enermyStateMachine)
    {
    }
    private bool alreadyAppliedForce;
    private bool alreadyAppliedDealing;
    public override void Enter()
    {
        alreadyAppliedForce = false;
        alreadyAppliedDealing = false;

        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(stateMachine.Enermy.AnimationData.AttackPramerterHash);
        StartAnimation(stateMachine.Enermy.AnimationData.BaseAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enermy.AnimationData.AttackPramerterHash);
        StopAnimation(stateMachine.Enermy.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Enermy.Animator, "Attack");
        if(normalizedTime < 1f)
        {
            if (normalizedTime >= stateMachine.Enermy.Data.ForceTransitionTime)
                TryApplyForce();

            if(!alreadyAppliedDealing && normalizedTime >= stateMachine.Enermy.Data.Dealing_Start_TransitionTime)
            {
                stateMachine.Enermy.Weapon.SetAttack(stateMachine.Enermy.Data.Damage, stateMachine.Enermy.Data.Force);
                stateMachine.Enermy.Weapon.gameObject.SetActive(true);
                alreadyAppliedDealing = true;
            }
            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Enermy.Data.Dealing_End_TransitionTime)
            {
                stateMachine.Enermy.Weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            if(IsInChaseRange())
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }
        }
    }
    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Enermy.ForceReceiver.Reset();

        stateMachine.Enermy.ForceReceiver.AddForce(stateMachine.Enermy.transform.forward * stateMachine.Enermy.Data.Force);
    }
}
