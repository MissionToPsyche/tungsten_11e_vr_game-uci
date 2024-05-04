using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// References https://forum.unity.com/threads/c-proper-state-machine.380612/#post-2471322
// Abstract class used to represent a frame in the tutorial.
public abstract class Frame : MonoBehaviour {
    public TutorialSystem tutorialSystem;
    // Will execute when we first enter the frame.
    public abstract void Enter();
    // Will execute when we exit the frame.
    public abstract void Exit();
    // Use this method to move to the next frame.
    public virtual void Next() { if (this.isActiveAndEnabled) this.tutorialSystem.Next(); }
}

public class TutorialSystem : MonoBehaviour
{
    public List<Frame> frames;
    private int currentIndex = -1;
    
    private void Start() {
        foreach (Frame frame in frames) {
            frame.gameObject.SetActive(false);
        }
        Next();
    }

    public void Next() {
        if (currentIndex >= 0) {
            Frame currentFrame = frames[currentIndex];
            currentFrame.Exit();
            currentFrame.gameObject.SetActive(false);
        }

        currentIndex++;

        if (currentIndex >= frames.Count) return;

        Frame nextFrame = frames[currentIndex];
        nextFrame.gameObject.SetActive(true);
        nextFrame.Enter();
    }
}
