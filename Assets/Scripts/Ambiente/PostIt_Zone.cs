using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIt_Zone : MonoBehaviour
{

    public PostIt_Ambiente m_PrefabPostIt;

    private void Awake()
    {
        int n = Mathf.RoundToInt(Random.Range(6, 12));

        for (int i = 0; i < n; i++)
        {
            float x = Random.Range(-0.65f, 0.65f);
            float y = Random.Range(-0.45f, 0.45f);

            Instantiate(m_PrefabPostIt, new Vector3(transform.position.x+x, transform.position.y+y, transform.position.z), transform.rotation, transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
