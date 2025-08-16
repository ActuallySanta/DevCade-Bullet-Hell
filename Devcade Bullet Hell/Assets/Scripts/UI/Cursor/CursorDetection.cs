using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Rewired;

public class CursorDetection : MonoBehaviour
{
    private GraphicRaycaster gr;
    private PointerEventData pointerEventData = new PointerEventData(null);

    private Player player;
    public int playerID;

    void Start()
    {
        gr = GetComponentInParent<GraphicRaycaster>();
        player = ReInput.players.GetPlayer(playerID);
    }

    void Update()
    {
        //Get the currently hovered information of the cursor
        pointerEventData.position = Camera.main.WorldToScreenPoint(transform.position);
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);

        //If the cursor is hovering over anything
        if (results.Count > 0)
        {
            //Get the first thing the cursor is hovering over
            Transform raycastCharacter = results[0].gameObject.transform;

            Debug.Log(raycastCharacter);

            Button selectedButton = raycastCharacter.GetComponent<Button>();

            if (selectedButton != null)
            {
                if (player.GetButtonDown("UISubmit")) selectedButton.onClick.Invoke();
            }
        }
    }
}
