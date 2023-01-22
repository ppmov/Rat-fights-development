using UnityEngine;

namespace Logic.Construction
{
    [AddComponentMenu("RTS/Construction/Building")]
    [RequireComponent(typeof(MeshRenderer))]
    public class Building : MonoBehaviour
    {
        private MeshRenderer _mesh; 

        private void Awake() => _mesh = GetComponent<MeshRenderer>();

        private void Start() => PutOnGround();

        private void PutOnGround() => transform.localPosition = new(0f, _mesh.bounds.center.y - _mesh.bounds.min.y, 0f);
    }
}