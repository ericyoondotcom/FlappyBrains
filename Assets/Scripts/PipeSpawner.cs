using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour {
    public GameObject pipe;
    public float minTime;
    public float maxTime;
    public float minHeight;
    public float maxHeight;

    public List<GameObject> pipes;

    public static PipeSpawner Instance;

    Coroutine cor;
    // Use this for initialization
	void Start () {
        pipes = new List<GameObject>();
        if (Instance == null) Instance = this;
        cor = StartCoroutine(Spawn());
	}

    public void DeleteAllPipes()
    {
        foreach(var p in pipes)
        {
            Destroy(p);
        }
        pipes = new List<GameObject>();
        StopCoroutine(cor);
        cor = StartCoroutine(Spawn());
    }

    public void SpawnPipe()
    {
        pipes.Add(Instantiate(pipe, new Vector3(transform.position.x, Random.Range(minHeight, maxHeight)), Quaternion.identity));
    }

    IEnumerator Spawn()
    {
        while(true){
            SpawnPipe();
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }

}
