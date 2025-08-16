using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{

    public float speed;
    private Player player;

    public int playerID;

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    void Update()
    {

        float x = player.GetAxis("UIHorizontal");
        float y = player.GetAxis("UIVertical");

        transform.position += new Vector3(x, y, 0) * Time.deltaTime * speed;

        //Vector3 worldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -worldSize.x, worldSize.x),
        //    Mathf.Clamp(transform.position.y, -worldSize.y, worldSize.y),
        //    transform.position.z);


    }

    public void MoveMouse(Vector2 obj)
    {
        transform.position += new Vector3(obj.x, obj.y, 0) * Time.deltaTime * speed;
    }
}
