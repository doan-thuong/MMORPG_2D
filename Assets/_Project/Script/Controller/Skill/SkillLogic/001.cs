using UnityEngine;

namespace SkillLogic
{
    public class Skill001 : SkillBase
    {
        private GameObject target;
        private float damage => data.b;

        public override void Initialize(GameObject owner)
        {
            EventManager.StartListeningEvent(EventName.Enemy.ENEMY_NEAREST, HandleEnemyTarget);
        }

        void HandleEnemyTarget(object data)
        {
            target = (GameObject)data;
        }

        protected override void Execute()
        {
            if (target.TryGetComponent(out EnemyController enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}