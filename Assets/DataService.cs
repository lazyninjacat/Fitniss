
using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
	}

    //########## Table Return queries - returns all data in each table ##########//

    public IEnumerable<ExerciseLog> GetExerciseLogTable()
    {
        return _connection.Table<ExerciseLog>();
    }

	public IEnumerable<UserLog> GetUserLogTable(){
		return _connection.Table<UserLog>();
	}

    public IEnumerable<Circuit> GetCircuitTable()
    {
        return _connection.Table<Circuit>();
    }

    public IEnumerable<Config> GetConfogTable()
    {
        return _connection.Table<Config>();
    }

    public string GetExerciseName(int orderID)
    {
        string query = "SELECT ExerciseName FROM Circuit WHERE OrderID = ?";
        return _connection.ExecuteScalar<string>(query, orderID);
    }

    public string GetExerciseAmount(int orderID)
    {
        string query = "SELECT ExerciseAmount FROM Circuit WHERE OrderID = ?";
        return _connection.ExecuteScalar<string>(query, orderID);
    }

    public int GetCircuitCount()
    {
        return _connection.Table<Circuit>().Count();
    }

    public int GetPresetCount()
    {
        return _connection.Table<CircuitPreset>().Count();
    }


    ///<summary>SQL query to reset the auto increment sequence number back to the value given</summary>
    ///<param name="tableName">the name of the table to reset the sequence number</param>
    ///<param name="resetToInt">the number to reset the sequence to</param>
    public void ReseedTable(string tableName, int resetToInt)
    {
        string cmd = "UPDATE SQLITE_SEQUENCE SET seq = ? WHERE name = ?";
        _connection.Execute(cmd, resetToInt, tableName);

    }

    public int AddUserLogEntry(string dateStr, float weight, float waist)
    {
        string query = "INSERT INTO UserLog (Timestamp, Weight, Waist) VALUES(?, ?, ?)";
        return _connection.Execute(query, dateStr, weight, waist);
    }

}

  /*  © 2018 GitHub, Inc.
    Terms
    Privacy
    Security
    Status
    Help

    Contact GitHub
    API
    Training
    Shop
    Blog
    About

Press h to open a hovercard with more details.
*/