using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Google.Play.Review;

public class ClearManager : MonoBehaviour
{
  public GameObject ClearPanel;
  public GameObject ClearImage;
  public GameObject ToTitle;

  public Camera MainCamera;

  //脱出演出
  public void Escape()
    {
        //クリアパネル表示
        ClearPanel.SetActive(true);
        //カメラを徐々にズーム&移動
        float defaultFov = MainCamera.fieldOfView;
        DOTween.To(() => MainCamera.fieldOfView, fov => MainCamera.fieldOfView = fov, 20, 10);
        MainCamera.transform.DOMove(new Vector3(1.5f,0,0), 10).SetRelative(true);

        //白パネルをフェードイン(2秒遅れで)
        ClearPanel.GetComponent<Image>().DOFade(255f, 2000f).SetDelay(2f);

        Invoke(nameof(AfterClear1),6);
    }

  private void AfterClear1()
    {
        ClearImage.SetActive(true);
        ToTitle.SetActive(true);
        //Unchi1.SetActive(true);
        //Unchi2.SetActive(true);
        //Unchi3.SetActive(true);

        AudioManager.Instance.SoundSE("Ending");
        //「脱出成功」をズームイン
        ClearImage.transform.DOScale(new Vector3(7.2f,2.9f,2), 4f).SetEase(Ease.OutBounce).SetDelay(0.5f);

        //「うんちくん」を回転しながらズームイン
      //Unchi1.transform.DOScale(new Vector3(2f,2f,1), 1f).SetDelay(3f);
       //Unchi1.transform.DORotate(new Vector3(0,0,353), 1f, RotateMode.WorldAxisAdd).SetDelay(3f);

        //Unchi2.transform.DOScale(new Vector3(2f,2f,1), 1f).SetDelay(3.5f);
        //Unchi2.transform.DORotate(new Vector3(0,0,353), 1f, RotateMode.WorldAxisAdd).SetDelay(3.5f);

        //Unchi3.transform.DOScale(new Vector3(1.6f,2f,1), 1f).SetDelay(4f);
        //Unchi3.transform.DORotate(new Vector3(0,0,353), 1f, RotateMode.WorldAxisAdd).SetDelay(4f);

        //「うんちくん」を振動させる
        // vibeUnchi1();
        //Invoke(nameof(vibeUnchi1),2f);
        //Invoke(nameof(vibeUnchi2),2.3f);
        //Invoke(nameof(vibeUnchi3),2.6f);

        // 「タイトルへ」をフェードイン
        ToTitle.GetComponent<Image>().DOFade(255f, 2000f).SetDelay(8f);


        //アプリレビュー表示
        Invoke(nameof(ShowReview), 9f);
    }


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

  //private void vibeUnchi2()
  //{
  //  var sequence = DOTween.Sequence();

  //  sequence.Append(
  //    Unchi2.transform.DORotate(new Vector3(0,0,14), 0.12f, RotateMode.WorldAxisAdd).SetDelay(4f)
  //  );
  //  sequence.Append(
  //    Unchi2.transform.DORotate(new Vector3(0,0,-14), 0.12f, RotateMode.WorldAxisAdd).SetDelay(2f)
  //  ).SetLoops(-1);

  //  sequence.Play();
  //}

  //private void vibeUnchi3()
  //{
  //  var sequence = DOTween.Sequence();

  //  sequence.Append(
  //    Unchi3.transform.DORotate(new Vector3(0,0,14), 0.12f, RotateMode.WorldAxisAdd).SetDelay(4f)
  //  );
  //  sequence.Append(
  //    Unchi3.transform.DORotate(new Vector3(0,0,-14), 0.12f, RotateMode.WorldAxisAdd).SetDelay(2f)
  //  ).SetLoops(-1);

  //  sequence.Play();
  //}


    private void ShowReview()
    {
#if UNITY_IOS
        UnityEngine.iOS.Device.RequestStoreReview();
#elif UNITY_ANDROID
        StartCoroutine(ShowReviewCoroutine());
#endif
    }


    /// <summary>
    /// Android端末でIn-App Review APIを呼ぶサンプル
    /// </summary>
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
