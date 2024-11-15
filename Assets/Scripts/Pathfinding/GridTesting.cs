using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridTesting : MonoBehaviour
{
    private Grid<HeatMapGridObject> grid;

    //making a grid
    void Start()
    {
        //after vector3.zero we have func method signiture that passes it all to our heatmap constructor
        grid = new Grid<HeatMapGridObject>(20, 10, 1f, Vector3.zero, (Grid<HeatMapGridObject>g, int x, int y) => new HeatMapGridObject(g, x ,y));
    }

    private void Update()
    {
        //set given value
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);
            if (heatMapGridObject != null) heatMapGridObject.AddValue(5);
        }
        // get value
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetGridObject(UtilsClass.GetMouseWorldPosition()));
        }
    }

    public class HeatMapGridObject {

        private const int MIN = 0;
        private const int MAX = 100;

        private Grid<HeatMapGridObject> grid;
        public int value;
        private int x;
        private int y;
        
        public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void AddValue(int addValue){
            value += addValue;
            value = Mathf.Clamp(value, MIN, MAX);
            grid.TriggerGameObjectChanged(x, y);
        }
        public float GetValueNormalized() { 
            return (float)value /MAX;
        }
        public override string ToString(){
            return value.ToString();
        }


        }
}
