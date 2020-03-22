using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject CurrentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPosition;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.startWidth = 0.3f;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempFingerPos, fingerPosition[fingerPosition.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject) Destroy(linePrefab);
            }
        }

        
    }
    void CreateLine()
    {
        CurrentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = CurrentLine.GetComponent<LineRenderer>();
        edgeCollider = CurrentLine.GetComponent<EdgeCollider2D>();
        fingerPosition.Clear();
        fingerPosition.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPosition.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPosition[0]);
        lineRenderer.SetPosition(1, fingerPosition[1]);
        edgeCollider.points = fingerPosition.ToArray();

    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPosition.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPosition.ToArray();
    }
}
