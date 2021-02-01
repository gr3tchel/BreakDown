using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingWallsManager : MonoBehaviour
{

    public GameObject[] Walls;
    public GameObject[] RedDoors;
    public GameObject[] GreenCubes;
    public GameObject[] TriggerJump;
    public GameObject[] White;
    public Key[] MyKeys;
    public Material[] MyMat;
    public KeysManager myKeysManager;

    public float MySpeed = 1.0f;

    public Wall[] movingWalls;
    public Wall[] movingRedDoors;
    public Wall[] movingGreenCubes;
    public Wall[] movingTriggerJump;
    public Wall[] movingWhite;

    void Start()
    {
        movingWalls = new Wall[Walls.Length];
        movingRedDoors = new Wall[RedDoors.Length];
        movingGreenCubes = new Wall[GreenCubes.Length];
        movingTriggerJump = new Wall[TriggerJump.Length];
        movingWhite = new Wall[White.Length];

        SetMovingElement(Walls, movingWalls);
        SetMovingElement(RedDoors, movingRedDoors);
        SetMovingElement(GreenCubes, movingGreenCubes);
        SetMovingElement(TriggerJump, movingTriggerJump);
        SetMovingElement(White, movingWhite);
    }

    void Update()
    {
        MyKeys = myKeysManager.Keys;

        /*    if (Input.GetKeyDown(KeyCode.R))
            {
                ClosePrevOpen();
                foreach (Wall w in movingRedDoors)
                {
                    SimpleWallAnim(movingRedDoors, w, -MySpeed);
                }
                //PlayWallAnimation(movingRedDoors, 0, -MySpeed);
            }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ClosePrevOpen();
            foreach (Wall w in movingWalls)
            {
                SimpleWallAnim(movingWalls, w, -MySpeed);
            }
            //PlayWallAnimation(movingRedDoors, 0, MySpeed);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            ClosePrevOpen();
            foreach (Wall w in movingGreenCubes)
            {
                SimpleWallAnim(movingGreenCubes, w, -MySpeed);
            }
            //PlayWallAnimation(movingRedDoors, 0, MySpeed);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ClosePrevOpen();
            foreach (Wall w in movingTriggerJump)
            {
                SimpleWallAnim(movingTriggerJump, w, -MySpeed);
            }
            //PlayWallAnimation(movingRedDoors, 0, MySpeed);
        }
        */

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    ClosePrevOpen();
        //    PlayWallAnimation(movingRedDoors, 1, -MySpeed);
        //}

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    ClosePrevOpen();
        //    PlayWallAnimation(movingRedDoors, 1, MySpeed);
        //}
    }

    void ClosePrevOpen()
    {
        Wall mywall = new Wall();

        Wall[] iWalls = Array.FindAll(movingWalls, p => p.Status == "Open");
        Wall[] iRedWalls = Array.FindAll(movingRedDoors, p => p.Status == "Open");
        Wall[] iGreenCubes = Array.FindAll(movingGreenCubes, p => p.Status == "Open");
        Wall[] iTriggerJump = Array.FindAll(movingTriggerJump, p => p.Status == "Open");
        Wall[] iWhite = Array.FindAll(movingWhite, p => p.Status == "Open");

        if (iWalls.Length > 0)
        {
            foreach (Wall mw in iWalls)
            {
                mywall = mw;
                movingWalls[mw.Index].Status = "Close";
                mywall.Status = "Close";
                SimpleWallAnim(iWalls, mywall, MySpeed);
            }
        }

        if (iRedWalls.Length > 0)
        {
            foreach (Wall rw in iRedWalls)
            {
                mywall = rw;
                movingRedDoors[rw.Index].Status = "Close";
                mywall.Status = "Close";
                SimpleWallAnim(iRedWalls, mywall, MySpeed);
            }
        }
        if (iGreenCubes.Length > 0)
        {
            foreach (Wall gc in iGreenCubes)
            {
                mywall = gc;
                movingGreenCubes[gc.Index].Status = "Close";
                mywall.Status = "Close";
                SimpleWallAnim(iGreenCubes, mywall, MySpeed);
            }
        }
        if (iTriggerJump.Length > 0)
        {
            foreach (Wall tj in iTriggerJump)
            {
                mywall = tj;
                movingTriggerJump[tj.Index].Status = "Close";
                mywall.Status = "Close";
                SimpleWallAnim(iTriggerJump, mywall, MySpeed);
            }
        }
        if (iWhite.Length > 0)
        {
            foreach (Wall wh in iWhite)
            {
                mywall = wh;
                movingWhite[wh.Index].Status = "Close";
                mywall.Status = "Close";
                SimpleWallAnim(iWhite, mywall, MySpeed);
            }
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        ClosePrevOpen();
        int index = Array.FindIndex(Walls, p => other.CompareTag(p.name));
        PlayWallAnimation(movingRedDoors, index, MySpeed);
    }*/

    
     void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.R)&&other.tag== "RedLock" && MyKeys[1].Owned == true)
        {
            //red
            MyMat[0].color = new Color32 (195,56,56,255);
            //viola
            MyMat[1].color = new Color32(87, 56, 156,255);
            //blu
            MyMat[3].color = new Color32(33, 60, 85,255);
            //pink
            MyMat[2].color = new Color32(125, 50, 117,255);
            //green
            MyMat[4].color = new Color32(48, 126, 43,255);
            ClosePrevOpen();
            foreach (Wall w in movingRedDoors)
            {
                SimpleWallAnim(movingRedDoors, w, -MySpeed);
            }
            //PlayWallAnimation(movingRedDoors, 0, -MySpeed);
        }

            if (Input.GetKeyDown(KeyCode.G) && other.tag == "GreenLock" && MyKeys[2].Owned == true)
            {
            //red
                 MyMat[0].color = new Color32(128, 43, 43,255);
            //viola
            MyMat[1].color = new Color32(87, 56, 156, 255);
            //blu
            MyMat[3].color = new Color32(33, 60, 85, 255);
            //pink
            MyMat[2].color = new Color32(125, 50, 117, 255);
            //green
            MyMat[4].color = new Color32(99, 255, 90, 255);
            ClosePrevOpen();
                foreach (Wall w in movingGreenCubes)
                {
                    SimpleWallAnim(movingGreenCubes, w, -MySpeed);
                }
                //PlayWallAnimation(movingRedDoors, 0, -MySpeed);
            }

        if (Input.GetKeyDown(KeyCode.B) && other.tag == "BlueLock" && MyKeys[3].Owned == true)
        {
            //red
            MyMat[0].color = new Color32(128, 43, 43, 255);
            //viola
            MyMat[1].color = new Color32(87, 56, 156, 255);
            //blu
            MyMat[3].color = new Color32(52, 97, 140, 255);
            //pink
            MyMat[2].color = new Color32(125, 50, 117, 255);
            //green
            MyMat[4].color = new Color32(48, 126, 43, 255);
            ClosePrevOpen();
            foreach (Wall w in movingWalls)
            {
                SimpleWallAnim(movingWalls, w, -MySpeed);
            }
            //PlayWallAnimation(movingRedDoors, 0, MySpeed);
        }

        if (Input.GetKeyDown(KeyCode.P) && other.tag == "PinkLock" && MyKeys[4].Owned == true)
        {
            //red
            MyMat[0].color = new Color32(128, 43, 43, 255);
            //viola
            MyMat[1].color = new Color32(87, 56, 156, 255);
            //blu
            MyMat[3].color = new Color32(33, 60, 85, 255);
            //pink
            MyMat[2].color = new Color32(185, 71, 172, 255);
            //green
            MyMat[4].color = new Color32(48, 126, 43, 255);
            ClosePrevOpen();
            foreach (Wall w in movingTriggerJump)
            {
                SimpleWallAnim(movingTriggerJump, w, -MySpeed);
            }
            //PlayWallAnimation(movingRedDoors, 0, MySpeed);
        }

    }


    void PlayWallAnimation(Wall[] movingElements, int animIndex, float speed)
    {
        if (speed == 0)
            movingElements[animIndex].Status = "None";
        if (speed < 0)
            movingElements[animIndex].Status = "Open";
        if (speed > 0)
            movingElements[animIndex].Status = "Close";

        movingElements[animIndex].DoorAnimation.enabled = true;

        AnimatorClipInfo[] temps = movingElements[animIndex].DoorAnimation.GetCurrentAnimatorClipInfo(0);
        AnimatorClipInfo clipInfo = new AnimatorClipInfo();
        if (temps.Length > 0)
        {
            clipInfo = temps[0];
        }

        movingElements[animIndex].DoorAnimation.StartPlayback();

        movingElements[animIndex].DoorAnimation.speed = speed;

        movingElements[animIndex].DoorAnimation.Play(clipInfo.clip.name, 0, speed < 0 ? 1 : 0);
    }

    void SimpleWallAnim(Wall[] movingElements, Wall wall, float speed)
    {
        if (speed == 0)
            movingElements[wall.Index].Status = "None";
        if (speed < 0)
            movingElements[wall.Index].Status = "Open";
        if (speed > 0)
            movingElements[wall.Index].Status = "Close";

        wall.DoorAnimation.enabled = true;

        AnimatorClipInfo[] temps = wall.DoorAnimation.GetCurrentAnimatorClipInfo(0);
        AnimatorClipInfo clipInfo = new AnimatorClipInfo();
        if (temps.Length > 0)
        {
            clipInfo = temps[0];
        }

        wall.DoorAnimation.StartPlayback();

        wall.DoorAnimation.speed = speed;

        wall.DoorAnimation.Play(clipInfo.clip.name, 0, speed < 0 ? 1 : 0);
    }

    void SetMovingElement(GameObject[] srcElements, Wall[] movingElements)
    {
        for (int i = 0; i < srcElements.Length; i++)
        {
            movingElements[i] = new Wall()
            {
                Index = i,
                Name = srcElements[i].name,
                Tag = srcElements[i].tag,
                WallColor = srcElements[i].transform.GetChild(0).GetComponent<MeshRenderer>().material.color,
                DoorAnimation = srcElements[i].transform.GetComponent<Animator>(),
                TheWall = srcElements[i],
                Status = "None"
            };

            PlayWallAnimation(movingElements, i, 0);
        }
    }
}

[Serializable]
public struct Wall
{
    public int Index;
    public string Name;
    public string Tag;
    public GameObject TheWall;
    public Color WallColor;
    public Animator DoorAnimation;
    public string Status;
}
