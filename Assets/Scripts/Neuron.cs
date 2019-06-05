using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron {
    public double bias;
    public double[] weights;

    public Neuron(int inputs){
        weights = new double[inputs];
        bias = 0;
    }

    public void Randomize(){
        bias = Random.Range(-1, 1);
        for (int i = 0; i < weights.Length; i++){
            weights[i] = Random.Range(-1, 1);
        }
    }

    public double Compute(double[] inputs)
    {
        double output = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            output += weights[i] * inputs[i];
        }

        output += bias;

        output = BirdBrain.activation(output);
        return output;

    }

}
