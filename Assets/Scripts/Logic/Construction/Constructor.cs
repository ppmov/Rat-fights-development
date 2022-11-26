using UnityEngine;

namespace Logic.Construction
{
    public static class Constructor
    {
        public static void Build(BuildPlan plan, BuildSite site)
        {
            Object.Instantiate(plan.prefab.gameObject, site.transform.parent);
            site.availablePlans = plan.availablePlans;
        }
    }
}