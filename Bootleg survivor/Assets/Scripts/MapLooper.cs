using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    public GameObject Camera;
    public GameObject MapPrefab;
    public GameObject Background;

    private GameObject centreMapTile;
    private GameObject topMapTile;
    private GameObject rightMapTile;
    private GameObject bottomMapTile;
    private GameObject leftMapTile;
    private GameObject[] mapCorners;

    private float tileWidth;
    private float tileHeight;
    private float topBorderPos;
    private float rightBorderPos;
    private float bottomBorderPos;
    private float leftBorderPos;

    private enum Direction{
        up,
        right,
        down,
        left
    }

    private enum PartOfMap{
        topRight,
        bottomRight,
        bottomLeft,
        topLeft
    }

    // Start is called before the first frame update
    void Start()
    {
        tileWidth = Background.transform.localScale.x;
        tileHeight = Background.transform.localScale.y;

        topBorderPos = tileHeight/2;
        rightBorderPos = tileWidth/2;
        bottomBorderPos = -tileHeight/2;
        leftBorderPos = -tileWidth/2;

        centreMapTile = Instantiate(MapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        topMapTile = Instantiate(MapPrefab, new Vector3(0, tileHeight, 0), Quaternion.identity);
        rightMapTile = Instantiate(MapPrefab, new Vector3(tileWidth, 0f, 0), Quaternion.identity);
        bottomMapTile = Instantiate(MapPrefab, new Vector3(0, -tileHeight, 0), Quaternion.identity);
        leftMapTile = Instantiate(MapPrefab, new Vector3(-tileWidth, 0, 0), Quaternion.identity);

        mapCorners = new GameObject[4];
        mapCorners[0] = Instantiate(MapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        mapCorners[1] = Instantiate(MapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        mapCorners[2] = Instantiate(MapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        mapCorners[3] = Instantiate(MapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        UpdateMapCorners();
    }

    private void FixedUpdate(){

        if(Camera.transform.position.x > rightBorderPos){//Go past right border
            leftBorderPos = rightBorderPos;
            rightBorderPos += tileWidth;
            UpdateMapCorners();

            MoveTile(topMapTile, Direction.right);
            MoveTile(bottomMapTile, Direction.right);
            MoveTile(leftMapTile, Direction.right, 3);

            GameObject oldLeft = leftMapTile;
            leftMapTile = centreMapTile;
            GameObject oldRight = rightMapTile;
            rightMapTile = oldLeft;
            centreMapTile = oldRight;
        }else if(Camera.transform.position.x < leftBorderPos){//Go past left border
            rightBorderPos = leftBorderPos;
            leftBorderPos -= tileWidth;
            UpdateMapCorners();

            MoveTile(topMapTile, Direction.left);
            MoveTile(bottomMapTile, Direction.left);
            MoveTile(rightMapTile, Direction.left, 3);

            GameObject oldRight = rightMapTile;
            rightMapTile = centreMapTile;
            rightMapTile.name = "Centre map tile";
            GameObject oldLeft = leftMapTile;
            leftMapTile = oldRight;
            leftMapTile.name = "Right map tile";
            centreMapTile = oldLeft;
            centreMapTile.name = "Left map tile";
        }else if(Camera.transform.position.y > topBorderPos){//Go past top border
            bottomBorderPos = topBorderPos;
            topBorderPos += tileHeight;
            UpdateMapCorners();

            MoveTile(leftMapTile, Direction.up);
            MoveTile(rightMapTile, Direction.up);
            MoveTile(bottomMapTile, Direction.up, 3);

            GameObject oldBottom = bottomMapTile;
            bottomMapTile = centreMapTile;
            GameObject oldTop = topMapTile;
            topMapTile = oldBottom;
            centreMapTile = oldTop;
        }else if(Camera.transform.position.y < bottomBorderPos){//Go past bottom border
            topBorderPos = bottomBorderPos;
            bottomBorderPos -= tileHeight;
            UpdateMapCorners();

            MoveTile(leftMapTile, Direction.down);
            MoveTile(rightMapTile, Direction.down);
            MoveTile(topMapTile, Direction.down, 3);

            GameObject oldTop = topMapTile;
            topMapTile = centreMapTile;
            GameObject oldBottom = bottomMapTile;
            bottomMapTile = oldTop;
            centreMapTile = oldBottom;
        }
    }

    private void UpdateMapCorners(){
        mapCorners[0].transform.position = new Vector3(rightBorderPos+tileWidth/2, topBorderPos+tileHeight/2, 0);
        mapCorners[1].transform.position = new Vector3(rightBorderPos+tileWidth/2, bottomBorderPos-tileHeight/2, 0);
        mapCorners[2].transform.position = new Vector3(leftBorderPos-tileWidth/2, bottomBorderPos-tileHeight/2, 0);
        mapCorners[3].transform.position = new Vector3(leftBorderPos-tileWidth/2, topBorderPos+tileHeight/2, 0);
    }

    private Vector3 AdjustVector(Vector3 vector, float x, float y, float z){
        return new Vector3(vector.x+x, vector.y+y, vector.z+z);
    }

    private void AdjustPosition(GameObject go, float x, float y, float z){
        go.transform.position = AdjustVector(go.transform.position, x, y, z);
    }

    private void MoveTile(GameObject tile, Direction direction, float totalSteps=1){
        switch(direction){
            case Direction.up:
                AdjustPosition(tile, 0, tileHeight*totalSteps, 0);
                break;
            case Direction.right:
                AdjustPosition(tile, tileWidth*totalSteps, 0, 0);
                break;
            case Direction.down:
                AdjustPosition(tile, 0, -tileHeight*totalSteps, 0);
                break;
            case Direction.left:
                AdjustPosition(tile, -tileWidth*totalSteps, 0, 0);
                break;
        }
    }
}
