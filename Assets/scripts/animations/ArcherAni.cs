using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ArcherAni : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, jump, dash;
    public string currState;

    private void Start()
    {
        currState = "idle";
        SetCharacterState(currState);
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timescale;
    }

    public void SetCharacterState(string state)
    {
        if (state == "jump")
        {
            SetAnimation(jump, false, 1);
        }
        else if (state == "dash")
        {
            SetAnimation(dash, false, 2f);
        }
        else if (state == "idle")
        {
            SetAnimation(idle, true, 1);
        }
    }
}
