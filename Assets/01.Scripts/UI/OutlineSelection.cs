using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    //커서
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D cursorTextureEnemy;

    private Vector2 cursorHotspot;

    //아웃라인
    [SerializeField] private Color color;
    [SerializeField] private float size;

    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private void Awake()
    {
        cursorHotspot = new Vector2(cursorTexture.width/1.5f, cursorTexture.height/1.5f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    //void Update()
    //{
    //    // Highlight
    //    if (highlight != null)
    //    {
    //        highlight.gameObject.GetComponent<Outline>().enabled = false;
    //        highlight = null;
    //    }
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
    //    {
    //        highlight = raycastHit.transform;
    //        if (highlight.CompareTag("Enemy") && highlight != selection)
    //        {
    //            Cursor.SetCursor(cursorTextureEnemy, cursorHotspot, CursorMode.Auto);
    //            if (highlight.gameObject.GetComponent<Outline>() != null)
    //            {
    //                highlight.gameObject.GetComponent<Outline>().enabled = true;
    //            }
    //            else
    //            {
    //                Outline outline = highlight.gameObject.AddComponent<Outline>();
    //                outline.enabled = true;
    //                highlight.gameObject.GetComponent<Outline>().OutlineColor = color;
    //                highlight.gameObject.GetComponent<Outline>().OutlineWidth = size;
    //            }
    //        }
    //        else
    //        {
    //            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    //            highlight = null;
    //        }
    //    }

    //    // Selection
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (highlight)
    //        {
    //            if (selection != null)
    //            {
    //                selection.gameObject.GetComponent<Outline>().enabled = false;
    //            }
    //            selection = raycastHit.transform;
    //            selection.gameObject.GetComponent<Outline>().enabled = true;
    //            highlight = null;
    //        }
    //        else
    //        {
    //            if (selection)
    //            {
    //                selection.gameObject.GetComponent<Outline>().enabled = false;
    //                selection = null;
    //            }
    //        }
    //    }
    //}

}
