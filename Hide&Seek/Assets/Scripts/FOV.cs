using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FOV : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;


    public LayerMask[] obstacleMasks;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    public int edgeResolveIterations;


    public List<Transform> VisibleTargets = new List<Transform>();
    public List<Transform> Targets = new List<Transform>();

    public float meshResolution;
    public float edgeDstThreshold;

    private void Start()
    {
        viewMesh = new Mesh();

        //viewMeshFilter = new MeshFilter();

        viewMesh.name= "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetsWithDelay",0.2f);

        obstacleMask = LayerMask.GetMask("Obstacles","door","Floor");
    }

    

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void Update()
    {
        DrawFieldOfView();
    }

    void FindVisibleTargets()
    {
        foreach (var item in VisibleTargets)
        {
            //item.GetComponent<MeshRenderer>().enabled = false;
            if(item.GetComponent<NavMeshAgent>() != null && !item.CompareTag("Catched") && !item.CompareTag( "CatchedPlayer"))
            item.tag = "Hider";
            else if((!item.CompareTag("CatchedPlayer") && !item.CompareTag("Catched")))
            item.tag = "HiderPlayer";
        }
        VisibleTargets.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position,viewRadius,targetMask);

        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget,obstacleMask))
                {
                    VisibleTargets.Add(target);
                    if (target.CompareTag("HiderPlayer") || target.CompareTag("VisibleHiderPlayer"))
                        target.tag = "VisibleHiderPlayer";
                    else if (!target.CompareTag("CatchedPlayer") && !target.CompareTag("Catched"))
                        target.tag = "VisibleHider";
                    

                    //target.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }


    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle*meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize*i;
            ViewCastInfo viewCast = ViewCast(angle);

            if(i>0)
            {
                bool edgeDstThresholdEXceeded = Mathf.Abs(oldViewCast.dst-viewCast.dst)>edgeDstThreshold;
                if(oldViewCast.hit != viewCast.hit || oldViewCast.hit && viewCast.hit && edgeDstThresholdEXceeded)
                {
                    EdgeInfo edge = FindEdge(oldViewCast,viewCast);
                    if (edge.pointA!=Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(viewCast.point);
            oldViewCast = viewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount+1];


        int[] triangles;
        int n = (vertexCount - 2) * 3;
        if(n>=0)
        triangles = new int[n];
        else
        triangles = new int[0];



        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount-1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCastInfo, ViewCastInfo maxViewCastInfo)
    {
        float minAngle = minViewCastInfo.angle;
        float maxAngle = maxViewCastInfo.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;
        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdEXceeded = Mathf.Abs(minViewCastInfo.dst - newViewCast.dst) > edgeDstThreshold;

            if (newViewCast.hit == minViewCastInfo.hit &&!edgeDstThresholdEXceeded )
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint,maxPoint);
    }


    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position,dir,out hit,viewRadius,obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool hit,Vector3 point,float dst,float angle)
        {
            this.hit = hit;
            this.point = point;
            this.dst = dst;
            this.angle = angle;
        }

    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;
        public EdgeInfo(Vector3 pointA,Vector3 pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
        }

    }
}
