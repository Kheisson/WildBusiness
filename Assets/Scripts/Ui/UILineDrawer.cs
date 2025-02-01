using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
    public class UILineDrawer : MonoBehaviour
    {
        public GameObject content; // Reference to the Content GameObject
        public GameObject linePrefab; // Reference to the Line prefab (an Image)

        private List<GameObject> lines = new List<GameObject>();
        
        private void Start()
        {
            DrawLines();
        }

        public void DrawLines()
        {
            foreach (GameObject line in lines)
            {
                Destroy(line);
            }
            lines.Clear();

            int childCount = content.transform.childCount;
            for (int i = 0; i < childCount - 1; i++)
            {
                RectTransform currentChild = content.transform.GetChild(i).GetComponent<RectTransform>();
                RectTransform nextChild = content.transform.GetChild(i + 1).GetComponent<RectTransform>();

                Vector3 start = currentChild.position;
                Vector3 end = nextChild.position;

                GameObject line = Instantiate(linePrefab, content.transform);
                lines.Add(line);

                RectTransform lineRect = line.GetComponent<RectTransform>();
                UpdateLine(lineRect, start, end);
            }
            
            foreach (GameObject line in lines)
            {
                line.transform.SetAsFirstSibling();
            }
        }

        private void UpdateLine(RectTransform line, Vector3 start, Vector3 end)
        {
            Vector3 midpoint = (start + end) / 2;

            line.position = midpoint;
            float distance = Vector3.Distance(start, end);
            line.sizeDelta = new Vector2(line.sizeDelta.y, distance);
        }
    }
}
