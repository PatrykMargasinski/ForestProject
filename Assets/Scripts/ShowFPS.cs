 using UnityEngine;
 using System.Collections;
 using UnityEngine.UI;
 
 public class ShowFPS : MonoBehaviour //skrypt obliczający ile klatek zostało wygenerowanych w ciągu sekundy
 {
     private Text fpsText;
     private float deltaTime;

     void Start()
     {
         fpsText=GetComponent<Text>();
     }
 
     void Update () 
     {
         deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
         float fps = 1.0f / deltaTime;
         fpsText.text = Mathf.Ceil (fps).ToString ();
     }
 }