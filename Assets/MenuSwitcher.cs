using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public List<GameObject> menus;
    public GameObject currentMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        if (currentMenu) currentMenu.gameObject.SetActive(true);
    }

    public void setMenu(int i) 
    {
        if (currentMenu) currentMenu.gameObject.SetActive(false);
        currentMenu = menus[i];
        currentMenu.SetActive(true);
    }
}
