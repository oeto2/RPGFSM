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
        //�޺� ���� �ִϸ��̼� ����
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackPramerterHash);

        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        //AttackData�� �ִ� �迭�� �����ϴ� ComboIndex�� ����
        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackPramerterHash);

        //�޺� ���� ����
        if (!alreadyApplyCombo)
        {
            stateMachine.ComboIndex = 0;
        }
    }

    private void TryComboAttack()
    {
        //�̹� �޺� �������� ���
        if (alreadyApplyCombo) return;

        //������ ������ ���
        if (attackInfoData.ComboStateIndex == -1) return;

        //�������� �ƴҰ��
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

        //Attack Tag�� �޸� ���� �ִϸ��̼��� �󸶳� ����Ǿ����� �ð��� ����
        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");

        //�ִϸ��̼� ���� ��
        if(normalizedTime < 1f)
        {
            //�ִϸ��̼� �ð� >= �� ���� �ð�
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
                TryApplyForce();

            //�ִϸ��̼� �ð� >= �޺� ���� �ð�
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack();
        }

        //�ִϸ��̼� ����
        else
        {
            //�޺� ���� ���̶��
            if(alreadyApplyCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }

            //�޺� ���ÿ� ���� �ߴٸ�
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}
