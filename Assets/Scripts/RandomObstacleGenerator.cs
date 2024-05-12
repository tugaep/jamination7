using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RandomObstacleGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;

    void Start()
    {
        float noiseSeed0x = Random.Range(-5f, 5f);
        float noiseSeed0y = Random.Range(-5f, 5f);
        float noiseSeed1x = Random.Range(-5f, 5f);
        float noiseSeed1y = Random.Range(-5f, 5f);

        for (float ix = -27; ix < 28; ix += Random.Range(1.2f, 2f))
        {
            for (float iy = -27; iy < 28; iy += Random.Range(1.2f, 2f))
            {
                // Player 1 Object Creation
                if(Mathf.PerlinNoise(ix * noiseSeed1x + noiseSeed0x, iy * noiseSeed1y + noiseSeed0y) > 0.7f)
                {
                    ColorfulObstacle colObs = Instantiate(obstacles[Random.Range(0, 3)], new Vector3(ix, iy, 0), Quaternion.identity).GetComponent<ColorfulObstacle>();
                    colObs.gameObject.layer = 6;
                    colObs.objectOnLayer1 = false;

                    int randomColor = Random.Range(1, 8);
                    colObs.colorRed = (randomColor & 0b100) == 0b100;
                    colObs.colorGreen = (randomColor & 0b010) == 0b010;
                    colObs.colorBlue = (randomColor & 0b001) == 0b001;
                }

                // Player 2 Object Creation
                if (Mathf.PerlinNoise(ix * noiseSeed0x + noiseSeed1x, iy * noiseSeed0y + noiseSeed1y) > 0.7f)
                {
                    ColorfulObstacle colObs = Instantiate(obstacles[Random.Range(0, 3)], new Vector3(ix, iy, 0), Quaternion.identity).GetComponent<ColorfulObstacle>();
                    colObs.gameObject.layer = 7;
                    colObs.objectOnLayer1 = true;

                    int randomColor = Random.Range(1, 8);
                    colObs.colorRed = (randomColor & 0b100) == 0b100;
                    colObs.colorGreen = (randomColor & 0b010) == 0b010;
                    colObs.colorBlue = (randomColor & 0b001) == 0b001;
                }
            }
        }
    }

    void Update()
    {
        
    }
}
