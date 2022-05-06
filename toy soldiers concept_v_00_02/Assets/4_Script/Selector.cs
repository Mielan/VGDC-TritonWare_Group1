using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private int soldierIndex;
    private bool turn = false;
    
    public List<SoldierScript> soldiersTeamA = new List<SoldierScript>();

    public List<SoldierScript> soldiersTeamB = new List<SoldierScript>();

    public List<SoldierScript> soldiers;


    private int toFireCount = 0;
    
    private List<SoldierScript> toFire;

    [SerializeField]
    private int maxCanFire = 3;
    
    void Awake()
    {
        foreach (SoldierScript soldier in soldiersTeamA) 
        {
            soldier.selector = this;
        }
        foreach (SoldierScript soldier in soldiersTeamB)
        {
            soldier.selector = this;
        }
        soldiers = soldiersTeamA;
        soldiers[0].selected = true;

    }

    //void selectIndex(int index)
    //{
    //    if (soldierIndex < soldiers.Length - 1 && soldierIndex > 0)
    //    {
    //        soldiers[soldierIndex].selected = false;
    //        soldierIndex = index;
    //        soldiers[soldierIndex].selected = true;
    //    }
    //    // else exception
    //}

    public void AddToFire(SoldierScript soldier) 
    {
        if (toFireCount < maxCanFire)
        {
            toFireCount++;
        }
        else 
        {
            toFire[0].gun.setAim = false;
            toFire.RemoveAt(0);
        }
        //toFire.Add(soldier);

    }

    public void RemoveToFire(SoldierScript soldier)
    {
        if (toFireCount > 0)
        {
            toFire.Remove(soldier);
            toFireCount--;
        }
        
    }

    void selectUp() 
    {
        if (soldierIndex < soldiers.Count-1)
        {
            soldiers[soldierIndex].selected = false;
            soldierIndex++;
            soldiers[soldierIndex].selected = true;
        }
    }

    void selectDown()
    {
        if (soldierIndex > 0)
        {
            soldiers[soldierIndex].selected = false;
            soldierIndex--;
            soldiers[soldierIndex].selected = true;
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("selectUp"))
        {
            selectUp();
        }
        else if (Input.GetButtonDown("selectDown")) 
        {
            selectDown();
        }

        if (Input.GetButtonDown("Jump")) 
        {
            turn = !turn;
            soldiers[soldierIndex].selected = false;
            if (turn)
            {
                print("B");
                soldiers = soldiersTeamB;
            }
            else 
            {
                print("A");

                soldiers = soldiersTeamA;
            }
            soldierIndex = 0;
            soldiers[soldierIndex].selected = true;
        }
    }
}
