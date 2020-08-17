using Mathematic;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    public int noOfDinos;

    public static List<Dino> dinos = new List<Dino>();
    public static List<Dino> deadDinos = new List<Dino>();
    private List<Dino> nextGen = new List<Dino>();

    public GameObject dinoPrefab;
    public Text genText;
    public int genCounter = 0;

    void Update()
    {
        //Debug.LogError(dinos.Count);

        if (dinos.Count == 0)
        {
            Restart();
        }

        genText.text = $"Gen {genCounter}";
    }

    private void Restart()
    {
        dinos.Clear();
        nextGen.Clear();
        generateDinos();
        deadDinos.Clear();

        Spawner.obstacles.Clear();
        foreach (var item in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            Destroy(item);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("FlyingDino"))
        {
            Destroy(item);
        }

        Spawner.timer = 1.51f;

        genCounter++;
    }

    private List<Dino> StrongestDinos()
    {
        List<Dino> nn = new List<Dino>();

        if (deadDinos.Count == 0)
        {
            nn.Add(dinoPrefab.GetComponent<Dino>());
            nn.Add(dinoPrefab.GetComponent<Dino>());
            nn.Add(dinoPrefab.GetComponent<Dino>());
            nn.Add(dinoPrefab.GetComponent<Dino>());
        }

        else
        {
            var allDinos = deadDinos.OrderBy(x => x.age).ToList();

            nn.Add(allDinos[allDinos.Count - 1]);
            nn.Add(allDinos[allDinos.Count - 2]);
            nn.Add(allDinos[allDinos.Count - 3]);
            nn.Add(allDinos[allDinos.Count - 4]);
        }

        return nn;
    }

    private void generateDinos()
    {
        foreach (var item in StrongestDinos())
        {
            GameObject dino = Instantiate(dinoPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            dino.GetComponent<Dino>().generateNN(item.brain);
            nextGen.Add(item);
            dinos.Add(dino.GetComponent<Dino>());
        }

        while (dinos.Count < noOfDinos)
        {
            GameObject dino = Instantiate(dinoPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

            NeuralNetwork parent1 = nextGen[Random.Range(0, 4)].brain ?? new NeuralNetwork(2, 6, 6, 4);
            NeuralNetwork parent2 = nextGen[Random.Range(0, 4)].brain ?? new NeuralNetwork(2, 6, 6, 4);

            NeuralNetwork nn = NeuralNetwork.CrossOver(parent1, parent2);

            dino.GetComponent<Dino>().generateNN(nn);
            dinos.Add(dino.GetComponent<Dino>());
        }
    }
}
