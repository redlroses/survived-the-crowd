using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JetBrains.Annotations;
using Sources.AnimatorStateMachine;
using Sources.HealthLogic;
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

        private readonly int[] _attacks = { ClawAttackLeft, ClawAttackRight };
        private readonly float _downshiftAfterDeath = 1.5f;
        private readonly float _downshiftDelaySecond = 2f;
        private readonly float _downshiftDuration = 1.76f;
        private readonly float _hitAnimationDelay = 1.39f;

        private readonly int _moveStateHash = Animator.StringToHash("Blend Tree");

        [SerializeField] private Animator _animator;
        [SerializeField] [RequireInterface(typeof(IHealth))] private MonoBehaviour _health;

        private bool _isHitReady;
        private TweenerCore<Vector3, Vector3, VectorOptions> _moveTween;
        private WaitForSeconds _waitForHit;

        public event Action DeathAnimationEnded;

        public event Action AttackCarried;

        public AnimatorState State { get; private set; }

        private IHealth Health => (IHealth)_health;

        private void Awake()
        {
            _waitForHit = new WaitForSeconds(_hitAnimationDelay);
            _animator ??= GetComponent<Animator>();

            _moveTween = transform.DOMoveY(transform.position.y - _downshiftAfterDeath, _downshiftDuration)
                .SetDelay(_downshiftDelaySecond)
                .OnComplete(InvokeAfterDeathAnimation);
        }

        private void OnEnable()
        {
            _isHitReady = true;
            RestorePosition();
            Health.Ended += OnEndedHealth;
            Health.Changed += PlayHit;
        }

        private void OnDisable()
        {
            Health.Ended -= OnEndedHealth;
            Health.Changed -= PlayHit;
        }

        public void EnteredState(int stateHash)
        {
            SetStateBy(stateHash);
        }

        public void ExitedState(int stateHash)
        {
        }

        public void PlayHit()
        {
            if (_isHitReady)
            {
                StartCoroutine(PlayDelayHit());
            }
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(Die);
            PlayPostDeath();
        }

        public void FinishMove() =>
            _animator.SetBool(MoveForward, false);

        public void PlayAttack() =>
            _animator.SetTrigger(_attacks.GetRandom());

        public void StartMove() =>
            _animator.SetBool(MoveForward, true);

        public void SetSpeed(float speed) =>
            _animator.SetFloat(Speed, speed);

        private void PlayPostDeath()
        {
            _moveTween.Restart();
        }

        private void RestorePosition()
        {
            Vector3 position = transform.localPosition;
            transform.localPosition = position.SetY(0);
        }

        private void OnEndedHealth()
            => PlayDeath();

        private void InvokeAfterDeathAnimation()
        {
            DeathAnimationEnded?.Invoke();
        }

        [UsedImplicitly]
        private void OnAnimationEventAttack()
            => AttackCarried?.Invoke();

        private IEnumerator PlayDelayHit()
        {
            _isHitReady = false;

            _animator.SetTrigger(Hit);

            yield return _waitForHit;

            _isHitReady = true;
        }

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