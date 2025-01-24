using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable<T>
{
    public void Interact();
    public void Interact(T data);
    public void Abort();
    public string Identify();
}
