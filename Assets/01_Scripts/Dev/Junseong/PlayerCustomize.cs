using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCustomize
{
    public event Action OnChanged = null;

    private Sprite headSprite;
    private Sprite bodySprite;
    private Sprite accessori;
    
    public Sprite CurrentHeadSprite
    {
        get => headSprite;
        set
        {
            if (headSprite != value)
            {
                headSprite = value;
                OnChanged?.Invoke();
            }
        }
    }
    public Sprite CurrentBodySprite
    {
        get => bodySprite;
        set
        {
            if (bodySprite != value)
            {
                bodySprite = value;
                OnChanged?.Invoke();
            }
        }
    }
    public Sprite CurrentAccessariSprite
    {
        get => accessori;
        set
        {
            if (accessori != value)
            {
                accessori = value;
                OnChanged?.Invoke();
            }
        }
    }
}
