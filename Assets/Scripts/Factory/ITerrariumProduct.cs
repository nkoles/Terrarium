using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITerrariumProduct
{
    public int TraitData { get; set; }

    public void Initialise();

    public void Age();

    public void Lifecycle();
}
