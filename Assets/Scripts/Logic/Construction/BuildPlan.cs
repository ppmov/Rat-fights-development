using UnityEngine;

namespace Logic.Construction
{
    [CreateAssetMenu(fileName = "New Construct Plan", menuName = "RTS/Construction Plan")]
    public class BuildPlan : ScriptableObject
    {
        public Sprite icon;
        public GameObject prefab;
        public float buildDuration;
        public BuildPlan[] availablePlans;
    }
}