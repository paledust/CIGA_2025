using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] private Transform[] stairBars;
    [SerializeField] private float speed;
    private float intersection = 1.5f;
    void Start()
    {
        for (int i = 0; i < stairBars.Length; i++)
        {
            stairBars[i].position = Vector3.down * 4 + Vector3.up * intersection * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < stairBars.Length; i++)
        {
            stairBars[i].position += Vector3.up * speed * Time.deltaTime;
            if (stairBars[i].position.y > 3)
            {
                Vector3 pos = stairBars[i].position;
                int next = i + 1;
                if (next >= stairBars.Length)
                {
                    next = 0;
                }
                pos = stairBars[next].position + Vector3.down * intersection;
                stairBars[i].position = pos;
            }
        }
    }
}
