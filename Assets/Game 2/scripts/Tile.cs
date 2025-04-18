using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    MeshRenderer mesh;
    [SerializeField] int tileNum;
    private bool selected = false;
    private bool locked = false;
    public static int numSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void Select()
    {
        if(!locked && numSelected < 2)
        {
            if(numSelected == 0)
            {
                GameManager2.swap1 = tileNum;
            }
            if(numSelected == 1)
            {
                GameManager2.swap2 = tileNum;
            }

            selected = true;
            numSelected++;
            mesh.material.color = Color.blue;
        }
    }

    public void Deselect()
    {
        if(!locked && numSelected != 0)
        {
            selected = false;
            numSelected--;
            mesh.material.color = Color.white;
        }
    }

    public void Lock()
    {
        locked = true;
        //mesh.material.color = Color.yellow;
        mesh.material.SetInt("_Border", 0);
    }

    void OnMouseOver()
    {
        if(!locked)
        {
            if(!selected && numSelected < 2)
            {
                mesh.material.color = Color.gray;
            }
            if(Input.GetMouseButtonDown(0))
            {
                if(!selected)
                {
                    Select();
                }
                else
                {
                    Deselect();
                }
            }
        }
    }

    void OnMouseExit()
    {
        if(!locked && !selected)
        {
            mesh.material.color = Color.white;
        }
    }
}
