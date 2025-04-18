using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static int swap1;
    public static int swap2;
    private int numCorrect = 0;
    [SerializeField] GameObject[] tiles = new GameObject[25];
    Vector3[] gridCoord = new Vector3[25];
    int[] gridPos = {10, 7, 24, 14, 20, 1, 21, 0, 5, 8, 2, 3, 11, 12, 16, 23, 15, 13, 19, 9, 4, 17, 18, 22, 6};
    public GameObject instructionsPopUp;
    public GameObject EndUI;
    public KeyCode toggleKey = KeyCode.I;
    public bool win = false;
    public AudioClip swapSFX;
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game 2 has now started");
        EndUI.SetActive(false);

        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                gridCoord[5*i + j].x = -4 + 2*j;
                gridCoord[5*i + j].y = 0;
                gridCoord[5*i + j].z = 4 - 2*i;
            }
        }

        for(int i = 0; i < 25; i++)
        {
            tiles[i].transform.position = gridCoord[gridPos[i]];
        }
    }

    void SwapTiles(int tile1, int tile2)
    {
        tiles[tile1].transform.position = gridCoord[gridPos[tile2]];
        tiles[tile2].transform.position = gridCoord[gridPos[tile1]];

        int temp = gridPos[tile1];
        gridPos[tile1] = gridPos[tile2];
        gridPos[tile2] = temp;

        tiles[tile1].GetComponent<Tile>().Deselect();
        tiles[tile2].GetComponent<Tile>().Deselect();

        if(tile1 == gridPos[tile1])
        {
            tiles[tile1].GetComponent<Tile>().Lock();
            numCorrect++;
            //correct swap sfx
            audioSource.PlayOneShot(swapSFX);
        }
        if(tile2 == gridPos[tile2])
        {
            tiles[tile2].GetComponent<Tile>().Lock();
            numCorrect++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //logic for turning instructions on and off with 'I' key
        if(Input.GetKeyDown(toggleKey))
        {
            instructionsPopUp.SetActive(!instructionsPopUp.activeSelf);
        }

        if(Tile.numSelected == 2)
        {
            SwapTiles(swap1, swap2);
            Tile.numSelected = 0;
        }
        if(numCorrect >= 25 && !win)
        {
            win = true;
            Debug.Log("You win!");
            EndUI.SetActive(true);
        }
    }
    
}