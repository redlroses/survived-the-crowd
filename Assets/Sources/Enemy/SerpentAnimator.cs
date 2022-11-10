using System;
using DG.Tweening;
using JetBrains.Annotations;
using Sources.AnimatorStateMachine;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class SerpentAnimator : MonoBehaviour, IEnemyAnimator, IAnimationStateReader
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Hit = Animator.StringToHash("Take Damage");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int MoveForward = Animator.StringToHash("Move Forward");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int ClawAttackLeft = Animator.StringToHash("Claw Attack Left");
        private static readonly int ClawAttackRight = Animator.StringToHash("Claw Attack Right");

        private readonly int _moveStateHash = Animator.StringToHash("Blend Tree");

        private readonly int[] _attacks = {ClawAttackLeft, ClawAttackRight};
        private readonly float _downshiftAfterDeath = 5f;
        private readonly float _downshiftDuration = 2.7f;
        private readonly float _downshiftDelaySecond = 3f;

        [SerializeField] private Animator _animator;

        public AnimatorState State { get; private set; }

        public event Action DeathAnimationEnded;

        public event Action AttackCarried;

        private void Awake()
        {
            _animator ??= GetComponent<Animator>();
        }

        public void PlayHit() => _animator.SetTrigger(Hit);

        public void PlayDeath()
        {
            _animator.SetTrigger(Die);
            PlayPostDeath();
        }

        public void FinishMove() => _animator.SetBool(MoveForward, false);

        public void PlayAttack() => _animator.SetTrigger(_attacks.GetRandom());

        public void StartMove() => _animator.SetBool(MoveForward, true);

        public void SetSpeed(float speed) => _animator.SetFloat(Speed, speed);

        private void PlayPostDeath()
        {
            transform.DOMoveY(transform.position.y - _downshiftAfterDeath, _downshiftDuration)
                .SetDelay(_downshiftDelaySecond).OnComplete(() => DeathAnimationEnded?.Invoke());
        }

        public void EnteredState(int stateHash)
        {
            SetStateBy(stateHash);
        }

        public void ExitedState(int stateHash)
        {
        }

        [UsedImplicitly]
        private void OnAnimationEventAttack()
            => AttackCarried?.Invoke();

        private void SetStateBy(int hash)
        {
            if (hash == Hit)
            {
                State = AnimatorState.Hit;
            }
            else if (hash == _moveStateHash)
            {
                State = AnimatorState.Walking;
            }
            else if (hash == ClawAttackLeft)
            {
                State = AnimatorState.Attack;
            }
            else if (hash == ClawAttackRight)
            {
                State = AnimatorState.Attack;
            }
            else if (hash == Idle)
            {
                State = AnimatorState.Idle;
            }
            else
            {
                State = AnimatorState.Unknown;
            }
        }
    }
}
