using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string comboAttackParmeterName = "ComboAttack";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParamerterHash { get; private set; }
    public int RunParamerterHash { get; private set; }

    public int AirParamerterHash { get; private set; }
    public int JumpParamerterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    public int AttackPramerterHash { get; private set; }
    public int ComboAttackPramerterHash { get; private set; }


    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParamerterHash = Animator.StringToHash(walkParameterName);
        RunParamerterHash = Animator.StringToHash(runParameterName);

        AirParamerterHash = Animator.StringToHash(airParameterName);
        JumpParamerterHash = Animator.StringToHash(jumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);

        AttackPramerterHash = Animator.StringToHash(attackParameterName);
        ComboAttackPramerterHash = Animator.StringToHash(comboAttackParmeterName);
    }
}


