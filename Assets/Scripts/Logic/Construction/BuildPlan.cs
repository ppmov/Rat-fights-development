using UnityEngine;

namespace Logic.Construction
{
    [CreateAssetMenu(fileName = "New Construct Plan", menuName = "RTS/Construction Plan")]
    public class BuildPlan : ScriptableObject
    {
        public string id;
        public Sprite icon;
        public Building prefab;
        public BuildPlan[] availablePlans;
    }
}