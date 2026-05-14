using Mono.Data.Sqlite;
using ShapeDefenseSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using static ShapeDefenseSpace.GameData;

public class OutSaveManager : MonoBehaviour
{
    private readonly string DBName = "ShapeDefenseDB.db";

    public string GetDownloadFolderPath() {
        if (Application.platform == RuntimePlatform.Android) {
            // 안드로이드 Java 클래스를 사용하여 다운로드 폴더 경로 가져오기
            using (AndroidJavaClass envClass = new AndroidJavaClass("android.os.Environment")) {
                using (AndroidJavaObject downloadsFolder = envClass.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", envClass.GetStatic<string>("DIRECTORY_DOWNLOADS"))) {
                    return downloadsFolder.Call<string>("getAbsolutePath");
                }
            }
        }
        else {
            // 다른 플랫폼에서는 지원하지 않음
            // alert 생성 고려
            //Debug.LogWarning("This method only works on Android.");
            return null;
        }
    }

    public void SaveToDownloadFolder() {
        string downloadPath = GetDownloadFolderPath();
        //Debug.Log(downloadPath);
        if (!string.IsNullOrEmpty(downloadPath)) {
            DataSavingPanelControll.Instance.OnLoadingField();
            StartCoroutine(CopyDB(downloadPath, 1));
        }
        else {
            DataSavingPanelControll.Instance.OnFailText();
            DataSavingPanelControll.Instance.SaveEndField();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="downloadPath"></param>
    /// <param name="type">1. save // 2. load</param>
    /// <returns></returns>
    IEnumerator CopyDB(string downloadPath, int type) {

        string sourcePath = ""; 
        string destinationPath = ""; 

        switch (type) {
            case 1:
                sourcePath = Path.Combine(Application.persistentDataPath, DBName);
                destinationPath = Path.Combine(downloadPath, DBName);
                break;
            case 2:
                sourcePath = Path.Combine(downloadPath, DBName);
                destinationPath = Path.Combine(Application.persistentDataPath, DBName);
                break;
        }

        //Debug.Log(destinationPath);

        yield return wfs_1;

        
        if (File.Exists(sourcePath)) {
            try {
                File.Copy(sourcePath, destinationPath, true);
                //Debug.Log($"DB file copied to: {destinationPath}");
            }
            catch (IOException ioEx) {
                //Debug.LogError("File copy failed due to an IO exception: " + ioEx.Message);
            }
            catch (Exception ex) {
                //Debug.LogError("File copy failed: " + ex.Message);
            }
        }
        else {
            DataSavingPanelControll.Instance.OnFailText();
            //Debug.LogError("DB file not found at source path: " + sourcePath);
        }

        // copy가 일어났는지 검사
        if(!File.Exists(destinationPath)) {
            DataSavingPanelControll.Instance.OnFailText();
        }
        switch (type) {
            case 1:
                DataSavingPanelControll.Instance.SaveEndField();
                break;
            case 2:
                ReConnectDB();
                break;
        }
    }

    public void LoadFromDownloadFolder() {
        string downloadPath = GetDownloadFolderPath();

        if (!string.IsNullOrEmpty(downloadPath)) {
            DataSavingPanelControll.Instance.OnLoadingField();
            StartCoroutine(CopyDB(downloadPath, 2));
        }
        else {
            DataSavingPanelControll.Instance.OnFailText();
            DataSavingPanelControll.Instance.LoadEndField();
        }
    }

    private void ReConnectDB() {
        // 옮긴 db 파일을 기반으로 데이터를 다시 받음
        ConnectDB.Instance.Connect_UserDB();
        ConnectDB.Instance.Connect_UnitDB();
        ConnectDB.Instance.Connect_EnemyDB();
        ConnectDB.Instance.Connect_QuestDB();
        ConnectDB.Instance.Connect_SettingDB();

        UtilityHub.PageHeaderSetting();
        DataSavingPanelControll.Instance.LoadEndField();
    }
}
