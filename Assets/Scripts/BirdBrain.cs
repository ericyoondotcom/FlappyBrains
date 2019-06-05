
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Flap))]
[RequireComponent(typeof(SpriteRenderer))]
public class BirdBrain : MonoBehaviour {

    public delegate double Activation(double x);

    static double Sigmoid(double x){
        return 1 / (1 + System.Math.Pow(System.Math.E, -x));
    }

    public static Activation activation = Sigmoid;
    static int[] layerNeurons = {2, 3, 3, 1};

    SpriteRenderer sprite;
    Rigidbody2D rb;
    public Layer[] layers;

    public float distance;


    Vector3 startingPos;

    bool _alive = true;

    public BirdManager birdManager;
    Flap flap;

    GameObject closestPipe;

    public bool Alive
    {
        get
        {
            return _alive;
        }
        set
        {
            if(sprite)  sprite.enabled = value;
            if (rb) rb.simulated = value;
            bool prev = _alive;
            _alive = value;
            if (!value && prev)
            {
                birdManager.BirdDied();
            }

        }
    }

    public void Randomize(){
        layers = new Layer[layerNeurons.Length - 1];
        for (int i = 1; i < layerNeurons.Length; i++){
            layers[i - 1] = new Layer(layerNeurons[i], layerNeurons[i - 1]);
            layers[i - 1].Randomize();
        }
    }

    public void Mutate(double rate){
        foreach(Layer layer in layers){
            foreach(Neuron neuron in layer.neurons){
                if(Random.Range(0f, 1f) < rate){
                    neuron.bias += Random.Range(-1f, 1f);
                }

                for (int i = 0; i < neuron.weights.Length; i++){
                    if(Random.Range(0f, 1f) < rate){
                        neuron.weights[i] += Random.Range(-1f, 1f);
                    }
                }
            }
        }
    }

    public void Reset()
    {

        transform.position = startingPos;
        closestPipe = null;
        FindClosestPipe();
        Alive = true;
    }

    public GameObject FindClosestPipe()
    {
        var gos = PipeSpawner.Instance.pipes;
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            if (go.transform.position.x <= transform.position.x + 1) continue;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        closestPipe = closest;
        return closest;
    }

    // Use this for initialization
    void Start () {
        startingPos = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flap = GetComponent<Flap>();
	}

    public double[] Compute(double[] inputs)
    {
        double[] prevOutputs = layers[0].Compute(inputs);

        for (int i = 1; i < layers.Length; i++)
        {
            layers[i].Compute(prevOutputs);
        }
        return prevOutputs;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            distance = birdManager.time;
            Alive = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindClosestPipe();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.K) && Random.Range(0, 5) == 0)
        {
            distance = transform.position.x - startingPos.x;
            Alive = false;
        }
        if (!Alive) return;
        if (closestPipe == null)
        {
            FindClosestPipe();
            return;
        }

        double[] data = { closestPipe.transform.position.y - transform.position.y, closestPipe.transform.position.x };
        double result = Compute(data)[0];
        if (result > 0.5f && !flap.human){
            flap.DoFlap();
        }
	}
}
