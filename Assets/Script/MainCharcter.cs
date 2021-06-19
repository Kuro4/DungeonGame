using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MainCharcter : MonoBehaviour
{
    // Start is called before the first frame update
    const int MapWidth  = 75;  //  マップのX最大サイズ
    const int MapHeight  = 75;  //  マップの最大サイズ
    const int wall = 0; //  壁
    const int room = 1; //  部屋
    const int road = 2;　//　通路
    const int roomMinHeight = 5;
    const int roomMaxHeight = 10;

    const int roomMinWidth = 5;
    const int roomMaxWidth = 10;

    const int RoomCountMax = 50;   

    //道の集合点を増やしたいならこれを増やす
    const int meetPointCount = 1;
    
    private const int XPos = 0;
    private const int YPos = 1;
    private int[] MapPosition = new int[2];    //  主人公のマップ位置
    private int HP; //  HPとういことよ

    public Image imagePrefab;    //  画像
    private Image[,] image = new Image[MapHeight ,MapWidth];
    private GameObject MapCanvas;  //  地図用のキャンバス

    public GameObject prefab;   //  プレハブ
    private GameObject MapTileParent;   //  生成したマップを格納する親
    private GameObject[,] WallObject = new GameObject[MapHeight ,MapWidth];    //  マップ表示のオブジェクト
    private int[,] Map;
    void Start()
    {   
        ResetMapData();
        CreateSpaceData();
        CreateDangeon();
        // 主人公の初期位置設定 
        MapPosition[XPos] = 0;
        MapPosition[YPos] = 0;
        Vector3 pos = new Vector3(MapPosition[XPos]*2+1,1,MapPosition[YPos]*2+1);
        this.transform.position = pos;
        //  主人公のステータス設定
        HP = 10;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //this.transform.Translate (0.0f,0.0f,0.1f);
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0);
            //  入力により主人公のマップ位置を変更
            if(MapPosition[YPos]<MapHeight -1){
                MapPosition[YPos]++;
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            //this.transform.Translate (0.0f,0.0f,0.1f);
            this.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0);
            //  入力により主人公のマップ位置を変更
            if(MapPosition[YPos]>0){
                MapPosition[YPos]--;
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            //this.transform.Translate (0.0f,0.0f,0.1f);
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0);
            //  入力により主人公のマップ位置を変更
            if(MapPosition[XPos]<MapWidth -1){
                MapPosition[XPos]++;
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            //this.transform.Translate (0.0f,0.0f,0.1f);
            this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0);
            //  入力により主人公のマップ位置を変更
            if(MapPosition[XPos]>0){
                MapPosition[XPos]--;
            }
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            //削除
            for (int i = 0; i < MapHeight;i++){
                for (int j = 0; j < MapWidth;j++){
                    Destroy(WallObject[i, j]);
                    Destroy(image[i, j]);
                }
            }
                //  生成
            ResetMapData();
            CreateSpaceData();
            CreateDangeon();
        }
        //  移動処理（仮）
        Vector3 pos = new Vector3(MapPosition[XPos]*2+1,1,MapPosition[YPos]*2+1);
        this.transform.position = pos;
    }

    /// <summary>
    /// Mapの二次元配列の初期化
    /// </summary>
     private void ResetMapData() {
        Map = new int[MapHeight, MapWidth];
        for (int i = 0; i < MapHeight; i++) {
            for (int j = 0; j < MapWidth; j++) {
                Map[i, j] = wall;
            }
        }
    }
    /// <summary>
    /// マップデータをもとにダンジョンを生成
    /// </summary>
    private void CreateDangeon()
    {
        MapTileParent = GameObject.Find("Map"); //  マップtileの親を探して定義
        MapCanvas = GameObject.Find("MapCanvas");   //  地図用のそれ
        for (int i = 0; i < MapHeight; i++)
        {
            for (int j = 0; j < MapWidth; j++)
            {
                if (Map[i, j] == wall)
                {
                    WallObject[i, j] = Instantiate(prefab, new Vector3((i * 2) + 1, 0, (j * 2) + 1), Quaternion.identity); //　プレハブからゲームオブジェクト生成
                    WallObject[i, j].transform.parent = MapTileParent.transform;

                    //  マップ表示用の
                    image[i, j] = Instantiate(imagePrefab, new Vector3(i*10+10,10+j*10,0), Quaternion.identity);
                    image[i, j].transform.SetParent(MapCanvas.transform);
                    //  image[i,j].rectTransform

                }
                if(Map[i,j] == room){
                    image[i, j] = Instantiate(imagePrefab, new Vector3(i*10+10,10+j*10,0), Quaternion.identity);
                    image[i, j].transform.SetParent(MapCanvas.transform);
                    image[i, j].color = new Color(0.0f / 255.0f, 100.0f / 255.0f, 0.0f / 255.0f);
                }
                if(Map[i,j] == road){
                    image[i, j] = Instantiate(imagePrefab, new Vector3(i*10+10,10+j*10,0), Quaternion.identity);
                    image[i, j].transform.SetParent(MapCanvas.transform);
                    image[i, j].color = new Color(0.0f / 255.0f, 0.0f / 255.0f, 100.0f / 255.0f);
                }
            }
        }
    }
    /// <summary>
    /// 空白部分のデータを変更
    /// </summary>
    private void CreateSpaceData()
    {
        int roomCount = RoomCountMax;

        int[] meetPointsX = new int[meetPointCount];
        int[] meetPointsY = new int[meetPointCount];
        for (int i = 0; i < meetPointsX.Length; i++)
        {
            meetPointsX[i] = Random.Range(MapWidth / 4, MapWidth * 3 / 4);
            meetPointsY[i] = Random.Range(MapHeight / 4, MapHeight * 3 / 4);
            Map[meetPointsY[i], meetPointsX[i]] = road;
        }

        for (int i = 0; i < roomCount; i++)
        {
            int roomHeight = Random.Range(roomMinHeight, roomMaxHeight);
            int roomWidth = Random.Range(roomMinWidth, roomMaxWidth);
            int roomPointX = Random.Range(2, MapWidth - roomMaxWidth - 2);
            int roomPointY = Random.Range(2, MapWidth - roomMaxWidth - 2);

            int roadStartPointX = Random.Range(roomPointX, roomPointX + roomWidth);
            int roadStartPointY = Random.Range(roomPointY, roomPointY + roomHeight);

            bool isRoad = CreateRoomData(roomHeight, roomWidth, roomPointX, roomPointY);

            if (isRoad == false)
            {
                CreateRoadData(roadStartPointX, roadStartPointY, meetPointsX[Random.Range(0, meetPointCount-1)], meetPointsY[Random.Range(0, meetPointCount-1)]);
            }
        }
    }
    /// <summary>
    /// 部屋データを生成。すでに部屋がある場合はtrueを返し、道を作らないようにする
    /// </summary>
    /// <param name="roomHeight">部屋の高さ</param>
    /// <param name="roomWidth">部屋の横幅</param>
    /// <param name="roomPointX">部屋の始点(x)</param>
    /// <param name="roomPointY">部屋の始点(y)</param>
    /// <returns></returns>
    private bool CreateRoomData(int roomHeight, int roomWidth, int roomPointX, int roomPointY) {
        bool isRoad = false;
        //  追加した処理
        //  部屋を重ねて生成しない
            //  for分のi,jに-1、room～に+1をしたのは部屋がすぐ隣に生成されることを防ぐため
        for (int i = -1; i < roomHeight+1; i++) {       
            for (int j = -1; j < roomWidth+1; j++) {
                if (Map[roomPointY + i, roomPointX + j] == room ) {
                    isRoad = true;
                    return isRoad;
                }
            }
        }
        //  もともとの処理
        //  すでに部屋がある場合はtrueを返し、道を作らないようにする
        for (int i = 0; i < roomHeight; i++) {
            for (int j = 0; j < roomWidth; j++) {
                if (Map[roomPointY + i, roomPointX + j] == road) {
                    Map[roomPointY + i, roomPointX + j] = room;
                    isRoad = true;
                } else {
                    Map[roomPointY + i, roomPointX + j] = room;
                }
            }
        }
        return isRoad;
    }
/// <summary>
    /// 道データを生成
    /// </summary>
    /// <param name="roadStartPointX"></param>
    /// <param name="roadStartPointY"></param>
    /// <param name="meetPointX"></param>
    /// <param name="meetPointY"></param>
    private void CreateRoadData(int roadStartPointX, int roadStartPointY, int meetPointX, int meetPointY) {

        bool isRight;
        if (roadStartPointX > meetPointX) {
            isRight = true;
        } else {
            isRight = false;
        }
        bool isUnder;
        if (roadStartPointY > meetPointY) {
            isUnder = false;
        } else {
            isUnder = true;
        }

        if(Random.Range(0,2) == 0) {

            while (roadStartPointX != meetPointX) {
                Map[roadStartPointY, roadStartPointX] = road;
                if (isRight == true)
                {
                    roadStartPointX--;
                }
                else
                {
                    roadStartPointX++;
                }
                if (RoomCheck(roadStartPointY, roadStartPointX) == true) { break; }
            }

            while(roadStartPointY != meetPointY) {
                Map[roadStartPointY, roadStartPointX] = road;
                if (isUnder == true)
                {
                    roadStartPointY++;
                }
                else
                {
                    roadStartPointY--;
                }
                if (RoomCheck(roadStartPointY, roadStartPointX) == true) { break; }
            }

        } else {

            while (roadStartPointY != meetPointY) {
                Map[roadStartPointY, roadStartPointX] = road;
                if (isUnder == true)
                {
                    roadStartPointY++;
                }
                else
                {
                    roadStartPointY--;
                }
                if (RoomCheck(roadStartPointY, roadStartPointX) == true) { break; }
            }

            while (roadStartPointX != meetPointX) {
                Map[roadStartPointY, roadStartPointX] = road;
                if (isRight == true)
                {
                    roadStartPointX--;
                }
                else
                {
                    roadStartPointX++;
                }
                if (RoomCheck(roadStartPointY, roadStartPointX) == true) { break; }
            }
        }
    }

    private bool RoomCheck(int i,int j)
    {
        if(Map[i,j] == room){
           //return true;
        }
        return false;
    }

}
