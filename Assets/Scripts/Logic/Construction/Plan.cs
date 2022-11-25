using UnityEngine;

namespace Logic.Construction
{
    [CreateAssetMenu(fileName = "New Construct Plan", menuName = "RTS/Construction Plan")]
    public class Plan : ScriptableObject
    {
        public string id;
        public Sprite icon;
        public GameObject prefab;
    }
}