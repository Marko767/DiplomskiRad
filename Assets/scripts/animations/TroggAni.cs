using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TroggAni : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset erupt, stomp, die;
    public string currState;
    public GameObject trogg;

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timescale;
        animationEntry.Complete += AnimationEntry_Complete;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currState == "die")
        {
            Destroy(trogg);
        }
    }

    public void SetCharacterState(string state)
    {
        if (state == "erupt")
        {
            SetAnimation(erupt, false, 0.3f);
            currState = "erupt";
        }
        else if (state == "stomp")
        {
            SetAnimation(stomp, false, 1f);
            currState = "stomp";
        }
        else if (state == "die")
        {
            SetAnimation(die, false, 1f);
            currState = "die";
        }
    }
}
