using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLaser : Model
{
    private void Start()
    {
        controller = controller.Clone();
        controller.SetController();
        (controller as IController).SetModel(this);
    }

    public IEnumerator Disabler(float f)
    {
        yield return new WaitForSeconds(f);
        gameObject.SetActive(false);
    }
}