using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Google.Play.Review;

//Android版

public class ClearManager : MonoBehaviour
{
    //脱出成功フラグ
    public bool isClear;

    //UIパネル
    public GameObject ClearPanel;
    public GameObject GamePanel;
    //「脱出成功」
    public GameObject ClearImage;
    //「他の脱出ゲーム」
    public GameObject ToOtherApp;

    //真っ白パネル
    public GameObject White;

    //カメラ
    public Camera MainCamera;

    //ミラーボール
    public GameObject MirrorBall;

    //Dotweenのアニメーション保持用
    private Sequence CameraMove;


    //脱出演出
    public void Escape()
    {
        isClear = true;

        //クリアパネル表示
        ClearPanel.SetActive(true);
        //カメラを徐々にズーム&移動
        float defaultFov = MainCamera.fieldOfView;
        DOTween.To(() => MainCamera.fieldOfView, fov => MainCamera.fieldOfView = fov, 30, 5.9f);
        MainCamera.transform.DOMove(new Vector3(1.2f, 0, 0), 5.9f).SetRelative(true);

        //白パネルをフェードイン(2秒遅れで)
        White.GetComponent<Image>().DOFade(255f, 2000f).SetDelay(2f);

        Invoke(nameof(AfterClear1), 6);
    }

    private void AfterClear1()
    {
        GamePanel.SetActive(false);

        //カメラ移動
        MainCamera.fieldOfView = 60;
        CameraManager.Instance.ChangeCameraPosition("RoomEnd");

        //ミラーボールを回転させる
        var sequence = DOTween.Sequence();
        sequence.Append(
            MirrorBall.transform.DORotate(new Vector3(0, 360, 0), 10f, RotateMode.WorldAxisAdd)
            .SetEase(Ease.Linear)
        ).SetLoops(-1);
        sequence.Play();

        //各パーツを表示
        ClearImage.SetActive(true);
        ToOtherApp.SetActive(true);

        AudioManager.Instance.SoundSE("Ending");
        //「脱出成功」をズームイン
        ClearImage.transform.DOScale(new Vector3(7.2f, 2.9f, 2), 4f)
            .SetDelay(0.5f)
            .SetEase(Ease.OutBounce);
        //白パネルをフェードアウト(1秒遅れで)
        White.GetComponent<Image>().DOFade(0, 6f)
            .SetEase(Ease.InSine);
        //フェードアウト後に非表示に
        Invoke(nameof(HideWhite), 5.9f);



        // 「他のアプリへ」をフェードイン
        ToOtherApp.GetComponent<Image>().DOFade(255f, 2000f).SetDelay(7f);

        //アプリレビュー表示
        Invoke(nameof(ShowReview), 8.5f);
    }

    //白パネルを非表示に
    private void HideWhite()
    {
        White.SetActive(false);
    }



    /// <summary>
    /// 端末ごとで評価依頼を表示する
    /// </summary>
    private void ShowReview()
    {
#if UNITY_IOS
        UnityEngine.iOS.Device.RequestStoreReview();
#elif UNITY_ANDROID
        StartCoroutine(ShowReviewCoroutine());
#endif
    }


    ///// <summary>
    ///// Android端末でIn-App Review APIを呼ぶサンプル
    ///// </summary>
    private IEnumerator ShowReviewCoroutine()
    {
       // https://developer.android.com/guide/playcore/in-app-review/unity
       var reviewManager = new ReviewManager();
       var requestFlowOperation = reviewManager.RequestReviewFlow();
       yield return requestFlowOperation;
       if (requestFlowOperation.Error != ReviewErrorCode.NoError)
       {
           // エラーの場合はここで止まる.
           yield break;
       }
       var playReviewInfo = requestFlowOperation.GetResult();
       var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
       yield return launchFlowOperation;
       if (launchFlowOperation.Error != ReviewErrorCode.NoError)
       {
           // エラーの場合はここで止まる.
           yield break;
       }
    }

}





    //=====以下参考===========================================================

    //「うんちくん」を傾けるを繰り返す
    //private void vibeUnchi1()
    //{
    //  var sequence = DOTween.Sequence();

    //  sequence.Append(
    //    Unchi1.transform.DORotate(new Vector3(0,0,14), 0.12f, RotateMode.WorldAxisAdd).SetDelay(4f)
    //  );
    //  sequence.Append(
    //    Unchi1.transform.DORotate(new Vector3(0,0,-14), 0.12f, RotateMode.WorldAxisAdd).SetDelay(2f)
    //  ).SetLoops(-1);

    //  sequence.Play();
    //}

    //「うんちくん」を回転しながらズームイン
    //Unchi1.transform.DOScale(new Vector3(2f,2f,1), 1f).SetDelay(3f);
    //Unchi1.transform.DORotate(new Vector3(0,0,353), 1f, RotateMode.WorldAxisAdd).SetDelay(3f);
