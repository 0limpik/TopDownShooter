using System.Threading.Tasks;
using TopDown.Scripts.Enemy.Behaviors.Base;
using TopDown.Scripts.Extensions;
using UnityEditor;
using UnityEngine;

namespace TopDown.Scripts.Enemy
{
    [RequireComponent(typeof(InitEnemyBehavior))]
    internal class EnemyBehavior : MonoBehaviour
    {
        private InitEnemyBehavior init;

        [SerializeField] private Renderer capsuleRenderer;

        private BaseBehaviour current;

        private string state;

        void Awake()
        {
            init = this.GetComponent<InitEnemyBehavior>();
        }

        void Start()
        {
            Run(init.Patrol).Forget();
        }

        void FixedUpdate()
        {
            if (current.running)
            {
                current.Update(Time.fixedDeltaTime);
            }

            if (init.ChaseAttack.CanAttack())
            {
                Run(init.ChaseAttack).Forget();
            }
            else
            {
                Run(init.Patrol).Forget();
            }
        }

        private Task Run(BaseBehaviour behaviour)
        {
            if (behaviour.running)
                return Task.CompletedTask;

            current?.Stop();

            current = behaviour;

            state = current.GetType().Name;

            if (behaviour == init.Patrol)
            {
                capsuleRenderer.material.color = Color.gray;
            }

            if (behaviour == init.ChaseAttack)
            {
                capsuleRenderer.material.color = Color.red;
            }

            return behaviour.Run();
        }

        void OnDrawGizmos()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
            Handles.Label(init.unit.transform.position, state);
#endif
        }
    }
}
