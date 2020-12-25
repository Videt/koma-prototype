using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour
{
    public LayerMask layerMask;
    public float fov; //поле зрения
    public Transform sightDirection;
    public int rayCount = 40; //количество лучей
    public float viewDistance = 4f; //расстояние обзора

    private Mesh mesh;
    private Vector3 origin; //координата начала треугольника (конуса зрения)
    private float angle = 20f; //в каком направлении светит луч
    private float angleIncrease;
    private Vector2 axis;
    private float oldAngle;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 50f;
    }
    private void Update()
    {
        DrawConeView();

        FlipSightDirection();
    }

    //конвертировать угол в Vector3
    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void FlipSightDirection()
    {
        if (axis.x > 0) //идет вправо
        {
            angle = 20f;
            oldAngle = angle;
        }
        else if (axis.x < 0)
        {
            angle = 210f;
            oldAngle = angle;
        }
        else //когда не движется сохраняется старое местоположение конуса
        {
            angle = oldAngle;
        }
    }

    //основная функция
    private void DrawConeView()
    {
        angleIncrease = fov / rayCount;
        axis.x = Input.GetAxis("Horizontal"); //направление движения персонажа (точка sightDirection на нем)
        origin = sightDirection.position;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1]; //координаты вершин мэша; две единицы это начальные вершина и луч
        Vector2[] uv = new Vector2[vertices.Length]; //натягивает графику на вершины
        int[] triangles = new int[rayCount * 3]; //соединяет точки мэша; умножаем на 3, потому что используем треугольник

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);

            //если сталквивается с препятствием, то делает вершиной точку столкновения
            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            //чтобы vertexIndex - 1 не выдал ошибку, так как при i = 0 не будет существовать предыдущей вершины
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease; //минус, а не плюс, чтобы увеличивать угол по часовой
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f); //чтобы мэш не пропал даже на большом расстоянии от точки начала
    }

    //использовать функции если понадобится установить кастомные настройки зрения для отдельных врагов
    /*public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetSightDirection(Vector3 sightDirection)
    {
        angle = GetAngleFromVectorFloat(sightDirection) - fov / 2f;
    }

    public void SetFov(float fov)
    {
        this.fov = fov;
    }
    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }*/

    /*public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        { n += 360; }

        return n;
    }*/
}
