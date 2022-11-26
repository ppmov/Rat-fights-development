using System.Collections;
using UnityEngine;

namespace Logic.Construction
{
    [AddComponentMenu("RTS/Construction/Site")]
    public class BuildSite : MonoBehaviour
    {
        public bool CanBeCanceled => _buildProcess != null;
        public BuildPlan[] AvailablePlans => _availablePlans;
        
        [SerializeField]
        private BuildPlan[] _availablePlans;
        private BuildPlan[] _lastAvailablePlans;

        private Building _building;
        private Coroutine _buildProcess;

        private void Awake() => _lastAvailablePlans = _availablePlans;

        public void Build(BuildPlan plan) => _buildProcess = StartCoroutine(Construction(plan));
        public void Cancel()
        {
            if (_buildProcess == null)
                return;

            StopCoroutine(_buildProcess);
            _buildProcess = null;
            _availablePlans = _lastAvailablePlans;
        }

        private IEnumerator Construction(BuildPlan plan)
        {
            _availablePlans = new BuildPlan[0];

            if (_building != null)
                Destroy(_building.gameObject);

            yield return new WaitForSeconds(plan.buildDuration);

            _building = Instantiate(plan.prefab, transform.parent).AddComponent<Building>();
            _lastAvailablePlans = _availablePlans = plan.availablePlans;
            _buildProcess = null;
        }
    }
}