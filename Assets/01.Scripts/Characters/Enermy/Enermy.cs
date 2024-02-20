using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : MonoBehaviour
{
    [field: Header("Refernces")]
    [field: SerializeField] public EnermySO Data { get; private set; }
 
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public CharacterController Controller { get; private set; }

    [field: SerializeField] public Weapon Weapon { get; private set; }

    public Health CharacterHealth { get; private set; }

    private EnermyStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        CharacterHealth = GetComponent<Health>();

        stateMachine = new EnermyStateMachine(this);
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdlingState);
        CharacterHealth.OnDie += OnDie;
    }

    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }
}
