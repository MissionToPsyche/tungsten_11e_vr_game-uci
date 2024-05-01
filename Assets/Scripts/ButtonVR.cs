using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            var oldPos = button.transform.localPosition;
            button.transform.localPosition = oldPos + new Vector3(0, -0.007f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser) {
            var oldPos = button.transform.localPosition;
            button.transform.localPosition = oldPos + new Vector3(0, 0.007f, 0);
            onRelease.Invoke(); 
            isPressed=false;
        }
    }
}
