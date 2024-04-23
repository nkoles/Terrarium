using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LerpingUtils
{
    static public class LerpTools    {
        static public IEnumerator LerpToPosition(Vector3 objectPosition, Vector3 targetPosition, float speed = 10)
        {
            while (objectPosition != targetPosition)
            {
                objectPosition = Vector3.Lerp(objectPosition, targetPosition, speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}
