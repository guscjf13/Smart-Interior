using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.Net.Json;
using System.IO;

public class ButtonManager : MonoBehaviour
{
    public GameObject Furnitrue, bed, bench_chair, chair, computer,
        desk, dresser, dressing_table, fireplace, gym_equipment, hanger, indoor_lamp,
        music, plant, recreation, shelving, sofa, stand, table, television,
        trash_can, tv_stand, wardrobe_cabinet;
    GameObject Furniture, Map, index, Map2;

    String id = "021";
    int id_int = 21;
    public static float roomWidth = 3, roomLength = 5, roomHeight = 2;
    public static double wall = 0.0;
    public static int pm_state = 0;

    String jsonHouse = "", jsonWall = "";
    int furnitureNum = 4;
    static public ArrayList furniture;
    Furniture[] myFurniture;
    private int clickSignal = 0;
    // Start is called before the first frame update
    void Start()
    {
        Furniture = GameObject.Find("Furniture");
        Map = GameObject.Find("Map");
        Map.GetComponent<RectTransform>().sizeDelta = new Vector2(roomWidth * 100, roomLength * 100);
        Map2 = GameObject.Find("Map2");
        getFurnitureInfo();
        setPrefabSize();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void btnClick_2()
    {
        index = EventSystem.current.currentSelectedGameObject;
        String str = index.name;
        Debug.Log(index.name);
        bed.SetActive(false); chair.SetActive(false); computer.SetActive(false);
        bench_chair.SetActive(false); desk.SetActive(false); dresser.SetActive(false);
        dressing_table.SetActive(false); fireplace.SetActive(false); gym_equipment.SetActive(false);
        hanger.SetActive(false); indoor_lamp.SetActive(false); music.SetActive(false);
        plant.SetActive(false); recreation.SetActive(false); shelving.SetActive(false);
        sofa.SetActive(false); stand.SetActive(false);
        table.SetActive(false); television.SetActive(false); trash_can.SetActive(false);
        tv_stand.SetActive(false); wardrobe_cabinet.SetActive(false);

        if (index.name == "bed") bed.SetActive(true);
        else if (index.name == "bench_chair") bench_chair.SetActive(true);
        else if (index.name == "chair") chair.SetActive(true);
        else if (index.name == "computer") computer.SetActive(true);
        else if (index.name == "desk") desk.SetActive(true);
        else if (index.name == "dresser") dresser.SetActive(true);
        else if (index.name == "dressing_table") dressing_table.SetActive(true);
        else if (index.name == "fireplace") fireplace.SetActive(true);
        else if (index.name == "gym_equipment") gym_equipment.SetActive(true);
        else if (index.name == "hanger") hanger.SetActive(true);
        else if (index.name == "indoor_lamp") indoor_lamp.SetActive(true);
        else if (index.name == "music") music.SetActive(true);
        else if (index.name == "plant") plant.SetActive(true);
        else if (index.name == "recreation") recreation.SetActive(true);
        else if (index.name == "shelving") shelving.SetActive(true);
        else if (index.name == "sofa") sofa.SetActive(true);
        else if (index.name == "stand") stand.SetActive(true);
        else if (index.name == "tv_stand") tv_stand.SetActive(true);
        else if (index.name == "table") table.SetActive(true);
        else if (index.name == "television") television.SetActive(true);
        else if (index.name == "trash_can") trash_can.SetActive(true);
        else if (index.name == "wardrobe_cabinet") wardrobe_cabinet.SetActive(true);

        //Category = GameObject.Find(index.name);
        //Debug.Log(Category.name);
        //Debug.Log("su");
        //Category.SetActive(false);
    }
    public void btnClick()
    {
        if (clickSignal == 2)
        {
            index = EventSystem.current.currentSelectedGameObject;
            GameObject child = Instantiate(Furnitrue, Map2.transform.position, gameObject.transform.rotation);

            child.GetComponent<FurnitureCreate>().childname = index.name;
            child.transform.SetParent(Map2.transform);
            child.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            clickSignal = 1;
        }
        else
        {
            index = EventSystem.current.currentSelectedGameObject;
            GameObject child = Instantiate(Furnitrue, Map.transform.position, gameObject.transform.rotation);
            child.GetComponent<FurnitureCreate>().childname = index.name;
            child.transform.SetParent(Furniture.transform);
            //Debug.Log(index.name + "Oh NO");
            foreach (Furniture f in furniture)
            {
                if ((index.name).CompareTo(f.modelID) == 0)
                {
                    child.GetComponent<RectTransform>().localScale = new Vector3((float)f.width, (float)f.length, (float)f.height);
                }
            }

            //child.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
    public void map2ButtonClick()
    {
        clickSignal = 2;
    }
    public void plusClick()
    {
        pm_state = 1;
    }
    public void minusClick()
    {
        pm_state = 2;
    }
    public void rotateClick()
    {
        pm_state = 0;
    }
    public void deleteClick()
    {
        pm_state = 3;
    }
    public void makeJson()
    {

        while (true)
        {
            String temp = "C:/Users/hyunchul/Desktop/house/" + id;
            DirectoryInfo diTemp = new DirectoryInfo(temp);
            if (!diTemp.Exists)
                break;

            id_int++;
            if (id_int < 10)
                id = "00" + id_int;
            else if (id_int < 100)
                id = "0" + id_int;
        }

        jsonHouse = "";
        jsonWall = "";
        initJsonHouse();
        makeJsonHouse();

        JsonTextParser parserHouse = new JsonTextParser();
        JsonObject objHouse = parserHouse.Parse(jsonHouse);
        JsonObjectCollection colHouse = (JsonObjectCollection)objHouse;

        string folderPath1 = "C:/Users/hyunchul/Desktop/house/" + id;
        DirectoryInfo di1 = new DirectoryInfo(folderPath1);
        if (!di1.Exists)
            di1.Create();
        File.WriteAllText("C:/Users/hyunchul/Desktop/house/" + id + "/house.json", colHouse.ToString());

        makeJsonWall();

        JsonTextParser parseWall = new JsonTextParser();
        JsonObject objWall = parserHouse.Parse(jsonWall);
        JsonObjectCollection colWall = (JsonObjectCollection)objWall;

        string folderPath2 = "C:/Users/hyunchul/Desktop/wall/" + id;
        DirectoryInfo di2 = new DirectoryInfo(folderPath2);
        if (!di2.Exists)
            di2.Create();
        File.WriteAllText("C:/Users/hyunchul/Desktop/wall/" + id + "/" + id + ".arch.json", colWall.ToString());

    }
    public void getFurnitureInfo()
    {
        furniture = new ArrayList();

        TextAsset data = (TextAsset)Resources.Load("Models");
        StringReader sr = new StringReader(data.text);

        string line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] words = line.Split('"');
            string id = words[0].Split(',')[0];
            double width = (double.Parse(words[5].Split(',')[0]) - double.Parse(words[3].Split(',')[0])) * 100;
            double height = (double.Parse(words[5].Split(',')[1]) - double.Parse(words[3].Split(',')[1])) * 100;
            double length = (double.Parse(words[5].Split(',')[2]) - double.Parse(words[3].Split(',')[2])) * 100;
            furniture.Add(new Furniture(id, width, height, length));
        }

    }

    public void setPrefabSize()
    {


    }

    public void initJsonHouse()
    {
        furnitureNum = Furniture.transform.childCount;

        //방 matrix 만들기
        double minX = 38.3498855248876;
        double minZ = 0;
        double minY = 37.44011313910404;
        double maxX = 43.54520764093708;
        double maxZ = 2.739999938756229;
        double maxY = 42.61519713265855;

        double xscale = (roomWidth) / (maxX - minX);
        double zscale = (roomHeight) / (maxZ - minZ);
        double yscale = (roomLength) / (maxY - minY);

        double x = -(minX + maxX) / 2;
        double z = -minZ;
        double y = -(minY + maxY) / 2;
        double[,] t = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { x, z, y, 1 } };

        x = roomWidth / 2;
        z = 0.0;
        y = roomLength / 2;
        double[,] t_scale = { { xscale, 0, 0, 0 }, { 0, zscale, 0, 0 }, { 0, 0, yscale, 0 }, { 0, 0, 0, 1 } };
        double[,] t_shift = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { x, z, y, 1 } };

        double[,] tempMatrix = new double[4, 4];
        for (int a = 0; a < 4; a++)
            for (int b = 0; b < 4; b++)
            {
                tempMatrix[a, b] = 0;
                for (int c = 0; c < 4; c++)
                    tempMatrix[a, b] += t[a, c] * t_scale[c, b];
            }

        double[,] roomMatrix = new double[4, 4];
        for (int a = 0; a < 4; a++)
            for (int b = 0; b < 4; b++)
            {
                roomMatrix[a, b] = 0;
                for (int c = 0; c < 4; c++)
                    roomMatrix[a, b] += tempMatrix[a, c] * t_shift[c, b];
            }

        jsonHouse += "{\r\n" +
                     "  \"version\": \"suncg@1.0.0\",\r\n" +
                     "  \"id\": \"" + id + "\",\r\n" +
                     "  \"up\": [\r\n" +
                     "    0,\r\n" +
                     "    1,\r\n" +
                     "    0\r\n" +
                     "  ],\r\n" +
                     "  \"front\": [\r\n" +
                     "    0,\r\n" +
                     "    0,\r\n" +
                     "    1\r\n" +
                     "  ],\r\n" +
                     "  \"scaleToMeters\": 1,\r\n" +
                     "  \"levels\": [\r\n" +
                     "    {\r\n" +
                     "      \"id\": \"0\",\r\n" +
                     "      \"bbox\": {\r\n" +
                     "        \"min\": [\r\n" +
                     "          0,\r\n" +
                     "          0,\r\n" +
                     "          0\r\n" +
                     "        ],\r\n" +
                     "        \"max\": [\r\n" +
                     "          " + roomWidth + ",\r\n" +
                     "          " + roomHeight + ",\r\n" +
                     "          " + roomLength + "\r\n" +
                     "        ]\r\n" +
                     "      },\r\n" +
                     "      \"nodes\": [\r\n" +
                     "        {\r\n" +
                     "          \"id\": \"0_0\",\r\n" +
                     "          \"type\": \"Room\",\r\n" +
                     "          \"valid\": 1,\r\n" +
                     "          \"modelId\": \"fr_0rm_0\",\r\n" +
                     "          \"nodeIndices\": [\r\n";

        for (int i = 1; i < furnitureNum; i++)
            jsonHouse += "            " + i + ",\r\n";
        jsonHouse += "            " + furnitureNum + "\r\n";

        jsonHouse += "          ],\r\n" +
                     "          \"transform\": [\r\n";

        for (int a = 0; a < 15; a++)
            jsonHouse += "            " + roomMatrix[a / 4, a % 4] + ",\r\n";
        jsonHouse += "            " + roomMatrix[3, 3] + "\r\n";

        jsonHouse += "          ],\r\n" +
                     "          \"roomTypes\": [\r\n" +
                     "            \"Bedroom\"\r\n" +
                     "          ],\r\n" +
                     "          \"bbox\": {\r\n" +
                     "            \"min\": [\r\n" +
                     "              0.0,\r\n" +
                     "              0.0,\r\n" +
                     "              0.0\r\n" +
                     "            ],\r\n" +
                     "            \"max\": [\r\n" +
                     "              " + (roomWidth) + ",\r\n" +
                     "              " + (roomHeight) + ",\r\n" +
                     "              " + (roomLength) + "\r\n" +
                     "            ]\r\n" +
                     "          }\r\n" +
                     "        },\r\n";

    }

    public void makeJsonHouse()
    {
        myFurniture = new Furniture[furnitureNum];
        for (int i = 0; i < furnitureNum; i++)
        {
            for (int j = 0; j < Furniture.transform.GetChild(i).transform.childCount; j++)
            {
                if (Furniture.transform.GetChild(i).transform.GetChild(j).gameObject.activeSelf)
                {
                    Debug.Log(Furniture.transform.GetChild(i).transform.GetChild(j).name);
                    string modelID = Furniture.transform.GetChild(i).transform.GetChild(j).name;

                    foreach (Furniture f in furniture)
                    {
                        if (modelID == f.modelID)
                        {
                            myFurniture[i] = new Furniture(f.modelID, f.width, f.height, f.length);
                            myFurniture[i].scale = Furniture.transform.GetChild(i).transform.localScale.x / f.width;
                            myFurniture[i].startPoint1 = (Furniture.transform.GetChild(i).transform.localPosition.x + roomWidth * 50) / 100 -
                                f.width * myFurniture[i].scale / 100 / 2;
                            myFurniture[i].startPoint2 = wall;
                            myFurniture[i].startPoint3 = (Furniture.transform.GetChild(i).transform.localPosition.y + roomLength * 50) / 100 -
                                f.length * myFurniture[i].scale / 100 / 2;
                            myFurniture[i].rotation = -Furniture.transform.GetChild(i).transform.eulerAngles.z;
                        }
                    }

                }
            }

        }


        for (int i = 0; i < furnitureNum; i++)
        {

            jsonHouse += "        {\r\n" +
                            "          \"id\": \"0_" + (i + 1) + "\",\r\n" +
                            "          \"type\": \"Object\",\r\n" +
                            "          \"valid\": 1,\r\n" +
                            "          \"modelId\": \"" + myFurniture[i].modelID + "\",\r\n" +
                            "          \"transform\": [\r\n";

            double[,] scaleMatrix = {
                {myFurniture[i].scale,0,0,0},
                {0,myFurniture[i].scale,0,0},
                {0,0,myFurniture[i].scale,0},
                {0,0,0,1}
            };

            double[,] locationMatrix = {
                {1,0,0,0},
                {0,1,0,0},
                {0,0,1,0},
                {myFurniture[i].startPoint1 + myFurniture[i].width * myFurniture[i].scale/ 100 / 2,myFurniture[i].startPoint2, myFurniture[i].startPoint3 + myFurniture[i].length * myFurniture[i].scale / 100 / 2,1}
            };

            double[,] rotationMatrix = {
                {Math.Cos((myFurniture[i].rotation)*Math.PI/180), 0, -Math.Sin((myFurniture[i].rotation)*Math.PI/180), 0},
                {0, 1, 0, 0},
                {Math.Sin((myFurniture[i].rotation)*Math.PI/180), 0, Math.Cos((myFurniture[i].rotation)*Math.PI/180), 0},
                {0, 0, 0, 1}
            };

            /*
            double[,] rotationMatrix = {
                {Math.Cos((myFurniture[i].rotation+180)*Math.PI/180), 0, -Math.Sin((myFurniture[i].rotation+180)*Math.PI/180), 0},
                {0, 1, 0, 0},
                {Math.Sin((myFurniture[i].rotation+180)*Math.PI/180), 0, Math.Cos((myFurniture[i].rotation+180)*Math.PI/180), 0},
                {0, 0, 0, 1}
            };
             */

            double[,] tempMatrix = new double[4, 4];
            for (int a = 0; a < 4; a++)
                for (int b = 0; b < 4; b++)
                {
                    tempMatrix[a, b] = 0;
                    for (int c = 0; c < 4; c++)
                        tempMatrix[a, b] += rotationMatrix[a, c] * scaleMatrix[c, b];
                }

            double[,] finalMatrix = new double[4, 4];
            for (int a = 0; a < 4; a++)
                for (int b = 0; b < 4; b++)
                {
                    finalMatrix[a, b] = 0;
                    for (int c = 0; c < 4; c++)
                        finalMatrix[a, b] += tempMatrix[a, c] * locationMatrix[c, b];
                }

            for (int a = 0; a < 15; a++)
                jsonHouse += "            " + finalMatrix[a / 4, a % 4] + ",\r\n";
            jsonHouse += "            " + finalMatrix[3, 3] + "\r\n";

            jsonHouse +=
                            "          ],\r\n" +
                            "          \"bbox\": {\r\n" +
                            "            \"min\": [\r\n" +
                            "              " + myFurniture[i].startPoint1 + ",\r\n" +
                            "              " + myFurniture[i].startPoint2 + ",\r\n" +
                            "              " + myFurniture[i].startPoint3 + "\r\n" +
                            "            ],\r\n" +
                            "            \"max\": [\r\n" +
                            "              " + (myFurniture[i].startPoint1 + myFurniture[i].width * myFurniture[i].scale / 100) + ",\r\n" +
                            "              " + (myFurniture[i].startPoint2 + myFurniture[i].height * myFurniture[i].scale / 100) + ",\r\n" +
                            "              " + (myFurniture[i].startPoint3 + myFurniture[i].length * myFurniture[i].scale / 100) + "\r\n" +
                            "            ]\r\n" +
                            "          },\r\n" +
                            "          \"materials\": [\r\n" +
                            "            {\r\n" +
                            "              \"name\": \"color\",\r\n" +
                            "              \"diffuse\": \"#a3a3a3\"\r\n" +
                            "            },\r\n" +
                            "            {\r\n" +
                            "              \"name\": \"color_1\",\r\n" +
                            "              \"diffuse\": \"#6990a3\"\r\n" +
                            "            }\r\n" +
                            "          ]\r\n";

            if (i == furnitureNum - 1)
                jsonHouse += "        }\r\n";
            else
                jsonHouse += "        },\r\n";

        }

        jsonHouse += "      ]\r\n" +
                        "    }\r\n" +
                        "  ],\r\n" +
                        "  \"bbox\": {\r\n" +
                        "    \"min\": [\r\n" +
                        "      0,\r\n" +
                        "      0,\r\n" +
                        "      0\r\n" +
                        "    ],\r\n" +
                        "    \"max\": [\r\n" +
                        "      " + roomWidth + ",\r\n" +
                        "      " + roomHeight + ",\r\n" +
                        "      " + roomLength + "\r\n" +
                        "    ]\r\n" +
                        "  }\r\n" +
                        "}";

    }

    public void makeJsonWall()
    {
        jsonWall += "{\r\n" +
                    "  \"version\": \"suncg-arch@1.0.0\",\r\n" +
                    "  \"id\": \"" + id + "\",\r\n" +
                    "  \"up\": [ 0, 1, 0 ],\r\n" +
                    "  \"front\": [ 0, 0, 1 ],\r\n" +
                    "  \"scaleToMeters\": 1,\r\n" +
                    "  \"defaults\": {\r\n" +
                    "    \"Wall\": {\r\n" +
                    "      \"depth\": 0.1,\r\n" +
                    "      \"extraHeight\": 0.035\r\n" +
                    "    },\r\n" +
                    "    \"Ceiling\": {\r\n" +
                    "      \"depth\": 0.05\r\n" +
                    "    },\r\n" +
                    "    \"Floor\": {\r\n" +
                    "      \"depth\": 0.05\r\n" +
                    "    },\r\n" +
                    "    \"Ground\": {\r\n" +
                    "      \"depth\": 0.08\r\n" +
                    "    }\r\n" +
                    "  },\r\n" +
                    "  \"elements\": [\r\n" +
                    "    {\r\n" +
                    "      \"id\": \"0_0_0\",\r\n" +
                    "      \"type\": \"Wall\",\r\n" +
                    "      \"roomId\": \"0_0\",\r\n" +
                    "      \"height\": " + roomHeight + ",\r\n" +
                    "      \"points\": [\r\n" +
                    "        [ 0, 0, 0 ],\r\n" +
                    "        [ 0, 0, " + (roomLength) + " ]\r\n" +
                    "      ],\r\n" +
                    "      \"holes\": [],\r\n" +
                    "      \"materials\": [\r\n" +
                    "        {\r\n" +
                    "          \"name\": \"inside\",\r\n" +
                    "          \"texture\": \"wallp_0\",\r\n" +
                    "          \"diffuse\": \"#7a0400\"\r\n" +
                    "        },\r\n" +
                    "        {\r\n" +
                    "          \"name\": \"outside\",\r\n" +
                    "          \"texture\": \"bricks_1\",\r\n" +
                    "          \"diffuse\": \"#ffffff\"\r\n" +
                    "        }\r\n" +
                    "      ],\r\n" +
                    "      \"depth\": 0.1\r\n" +
                    "    },\r\n" +
                    "    {\r\n" +
                    "\r\n" +
                    "      \"id\": \"0_0_1\",\r\n" +
                    "      \"type\": \"Wall\",\r\n" +
                    "      \"roomId\": \"0_0\",\r\n" +
                    "      \"height\": 3,\r\n" +
                    "      \"points\": [\r\n" +
                    "        [ " + (roomWidth) + ", 0, " + (roomLength) + " ],\r\n" +
                    "        [ " + (roomWidth) + ", 0, 0 ]\r\n" +
                    "      ],\r\n" +
                    "      \"holes\": [],\r\n" +
                    "      \"materials\": [\r\n" +
                    "        {\r\n" +
                    "          \"name\": \"inside\",\r\n" +
                    "          \"texture\": \"wallp_0\",\r\n" +
                    "          \"diffuse\": \"#7a0400\"\r\n" +
                    "        },\r\n" +
                    "        {\r\n" +
                    "          \"name\": \"outside\",\r\n" +
                    "          \"texture\": \"bricks_1\",\r\n" +
                    "          \"diffuse\": \"#ffffff\"\r\n" +
                    "        }\r\n" +
                    "      ],\r\n" +
                    "      \"depth\": 0.1\r\n" +
                    "    },\r\n" +
                    "    {\r\n" +
                    "      \"id\": \"0_0_2\",\r\n" +
                    "      \"type\": \"Wall\",\r\n" +
                    "      \"roomId\": \"0_0\",\r\n" +
                    "      \"height\": 3,\r\n" +
                    "      \"points\": [\r\n" +
                    "        [ " + (roomWidth) + ", 0, 0 ],\r\n" +
                    "        [ 0, 0, 0 ]\r\n" +
                    "      ],\r\n" +
                    "      \"holes\": [],\r\n" +
                    "      \"materials\": [\r\n" +
                    "        {\r\n" +
                    "          \"name\": \"inside\",\r\n" +
                    "          \"texture\": \"wallp_0\",\r\n" +
                    "          \"diffuse\": \"#7a0400\"\r\n" +
                    "        },\r\n" +
                    "        {\r\n" +
                    "          \"name\": \"outside\",\r\n" +
                    "          \"texture\": \"bricks_1\",\r\n" +
                    "          \"diffuse\": \"#ffffff\"\r\n" +
                    "        }\r\n" +
                    "      ],\r\n" +
                    "      \"depth\": 0.1\r\n" +
                    "    },\r\n" +
                    "    {\r\n" +
                    "      \"id\": \"0_0_3\",\r\n" +
                    "      \"type\": \"Wall\",\r\n" +
                    "      \"roomId\": \"0_0\",\r\n" +
                    "      \"height\": 3,\r\n" +
                    "      \"points\": [\r\n" +
                    "        [ 0, 0, " + (roomLength) + " ],\r\n" +
                    "        [ " + (roomWidth) + ", 0, " + (roomLength) + " ]\r\n" +
                    "      ],\r\n" +
                    "      \"holes\": [],\r\n" +
                    "      \"materials\": [\r\n" +
                    "        {\r\n" +
                    "          \"name\": \"inside\",\r\n" +
                    "          \"texture\": \"wallp_0\",\r\n" +
                    "          \"diffuse\": \"#7a0400\"\r\n" +
                    "        },\r\n" +
                    "        {\r\n" +
                    "          \"name\": \"outside\",\r\n" +
                    "          \"texture\": \"bricks_1\",\r\n" +
                    "          \"diffuse\": \"#ffffff\"\r\n" +
                    "        }\r\n" +
                    "      ],\r\n" +
                    "      \"depth\": 0.1\r\n" +
                    "    }\r\n" +
                    "  ]\r\n" +
                    "}";

    }
}

class Furniture
{
    public string modelID;
    public double width, height, length;
    public double startPoint1, startPoint2, startPoint3;
    public double rotation, scale;

    public Furniture(string modelID, double width, double height, double length)
    {
        this.modelID = modelID;
        this.width = width;
        this.height = height;
        this.length = length;
        startPoint1 = 0;
        startPoint2 = 0;
        startPoint3 = 0;
        rotation = 0;
        scale = 0;
    }

    public Furniture(string modelID)
    {
        this.modelID = modelID;
        this.width = 0;
        this.height = 0;
        this.length = 0;
        startPoint1 = 0;
        startPoint2 = 0;
        startPoint3 = 0;
        rotation = 0;
        scale = 0;
    }
}