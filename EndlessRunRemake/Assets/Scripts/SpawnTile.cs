using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnTile : MonoBehaviour
{

    public GameObject tileToSpawn;
    public GameObject ballToSpawn;
    public GameObject bombToSpawn;
    public GameObject bushToSpawn, bushToSpawn_1;

    //  public float zaman;
    public float distanceBetweenTiles;
    private float startTime = 1f;
    private Vector3 previousTilePosition;
    private Vector3 previousBallPositon;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1);
    int counter;
    GameObject prevTile;
    int bombtilecount = 3;




    void Start()
    {
        //  direction = mainDirection;
        for (int i = 0; i < 50; i++)
        {

            bool corner = false;
            Vector3[] dirs = { Vector3.forward, Vector3.left, Vector3.right };

            var prev = direction;

            if (counter >= 3)
            {
                direction = dirs[Random.Range(0, dirs.Length)];

                while (direction == prev * -1)
                {
                    direction = dirs[Random.Range(0, dirs.Length)];
                }
                counter = 0;


                if (direction != prev)
                {
                    prevTile.name = "kose";
                    prevTile.transform.GetChild(0).GetComponent<TurnControl>().direction = direction;
                    prevTile.transform.GetChild(0).gameObject.SetActive(true);
                    corner = true;

                }
            }


            else
                direction = prev;

            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;

            if (i == 0)
                spawnPos = Vector3.zero;


            counter++;

            startTime = Time.time;


            if (Random.Range(0, 2) == 0)
            {
                Instantiate(ballToSpawn, spawnPos + Vector3.up * 1.2f, Quaternion.Euler(-90, 0, 0));
            }

            else if (!corner && Random.Range(0, 2) == 0 && bombtilecount <= 0)
            {

                var ox = Instantiate(bombToSpawn, spawnPos + Vector3.up, Quaternion.Euler(0, 90, 0));
                ox.transform.localPosition += Vector3.right * (Random.Range(0, 2) == 1 ? -1 : 1);

            }
            if (bombtilecount > 0)
            {
                bombtilecount--;
            }

            if (corner && Random.Range(0, 2) == 0)
            {
                var xo = Instantiate(bushToSpawn, spawnPos + Vector3.right * 3 + Vector3.down*.5f , Quaternion.Euler(0, 0, 0));
                xo.transform.localPosition += Vector3.right * (Random.Range(0, 2) == 1 ? -1 : 1);
            }

            prevTile = Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
            previousTilePosition = spawnPos;

        }
    }

    public void playstile(GameObject g)
    {
        Vector3[] dirs = { Vector3.forward, Vector3.left, Vector3.right };


        var prev = direction;

        if (counter >= 3)
        {

            direction = dirs[Random.Range(0, dirs.Length)];

            while (direction == prev * -1)
            {

                direction = dirs[Random.Range(0, dirs.Length)];

            }
            counter = 0;
        }
        else
            direction = prev;

        Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;


        counter++;

        startTime = Time.time;
        g.transform.position = spawnPos;
        g.SetActive(true);
        //   Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
        previousTilePosition = spawnPos;

    }
    // Update is called once per frame
    void Update()
    {



    }



}



