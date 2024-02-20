using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //콤보 어택 애니메이션 실행
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackPramerterHash);

        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        //AttackData에 있는 배열에 존재하는 ComboIndex값 적용
        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackPramerterHash);

        //콤보 어택 실패
        if (!alreadyApplyCombo)
        {
            stateMachine.ComboIndex = 0;
        }
    }

    private void TryComboAttack()
    {
        //이미 콤보 어택중일 경우
        if (alreadyApplyCombo) return;

        //마지막 공격일 경우
        if (attackInfoData.ComboStateIndex == -1) return;

        //공격중이 아닐경우
        if (!stateMachine.IsAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Player.ForceReceiver.Reset();

        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        //Attack Tag가 달린 공격 애니메이션이 얼마나 진행되었는지 시간을 적용
        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");

        //애니메이션 진행 중
        if(normalizedTime < 1f)
        {
            //애니메이션 시간 >= 힘 적용 시간
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
                TryApplyForce();

            //애니메이션 시간 >= 콤보 적용 시간
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack();
        }

        //애니메이션 종료
        else
        {
            //콤보 어택 중이라면
            if(alreadyApplyCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }

            //콤보 어택에 실패 했다면
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}
