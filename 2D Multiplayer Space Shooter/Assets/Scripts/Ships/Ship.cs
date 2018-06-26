//Abstract class to define what a generic ship should implement


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour
{

    private int _id;

    [SerializeField] //field right under the declaration will be visible in unity
    private float _maxSpeed; //speed we can reach in 1 sec
    private float _rotSpeed; //speed we can turn in 1 sec
    private float _shipBoundaryRadius; //radius of the ship to not go over the screen limits



    public int ID
    {
        get { return this._id; }
        set { this._id = value; }
    }

    public int getID()
    {
        return ID;
    }

    public float MaxSpeed
    {
        get { return this._maxSpeed; }
        set { this._maxSpeed = value; }
    }

    public float RotSpeed
    {
        get { return this._rotSpeed; }
        set { this._rotSpeed = value; }
    }

    public float ShipBoundaryRadius
    {
        get { return this._shipBoundaryRadius; }
        set { this._shipBoundaryRadius = value; }
    }


    // Use this for initialization
    protected abstract void Start();

    // Update is called once per frame
    //where you check player input
    //it runs 30 to 60 fps
    protected abstract void Update();

    protected abstract void Movement();
    public abstract void Movement(float h, float v);

    public abstract void ResetHealth();

    //implemented here because the algorythm must be equal to all ships.
    protected Vector3 BoundariesRestrictions(Vector3 pos)
    {
        if (pos.y + _shipBoundaryRadius > Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - _shipBoundaryRadius;
        }

        else if (pos.y - _shipBoundaryRadius < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + _shipBoundaryRadius;
        }

        //for x axis we need the ratio

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        if (pos.x + _shipBoundaryRadius > widthOrtho)
        {
            pos.x = widthOrtho - _shipBoundaryRadius;
        }
        else if (pos.x - _shipBoundaryRadius < -widthOrtho)
        {
            pos.x = -widthOrtho + _shipBoundaryRadius;
        }

        return pos;
    }



}
