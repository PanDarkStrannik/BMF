using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathVizualizer : MonoBehaviour
{
    [SerializeReference] private NavMeshAgent navigationAgent;
    [SerializeField] private Color gizmosColor;
    [SerializeField,Min(0)] private float pointsRadius=0.1f;

    private void OnDrawGizmosSelected()
    {
        if(navigationAgent!=null)
        {
            if (navigationAgent.path.corners.Length > 0)
            {
                var points = navigationAgent.path.corners;
                Gizmos.color = gizmosColor;
                Gizmos.DrawSphere(navigationAgent.transform.position, pointsRadius);
                Gizmos.DrawLine(navigationAgent.transform.position, points[0]);
                for (int i = 0; i < points.Length; i++)
                {
                    Gizmos.DrawSphere(points[i], pointsRadius);
                    if (i + 1 < points.Length)
                    {
                        Gizmos.DrawLine(points[i], points[i + 1]);
                    }
                }
            }
        }
    }

    //public NavMeshAgent agent; // агент с которого рисуем путь

    //public GameObject point; // префаб для Waypoints
    //public GameObject line; // префаб для линий, между Waypoints

    //public float distance = 1; // минимальная дистанция между точками, чтобы не рисовать те, которые слишком близко
    //public float height = 0.01f; // коррекция позиции по высоте

    //private List<GameObject> points;
    //private Vector3 agentPoint;
    //private Vector3 lastPoint;
    //private List<GameObject> lines;

    //void Awake()
    //{
    //	points = new List<GameObject>();
    //	lines = new List<GameObject>();
    //	UpdatePath();
    //}

    //void ClearArray() // удаление объектов и очистка массивов
    //{
    //	foreach (GameObject obj in points)
    //	{
    //		Destroy(obj);
    //	}
    //	foreach (GameObject obj in lines)
    //	{
    //		Destroy(obj);
    //	}
    //	lines = new List<GameObject>();
    //	points = new List<GameObject>();
    //}

    //bool IsDistance(Vector3 distancePoint) // проверка дистанции между Waypoints
    //{
    //	bool result = false;
    //	float dis = Vector3.Distance(lastPoint, distancePoint);
    //	if (dis > distance) result = true;
    //	lastPoint = distancePoint;
    //	return result;
    //}

    //void UpdatePath() // рисуем путь
    //{
    //	lastPoint = agent.transform.position + Vector3.forward * 100f; // чтобы создать точку в текущей позиции

    //	ClearArray();

    //	for (int j = 0; j < agent.path.corners.Length; j++)
    //	{
    //		if (IsDistance(agent.path.corners[j]))
    //		{
    //			GameObject p = Instantiate(point) as GameObject;
    //			p.transform.position = agent.path.corners[j] + Vector3.up * height; // создаем точку и корректируем позицию 
    //			points.Add(p);
    //		}
    //	}

    //	for (int j = 0; j < points.Count; j++)
    //	{
    //		if (j + 1 < points.Count)
    //		{
    //			Vector3 center = (points[j].transform.position + points[j + 1].transform.position) / 2; // находим центр между точками
    //			Vector3 vec = points[j].transform.position - points[j + 1].transform.position; // находим вектор от точки А, к точке Б
    //			float dis = Vector3.Distance(points[j].transform.position, points[j + 1].transform.position); // находим дистанцию между А и Б

    //			GameObject p = Instantiate(line) as GameObject;
    //			p.transform.position = center - Vector3.up * height;
    //			p.transform.rotation = Quaternion.FromToRotation(Vector3.right, vec.normalized); // разворот по вектору
    //			p.transform.localScale = new Vector3(dis, 1, 1); // растягиваем по Х
    //			lines.Add(p);
    //		}
    //	}
    //}

    //void Update()
    //{
    //	RaycastHit hit;
    //	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //	if (Physics.Raycast(ray, out hit))
    //	{
    //		if (Input.GetMouseButtonDown(0))
    //		{
    //			agent.SetDestination(hit.point); // назначаем точку следования
    //		}
    //	}

    //	if (agentPoint != agent.path.corners[agent.path.corners.Length - 1]) UpdatePath(); // рисуем путь если был изменена конечная точка назначения
    //	agentPoint = agent.path.corners[agent.path.corners.Length - 1]; // запоминаем текущую конечную точку назначения

    //	if (agent.path.corners.Length == 1 && points.Count > 1) UpdatePath(); // рисуем путь, после прибытия в точку назначения
    //}
}
