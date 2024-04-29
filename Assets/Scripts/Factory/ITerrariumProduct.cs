using System.Collections;
using System.Collections.Generic;
using TerrariumTraits;
using UnityEngine;

public interface ITerrariumProduct
{
    public TraitData Traits { get; set; }

    public bool IsDead { get; set; }

    public Vector3 PositionalData { get; set; }

    public void Initialise();

    public void Age();

    public void Lifecycle();
}
