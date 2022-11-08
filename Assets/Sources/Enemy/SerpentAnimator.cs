using System;
using DG.Tweening;
using JetBrains.Annotations;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class SerpentAnimator : MonoBehaviour, IEnemyAnimator
    {
        private static readonly int Hit = Animator.StringToHash("Take Damage");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int MoveForward = Animator.StringToHash("Move Forward");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int ClawAttackLeft = Animator.StringToHash("Claw Attack Left");
        private static readonly int ClawAttackRight = Animator.StringToHash("Claw Attack Right");

        private readonly int[] _attacks = {ClawAttackLeft, ClawAttackRight};
        private readonly float _downshiftAfterDeath = 5f;
        private readonly float _downshiftDuration = 2.7f;
        private readonly float _downshiftDelaySecond = 3f;

        [SerializeField] private Animator _animator;


        public event Action DeathAnimationEnded;
        public event Action AttackCarried;

        private void Awake()
        {
            int a = ClawAttackLeft;
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

        [UsedImplicitly]
        private void OnAnimationEventAttack()
            => AttackCarried?.Invoke();
    }
}
