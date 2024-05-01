using System;
using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public interface ITerrariumProduct
{
    public TraitData Traits { get; set; }
    public GameObject SelfObject { get; }

    public bool IsBaby {  get; set; }
    public bool IsDead { get; set; }

    public int CurrentAge { get; set; }
    public int AgeVariable { get; set; }

    public int CurrentDecay { get; set; }

    public void Initialise();

    public void Age();

    public void Lifecycle();
}
