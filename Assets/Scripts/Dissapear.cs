using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour
{
   MeshRenderer[] rendererComponents;

        void Start() {
            rendererComponents  = GetComponentsInChildren<MeshRenderer>();
        }

        public float dissapearTime = 3;
        public float currentTime = 0;

        void Update() {
            foreach (MeshRenderer renderer in rendererComponents) {
                currentTime += Time.deltaTime;
                Color oldCol = renderer.material.color;
                Color newCol = new Color(oldCol.r, oldCol.g, oldCol.b, (dissapearTime - currentTime)/dissapearTime);
                renderer.material.color = newCol;
                if (currentTime >= dissapearTime) Destroy(gameObject);
            }
        }
}
