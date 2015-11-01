using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Contract
{
    public interface IInputListener
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_EDITOR
        void StartHold();
        void ReleaseHold();
        void ChangeAngle(float angleDelta);
#endif
#if UNITY_ANDROID || UNITY_IOS
        void StartTouch(Touch position);
        void ReleaseTouch(Touch position);
        void ChangeMove(Vector2 position);
#endif
    }
}