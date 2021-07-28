using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMoonLigthUser
{
    event Action OnMoonLigthUsed;
    void OnMoonLigthEnter();
    void OnMoonLigthExit();
}
