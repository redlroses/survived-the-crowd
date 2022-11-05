using System;

namespace Sources.Enemy
{
    public interface IEnemyAnimator
    {
        public event Action DeathAnimationEnded;
        public void PlayHit();
        public void PlayDeath();
        public void FinishMove();
        public void PlayAttack();
        public void StartMove();
        public void SetSpeed(float speed);
    }
}
