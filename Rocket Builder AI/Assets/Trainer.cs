using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour
{
    public AISolution bestSolution;
    public GameObject cam;
    public GameObject clone;
    public List<List<AISolution>> generations;
    public int currentGeneration = 0;
    public int solutionsPerGen = 10;
    public int numGenerations = 50;
    public int solutionNum = 0;
    public UnityEngine.UI.Text record;
    public UnityEngine.UI.Text height;
    public float zoom = 40;
    public bool replay;
    private int k = 4;
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
            if ((solutionNum < solutionsPerGen) && (currentGeneration < numGenerations))
            {
                
                List<AISolution> generation = new List<AISolution>();
                AISolution solution = generateSolution().GetComponent<AISolution>();
                solution.builder.parts = k;
                solution.builder.replay = this.replay;
                currentSolution = solution;
                generation.Add(solution);
                solution.builder.generateShip();
                solutionNum++;
                //TODO fix this generation tracking system its bad.
            }
            else
            {
                currentGeneration++;
                //k+=4;
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
                    solution.builder.replay = this.replay;
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
            {
                Debug.Log("New Best solution found!");
                bestSolution = currentSolution;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
     if(currentSolution!= null)
        {
            if (!currentSolution.running)
            {
                getBestSolution();
                record.text = "Record: " + bestSolution.fitnessScore + "m";
                train();
            }

            cam.transform.position = currentSolution.Cockpit.transform.position + new Vector3(0,0,-zoom);
            height.text = "Height: " + currentSolution.fitnessScore + "m";
            

        }   
    }
}
