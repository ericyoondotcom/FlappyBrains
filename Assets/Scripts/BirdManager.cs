using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BirdManager : MonoBehaviour {

    public GameObject bird;
    public int birdCount;

    GameObject[] birds;
    int aliveBirds = 0;

    public float time = 0;

    int generation = 1;
    float prevScore = 0;
    float hiscore = 0;
    Text text;

	void SpawnBirds () {
        aliveBirds = birdCount;
        birds = new GameObject[birdCount];
        for(int i = 0; i < birds.Length; i++)
        {
            birds[i] = Instantiate(bird);
            birds[i].GetComponent<SpriteRenderer>().color = Color.HSVToRGB((float)i / birdCount, 1, 1);
            BirdBrain bb = birds[i].GetComponent<BirdBrain>();
            bb.birdManager = this;
            //if (i == 0) birds[i].GetComponent<Flap>().human = true;
            bb.Randomize();
            bb.Reset();
        }
	}

    public void BirdDied()
    {
        aliveBirds--;
        if(aliveBirds <= 0)
        {
            generation++;
            print("Resetting");
            ResetAll();
            EvolveBirds();
        }
    }

    void UpdateText()
    {
        if (!text) return;
        text.text = "Gen: " + generation.ToString() + "\nTime: " + time.ToString() + "\nScore: " + prevScore.ToString() + "\nHi Score: " + hiscore.ToString();
    }

    void EvolveBirds()
    {
        Array.Sort<GameObject>(birds, (a, b) => (int)(b.GetComponent<BirdBrain>().distance - a.GetComponent<BirdBrain>().distance));
        BirdBrain topBird = birds[0].GetComponent<BirdBrain>();
        if (topBird.distance > hiscore) hiscore = topBird.distance;
        prevScore = topBird.distance;
        for (int i = (int)(0.1f * birds.Length); i < (int)(0.9f * birds.Length); i++)
        {
            birds[i].GetComponent<BirdBrain>().Mutate(.2f);
        }
        for (int i = (int)(0.9f * birds.Length); i < birds.Length; i++)
        {
            birds[i].GetComponent<BirdBrain>().Randomize();
        }
    }

    void ResetAll()
    {
        time = 0;
        aliveBirds = birdCount;
        PipeSpawner.Instance.DeleteAllPipes();
        for(int i = 0; i < birds.Length; i++)
        {
            birds[i].SetActive(true);
            birds[i].GetComponent<BirdBrain>().Reset();

        }
    }

    private void Start()
    {
        text = GetComponent<Text>();
        SpawnBirds();
        ResetAll();

    }
    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        UpdateText();
	}
}
