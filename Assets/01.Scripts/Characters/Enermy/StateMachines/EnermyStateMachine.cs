using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyStateMachine : StateMachine
{
    public Enermy Enermy { get; }
    public Health Target { get; private set; }

    public EnermyIdleState IdlingState { get; }
    public EnermyChasingState ChasingState { get; }
    public EnermyAttackState AttackState { get; }


    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public EnermyStateMachine(Enermy enermy)
    {
        Enermy = enermy;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        IdlingState = new EnermyIdleState(this);
        ChasingState = new EnermyChasingState(this);
        AttackState = new EnermyAttackState(this);

        MovementSpeed = enermy.Data.GroundedData.BaseSpeed;
        RotationDamping = enermy.Data.GroundedData.BaseRotationDamping;
    }
}
