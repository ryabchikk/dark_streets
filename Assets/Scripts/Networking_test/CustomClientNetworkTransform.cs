using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;

namespace Unity.Multiplayer.Samples.Utilities.CustomClientAuthority{

[DisallowMultipleComponent]
public class CustomClientNetworkTransform : NetworkTransform
{
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }

}