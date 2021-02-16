using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TreeChopper : MonoBehaviour//skrypt odpowiedzialny na ścinanie drzew
{
    private TreeInstance[] treeInstances;
    public Camera camera;
    public Terrain terrain;
    public float minimalDistanceToCut=5;
    public Text text;
    public Image aim;

    //robiona jest kopia wszystkich drzew. 
    //Przed wyłączeniem aplikacji wszystkie te drzewa są przypisywane do terrainu (metoda OnApplicationQuit)
    //dzięki temu przy każdym uruchomieniu gry ilość drzew będzie identyczna
    void Start()
    {
        treeInstances=terrain.terrainData.treeInstances.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int index;
        //sprawdzane jest, czy raycast trafił na jakiś punkt. Następnie, czy odległość między kamerą a punktem jest mniejsza niż minimalny dystans.
        //Pod koniec sprawdzane jest czy zostało trafione drzewo
        if (Physics.Raycast(ray, out hit) && Vector3.Distance(camera.transform.position,hit.point)<minimalDistanceToCut && FindSuitableTree(hit.point,out index))
        {
            aim.color=Color.green;
            text.text="Press E to chop";
            if(Input.GetKeyDown(KeyCode.E))
            {
                DestroyTree(index);
            }
        }
        else
        {
            aim.color=Color.red;
            text.text="";
        }
    }

    //aby sprawdzić czy raycast trafił w drzewo, sprawdzane jest, czy jest jakieś drzewo kolidujące z punktem trafienia
    //Jeżeli nie ma, index=-1. Jeżeli jest, index to indeks drzewa, które zostało trafione
    bool FindSuitableTree(Vector3 point, out int index)
    {
        Vector2 position2d = new Vector2(point.x, point.z);
        foreach (TreeInstance tree in terrain.terrainData.treeInstances)
        {
            Vector2 treePos2d = new Vector2(tree.position.x * 50f, tree.position.z * 50f);
            //propotypeIndex 0 i 1 to indeksy prototypów drzew, które mogą być ścięte
            if (Vector2.Distance(position2d, treePos2d) <= 1.2 && (tree.prototypeIndex==0 || tree.prototypeIndex==1))
            {
                index=Array.IndexOf(terrain.terrainData.treeInstances, tree);
                return true;
            }
        }
        index= -1; return false;
    }
    void DestroyTree(int index)//niszczenie drzewa o danym indeksie
    {
        var list = new List<TreeInstance>(terrain.terrainData.treeInstances);
        list.RemoveAt(index);
        terrain.terrainData.SetTreeInstances(list.ToArray(), false);
        var collider = terrain.GetComponent<TerrainCollider>();

        //po zniszczeniu drzewa nie znika jego collider, chyba że wyłącze i włącze cały terrain collider
        collider.enabled = false;
        collider.enabled = true;
    }

    void OnApplicationQuit() 
    {
        terrain.terrainData.SetTreeInstances(treeInstances,false);
    }
}
