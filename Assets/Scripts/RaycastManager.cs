using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastManager : MonoBehaviour
{
    private bool panelOpen = false;
    public List<GameObject> list = new List<GameObject>();
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (panelOpen)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].activeSelf)
                    {
                        list[i].SetActive(false);
                    }
                }
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                MonoBehaviour targetScript = hit.collider.GetComponentInParent<MonoBehaviour>();

                if (targetScript != null)
                {
                    if (targetScript is IClickable clickable)
                    {
                        clickable.OnClick();
                        panelOpen = true;
                    }
                }

            }
        }
    }
}
