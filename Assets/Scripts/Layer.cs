using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer {

    public Neuron[] neurons;

    public Layer(int neuronCount, int inputs)
    {
        neurons = new Neuron[neuronCount];
        for (int i = 0; i < neuronCount; i++){
            neurons[i] = new Neuron(inputs);
        }
    }

    public void Randomize(){
        for (int i = 0; i < neurons.Length; i++){
            neurons[i].Randomize();
        }
    }

    public double[] Compute(double[] inputs)
    {
        double[] outputs = new double[neurons.Length];
        for (int i = 0; i < neurons.Length; i++)
        {
            outputs[i] = neurons[i].Compute(inputs);
        }
        return outputs;
    }
}
