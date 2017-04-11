using System. Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class TestScript : MonoBehaviour {
    
    // Use this for initialization
    void Start ()
    {
        MasterData.Test.mst_test = new List<mst_test>();
        Init(ref MasterData.Test.mst_test);

    }

    void Init(ref List<mst_test> list)
    {
        list = new List<mst_test>();
        for (int i = 0; i < 10; i++)
        {
            mst_test test = new mst_test();
            test.id = i;
            test.name = "TEST" + i;
            list.Add(test);
        }
    }
	
    public void StartTest()
    {
        MasterData.TestClass NewClass = new MasterData.TestClass();
        Init(ref NewClass.mst_test);

        const string ClassName = "MasterData+TestClass";
        Type classType = Type.GetType(ClassName);
        //classType = typeof(MasterData.TestClass);
        Debug.Log(classType);
        var obj = Activator.CreateInstance(classType);

        FieldInfo[] infoArray = classType.GetFields();

        // プロパティ情報出力をループで回す
        foreach (FieldInfo info in infoArray)
        {
            Debug.Log(info.Name + ": " + info.GetValue(obj));
            var type = info.FieldType;
            Debug.Log(type);

            if (!type.IsGenericType) continue;
            
            //List<int>のCountプロパティに対するPropertyInfoを取得する
            PropertyInfo countProp = type.GetProperty("Count");
                
            //Countプロパティを実行し、リストに格納されている要素数を取得する
            int count = (int)countProp.GetValue(MasterData.Test.mst_test, null);

            //リストに要素された要素をインデクサを用いて取得し、
            //コンソールに表示する
            for (int i = 0; i < count; i++)
            {
                //インデクサに対するPropertyInfoを取得する
                PropertyInfo indexer = type.GetProperty("Item");

                //インデクサを呼び出しリスト中の値を取得する
                var value =
                    indexer.GetValue(MasterData.Test.mst_test, new System.Object[] { i });

                //コンソールに取得した値を表示する
                Debug.Log(value);
            }
        }
    }
}


public static class MasterData
{
    public class TestClass
    {
        public int num;
        public List<mst_test> mst_test;
    }

    public static TestClass Test = new TestClass();
}

public class mst_test
{
    public int id;
    public string name;
}