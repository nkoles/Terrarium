using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LerpingUtils
{
    static public class LerpTools    {
        static public IEnumerator LerpToPosition(Transform objectTransform, Vector3 targetPosition, float speed = 10)
        {
            float lerp = 0;

            while (lerp < 1)
            {
                lerp += Time.deltaTime * speed;

                objectTransform.position = Vector3.Lerp(objectTransform.position, targetPosition, lerp);

                yield return null;
            }
        }

        static public IEnumerator LerpTransform(Transform objectTransform, Vector3 transformFieldValue, int transformFieldID = 0, float speed = 10)
        {
            float lerp = 0;

            while(lerp < 1)
            {
                lerp += Time.deltaTime * speed;

                switch (transformFieldID)
                {
                    case 0:
                        objectTransform.position = Vector3.Lerp(objectTransform.position, transformFieldValue, lerp);
                        break;
                    case 1:
                        objectTransform.rotation = Quaternion.Lerp(Quaternion.EulerRotation(objectTransform.rotation.eulerAngles), Quaternion.EulerRotation(transformFieldValue), lerp);
                        break;
                    case 2:
                        objectTransform.localScale = Vector3.Lerp(objectTransform.localScale, transformFieldValue, lerp);
                        break;
                }

                yield return null;
            }
        }
    }
}
