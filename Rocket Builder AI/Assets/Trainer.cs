using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour
{
    public AISolution bestSolution;
    public GameObject clone;
    public List<List<AISolution>> generations;
    public int currentGeneration = 0;
    public int solutionsPerGen = 3;
    public int numGenerations = 2;
    public int solutionNum = 0;
    public AISolution currentSolution;
    void Start()
    {
        //bestSolutions = new List<AISolution>();
        generations = new List<List<AISolution>>();

        train();
    }
    GameObject generateSolution()
    {
        return Instantiate(clone , new Vector3(0, 20, 0), Quaternion.identity);
        
    }
    void train()
    {
        if (currentGeneration == 0)
        {
            if (solutionNum < solutionsPerGen)
            {
                solutionNum++;
                List<AISolution> generation = new List<AISolution>();
                AISolution solution = generateSolution().GetComponent<AISolution>();
                currentSolution = solution;
                generation.Add(solution);
                solution.builder.generateShip();
                //TODO fix this generation tracking system its bad.
            }
            else
            {
                currentGeneration++;
                solutionNum = 0;
                Debug.Log("Inital Generation finished!");
            }
        }
        else
        {
            if (currentGeneration < numGenerations)
            {
                if (solutionNum < solutionsPerGen)
                {
                    solutionNum++;

                    AISolution solution = generateSolution().GetComponent<AISolution>();
                    currentSolution = solution;
                    List<float[]> bestShipData = bestSolution.shipBlockData;
                    for (int i = 0; i < bestShipData.Count; i++)
                    {
                        string data = "[";
                        for (int j = 0; j < bestShipData[i].Length; j++)
                        {
                            data += bestShipData[i][j] + ", ";
                        }
                        data += "]  -" + i;
                        Debug.Log(data);
                    }
                    Debug.Log("Loading previous best");
                    solution.builder.loadShipData(bestShipData);
                }
                else
                {
                    currentGeneration++;
                    solutionNum = 0;
                }
            }
        }
    }
    void getBestSolution()
    {
        if (bestSolution == null)
        {
            bestSolution = currentSolution;
        }
        else
        {
            if (currentSolution.fitnessScore > bestSolution.fitnessScore)
                bestSolution = currentSolution;
        }
    }
    // Update is called once per frame
    void Update()
    {
     if(currentSolution!= null)
        {
            if (!currentSolution.running)
            {
                train();
                getBestSolution();
            }
        }   
    }
}
