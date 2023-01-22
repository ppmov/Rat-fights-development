using UnityEngine;
using UnityEngine.AI;

namespace Utilities.Navigation
{
    [AddComponentMenu("Navigation/Animated Nav Mesh Agent")]
    public class AnimatedNavMeshAgent : MonoBehaviour
    {
        public Animator animator;
        public NavMeshAgent agent;

        [Space]
        [SerializeField]
        private string _parameterName;

        private void Update() => animator.SetBool(_parameterName, agent.velocity.magnitude > 0);
    }
}