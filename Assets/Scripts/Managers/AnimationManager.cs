using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private bool _jumping;
    private bool _crouching;
    private bool _sleep;

    private int _currentState;
    private float _lockedTill;

    public Animator Anim;
    public Rigidbody2D Rb;

    private void Update()
    {
        var state = GetState();

        if (state == _currentState)
            return;

        Anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) 
            return _currentState;

        if (_crouching) return Crouch;
        if (_jumping) return Jump;

        if (CollisionDetect.Instance.onGround) 
            return Rb.velocity.x == 0 ? Idle : Walk;

        return Rb.velocity.y > 0 ? Jump : Jump/*Fall*/;
    }

    private static readonly int Idle = Animator.StringToHash("CharacterIdleAnim");
    private static readonly int Walk = Animator.StringToHash("CharacterWalkAnim");
    private static readonly int Jump = Animator.StringToHash("CharacterJumpAnim");
    private static readonly int Crouch = Animator.StringToHash("CharacterCrouchAnim");
    private static readonly int Sleep = Animator.StringToHash("CharacterSleepAnim");
}
