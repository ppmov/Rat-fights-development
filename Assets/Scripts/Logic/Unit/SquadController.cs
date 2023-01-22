using Infrastructure.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Logic.Unit
{
    [AddComponentMenu("RTS/Unit/Squad Controller")]
    public class SquadController : MonoBehaviour
    {
        public NavMeshAgent[] agents;

        public void OnGroundClick(Vector3 point)
        {
            foreach (var agent in agents)
                agent.SetDestination(point);
        }
    }
}