using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{

    private BannerView HeaderBannerView;
    private BannerView FooterBannerView;
    private BannerView squareBannerView;
    private RewardedAd rewardedAd;

    public HintManager HintClass;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        //画面上バナー
        this.RequestHeaderBanner();
        //画面下バナー
        this.RequestFooterBanner();
        //リワード広告(動画)
        this.RequestReward();
    }

    //**********************************
    //**バナー広告(ヘッダー)
    //**********************************

    //画面上のバナーの表示
    private void RequestHeaderBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7464443980940177/8321088359";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/8358130265";
#else
        //テスト
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        // Create a 320x50 banner at the top of the screen.
        HeaderBannerView = new BannerView(adUnitId, adSize, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        HeaderBannerView.LoadAd(request);
    }

    //**********************************
    //**バナー広告(フッター)
    //**********************************

    //画面下のバナーの表示
    private void RequestFooterBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7464443980940177/4190271650";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/6931985063";
#else
        //テスト
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        //バナーサイズ
        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        // Create a 320x50 banner at the top of the screen.
        FooterBannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        FooterBannerView.LoadAd(request);
    }

    //**********************************
    //**バナー広告(Menu画面内の長方形)
    //**********************************
    //Menu画面内のバナーの表示
    public void RequestSquareBanner()
    {
#if UNITY_ANDROID
         string adUnitId = "ca-app-pub-7464443980940177/8417810790";
#elif UNITY_IOS
        string adUnitId = "ca-app-pub-7464443980940177/3740199180";
#else
        //テスト
         string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#endif

        // Create a banner at the top of the screen.
        this.squareBannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Center);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.squareBannerView.LoadAd(request);
    }

    //Menu画面内のバナーの非表示
    public void unRequestSquareBanner()
    {
        squareBannerView.Destroy();
    }

    //**********************************
    //**リワード広告
    //**********************************

    //動画のロード
    private void RequestReward()
    {
        string adUnitId = "";
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-7464443980940177/7937944979";  //本番
#elif UNITY_IOS
        adUnitId = "ca-app-pub-7464443980940177/4764986725";  //本番
#else
        //テスト
        adUnitId = "ca-app-pub-3940256099942544/1712485313";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);
        //動画の視聴が完了したら「HandleUserEarnedReward」を呼ぶ
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // 広告が閉じたとき「HandleRewardedAdClosed」を呼ぶ
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    //動画の視聴開始
    public void ShowReawrd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    //動画視聴完了後の処理（途中で閉じられた場合は呼ばれない）
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //ヒントを表示
        HintClass.AfterWatch();
    }

    // 広告が閉じたときに呼び出されます
    public void HandleRewardedAdClosed(object sender, System.EventArgs args)
    {
        //リワード広告の再ロード
        this.RequestReward();
    }
}
