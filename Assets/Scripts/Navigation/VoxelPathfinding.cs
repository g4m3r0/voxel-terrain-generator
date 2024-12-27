using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class VoxelPathfinding : MonoBehaviour
{
    public NavMeshSurface navSurface;
    public NavMeshAgent agent;

    [ContextMenu("Build NavMesh")]
    public void BuildNavMesh()
    {
        navSurface.BuildNavMesh();
    }

    private void Start()
    {
        navSurface.BuildNavMesh();
    }

    private void Update()
    {
        // When right mouse is clicked the agent should walk to that spot
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}