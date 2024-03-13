using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Data;

/// <summary>
/// 읽어들이는 데이터의 포맷 클래스는 ILoader인터페이스를 구현해야 함.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface ILoader<TKey, TValue>
{
    /// <summary>
    /// 데이터를 Dictionary 형태로 변환하는 메서드. 
    /// Dictionary의 키와 값은 인터페이스의 제네릭 타입으로 지정.
    /// </summary>
    /// <returns></returns>
    Dictionary<TKey, TValue> MakeDict();
}

/// <summary>
/// 데이터를 관리하는 매니저.
/// </summary>
/// 
public class DataManager 
{
    // RoomData.json 파일을 리스트 형태로 불러옴
    public List<RoomData> ReadRoomDataFromFile()
    {
        string _path = Path.Combine(Application.persistentDataPath, "RoomData.json");
        List<RoomData> roomDataList = new List<RoomData>();

        if (File.Exists(_path))
        {
            string[] lines = File.ReadAllLines(_path);

            foreach (string line in lines)
            {
                RoomData roomData = JsonUtility.FromJson<RoomData>(line);
                roomDataList.Add(roomData);
            }
        }
        else
        {
            Debug.LogError("File not found: " + _path);
        }

        return roomDataList;
    }
}
