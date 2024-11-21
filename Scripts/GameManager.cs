using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI; //UIを扱うために必要な名前空間


public class GameManager : MonoBehaviour
{
    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeCnt;

    public GameObject mainImage; //イラスト文字を持つGameObject
    public Sprite gameOverSpr; //GAMEOVER画像
    public Sprite gameClearSpr; //GAMECLEAR画像
    public GameObject panel; //パネル
    public GameObject restartButton; //RESTARTボタン
    public GameObject nextButton; //NEXTボタン

    public GameObject scoreText;
    public static int totalScore;
    public int stageScore = 0;

    public AudioClip meGameOver;
    public AudioClip meGameClear;

    //+++プレイヤー操作+++
    public GameObject inputUI; //タッチ操作のUIパネル

    Image titleImage; //イラスト文字を表示しているImageコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();

        //画像を非表示にする
        Invoke("InactiveImage", 1.0f);
        //ボタン（パネル）を非表示にする
        panel.SetActive(false);

        timeCnt = GetComponent<TimeController>();
        if (timeCnt != null)
        {
            if (timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.gameState == "gameclear")
        {
            //ゲームクリア
            mainImage.SetActive(true); //イラスト文字を表示
            panel.SetActive(true); //パネル（ボタン）を表示
            //RESTARTボタンの無効化
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false; //Buttonコンポーネントのボタン有効化の変数をfalse
            mainImage.GetComponent<Image>().sprite = gameClearSpr; //GAMECLEARのイラスト文字に変更

            PlayerController.gameState = "gameend"; //何回もこの一連の処理を繰り返さないようにするため

            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;
                
               int time = (int)timeCnt.displayTime;
                totalScore += time * 10;
            }

            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();

            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameClear);
            }

            //+++プレイヤー操作+++
            inputUI.SetActive(false); //タッチ操作UIを隠す
        }
        else if (PlayerController.gameState == "gameover")
        {
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true;

            }

            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameClear);
            }

            //+++プレイヤー操作+++
            inputUI.SetActive(false); //タッチ操作UIを隠す

            //ゲームオーバー
            mainImage.SetActive(true); //イラスト文字を表示する
            panel.SetActive(true); //パネル（ボタン）を表示する
            //NEXTボタンを無効化する
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;//Buttonコンポーネントのボタン有効化の変数をfalse
            mainImage.GetComponent<Image>().sprite = gameOverSpr; //GAMECLEARのイラスト文字に変更

            PlayerController.gameState = "gameend"; //何回もこの一連の処理を繰り返さないようにするため
        }
        else if (PlayerController.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();

            if (timeCnt != null)
            {
                if (timeCnt.gameTime > 0.0f)
                {
                    int time = (int)timeCnt.displayTime;
                    timeText.GetComponent<TextMeshProUGUI>().text = time.ToString();

                    if (time == 0)
                    {
                        playerCnt.GameOver();
                    }
                }
            }

            if (playerCnt.score != 0)
            {
                stageScore += playerCnt.score;
                playerCnt.score = 0;
                UpdateScore();
            }
        }
    }

    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    //+++プレイヤー操作+++
    //GameManager経由のジャンプメソッドの制作
    public void Jump()
    {
        //プレイヤーオブジェクトを変数playerに取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //プレイヤーについているPlayerController.csを変数playerCntに取得
        PlayerController playerCnt = player.GetComponent<PlayerController>();

        //PlayerControllerのJumpメソッドを発動
        playerCnt.Jump();
    }
}