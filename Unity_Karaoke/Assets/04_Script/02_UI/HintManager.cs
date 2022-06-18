using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    /// <summary>
    /// 開発モード(true:ヒントオープン, false:動画広告)
    /// </summary>
    private bool DebugMode = false;


    //ゲーム進捗
    private int Progress;
    //現進捗のヒントフラグ (例:"0100")
    private string HintFlg;
    //現進捗で動画を視聴した回数
    private int CountWatch;

    //スタートリセットオブジェクト
    public StartResetManager StartResetClass;
    //広告オブジェクト
    public GoogleAds GoogleAds;

    //ヒント親オブジェクト
    public GameObject Hint3;
    public GameObject Hint4;

    //ヒントテキスト
    public Text HintTxt1;
    public Text HintTxt2;
    public Text HintTxt3;
    public Text HintTxt4;

    //ヒントテキストオブジェクト
    public GameObject TxtObject2;
    public GameObject TxtObject3;
    public GameObject TxtObject4;

    //動画説明&ボタンの親オブジェクト
    public GameObject Ads2;
    public GameObject Ads3;
    public GameObject Ads4;


    //「動画視聴しますか」ボタン
    public GameObject BtnAds2;
    public GameObject BtnAds3;
    public GameObject BtnAds4;



    //<summary>
    //ヒントテキスト(進捗(step),ヒント文字列配列)
    //</summary>
    private Dictionary<int, string[]> HintDB = new Dictionary<int, string[]>
     {
       {
         1, //ハンガーセット
         new string[]{
           //ヒント1
           "ソファにあるハンガーは拾って、どこに掛けてみようかな",
           //ヒント2
           "壁にあるハンガーラックに、１つだけハンガーがないなあ",
           //ヒント3
           "ハンガーラックの右から2番目のフックにハンガーを掛けよう",
         }
       },
       {
         2, //ハンガー4つの回転
         new string[]{
           //ヒント1
           "ハンガー4つはそれぞれ回転するようだ",
           //ヒント2
           "ハンガーのフックに似た形が部屋のどこかにあったはず",
           //ヒント3
           "壁にある絵には白鳥が4羽かかれているなあ",
           //ヒント4
           "白鳥の向きの通りにハンガーのフックの向きも合わせよう",
         }
       },
       {
         3, //ストロー挿す
         new string[]{
           //ヒント1
           "引き出しから出てきたストローはどうやって使おうかな",
           //ヒント2
           "ストローといえば・・・　ストローがささっていないコップがあるみたいだ",
           //ヒント3
           "ゲットしたストローは、ストローがないコップに使おう",
         }
       },
       {
         4, //コップ回転
         new string[]{
           //ヒント1
           "２つのコップは、部屋のどこかにもないだろうか",
           //ヒント2
           "本をめくるとこの部屋とよく似たページが・・・",
           //ヒント3
           "このページにも同じようにコップが2つあるみたいだ",
           //ヒント4
           "2つのコップを回転して、本の中のコップと同じ向きにそろえよう！",
         }
       },
       {
         5, //タンバリン並べ
         new string[]{
           //ヒント1
           "タンバリン３つは、机の上に置けそうだ",
           //ヒント2
           "タンバリンはある順番に置く必要があるみたいだ。タンバリンの形と何か関係があるのかな",
           //ヒント3
           "部屋のカドのゴミ箱、何かあやしい・・・　のぞいてみよう",
           //ヒント4
           "ゴミ箱の中の△〇□の通りに、タンバリンを並べよう",
         }
       },
       {
         7, //エアコンのリモコン
         new string[]{
           //ヒント1
           "エアコンのリモコンには、右と左の三角ボタンがある。この形どこかでも見たような",
           //ヒント2
           "カラオケ機が入った棚の扉に、リモコンのボタンと似ているマークがあるみたい",
           //ヒント3
           "マークをよく見ると右三角と左三角が縦に並んでいるように見える",
           //ヒント4
           "マークは上から右、左、右、右、左、左と並んでいる。この順番でリモコンのボタンを押してみよう",
         }
       },
       {
         8, //デンモクのロック解除
         new string[]{
           //ヒント1
           "デンモク画面がロック中・・・どうやら4桁の数字が必要のようだ",
           //ヒント2
           "数字を入れる4つの枠は、テレビ画面に表示されている四角とよく似ているみたい",
           //ヒント3
           "テレビにある四角の色は、数字の色を表しているみたいだ。たしか部屋中にいろんな色の数字があった気がする",
           //ヒント4
           "部屋中からそれぞれの色の数字を並べると6278。これでロックは解除されるはずだ",
         }
       },
       {
         9, //星の力
         new string[]{
           //ヒント1
           "デンモクに「曲番号」を入れると曲を予約できるみたい。予約したいけど、曲番号はどこに書いてあるだろうか・・・",
           //ヒント2
           "机の上に確か分厚い本が置いてあったような",
           //ヒント3
           "本には曲番号が書いてる。見覚えのある曲が1曲だけないかな？",
           //ヒント4
           "カラオケ機本体に表示されている「星の力」を予約してみよう。曲番号は77-283だ",
         }
       },
       {
         10, //1歩1歩
         new string[]{
           //ヒント1
           "デンモクの「りれき」を見てみよう！「りれき」からも曲が予約できるようだ",
           //ヒント2
           "「りれき」に見覚えのある曲がひとつだけないだろうか",
           //ヒント3
           "カラオケ機本体に表示されている「1歩1歩」を予約してみよう",
         }
       },
       {
         11, //九州Lovers
         new string[]{
           //ヒント1
           "デンモクの「歌手名」からも曲を予約できるようだ",
           //ヒント2
           "歌手名が出てこない・・・　部屋のどこかに歌手名がわかるものがないかなあ",
           //ヒント3
           "壁にはある歌手のポスターが貼られている。「新曲の配信スタート」か、せっかくなら歌ってみようかな",
           //ヒント4
           "歌手名で「きゃなる」と検索すると、きゃなるしてぃ〜ズの曲が予約できるよ",
         }
       },
       {
         12, //カラオケ機のボタン
         new string[]{
           //ヒント1
           "「星の力」の最初・・・　歌詞の出だしのことかな",
           //ヒント2
           "「1歩1歩」の最後・・・　曲の終わりはどんな歌詞だったっけ",
           //ヒント3
           "「九州Lovers」のデュエット・・・　「しりとりでは負ける」は「ん」で終わるということかな",
           //ヒント4
           "カラオケ機の3つのボタンは上から、りんご、雨、ペンギンで合わせるとOK",
         }
       },
       {
         13, //1000円注文
         new string[]{
           //ヒント1
           "ポテトフライが50円！お得だから注文してみよう",
           //ヒント2
           "注文の合計金額が1000円だとプレゼントか・・・  せっかくなら欲しい",
           //ヒント3
           "ポテトフライ50円とピザ950円を注文すれば、合計1000円だ",
         }
       },
       {
         14, //電話裏のボタン
         new string[]{
           //ヒント1
           "電話の横の絵３つはどこかで見たはず。テレビ画面だったかなあ",
           //ヒント2
           "曲を予約すると最後に出てくる採点画面に同じ絵があるよ",
           //ヒント3
           "3つの絵は採点画面の表現力、安定性、ビブラートと同じみたいだ",
           //ヒント4
           "採点画面からそれぞれの絵の数字を読みとると、ボタンを上から4、５、３にするとOK",
         }
       },
       {
         16, //デンモク再起動
         new string[]{
           //ヒント1
           "鍵が入った箱をひっくり返すと見覚えのある四角が4つ・・・　どこで使っただろうか",
           //ヒント2
           "前はたしかデンモクのロック画面で数字4つを入力したはず。ただ、今はもうロックが解除されているなあ",
           //ヒント3
           "もう1度ロック画面にしたいけど、どうすればいいだろうか。そういえば、どこかにデンモクの使い方が書かれていなような",
           //ヒント4
           "カラオケ機が入っている棚にデンモクの使い方が書かれている。デンモクの電源を1度切って、入れ直すとロック画面が現れるみたいだ",
         }
       },
       {
         17, //デンモクロック解除2回目
         new string[]{
           //ヒント1
           "鍵が入った箱の裏側の四角4つは、前と同じようにデンモクのロック画面と同じみたいだ",
           //ヒント2
           "四角のそれぞれの色は数字の色を表してる。部屋中に散りばめられている数字から同じ色を探そう",
           //ヒント3
           "黄色とピンクの数字は見つかって、緑も引出しの中にあるなあ。赤の数字・・・ない？いいや、どこかで見ているはず。例えばテレビとか",
           //ヒント4
           "黄色はテーブルの下。ピンクは絵の中。緑は引出し。赤はテレビ画面に。答えは2369だ！",
         }
       },
       {
         18, //ドライバーで箱開ける
         new string[]{
           //ヒント1
           "ドライバーをゲットしたら、やることはたった１つ。ネジを回すだけ",
           //ヒント2
           "ドライバーを使って、鍵が入った鍵の箱を開けよう",
           //ヒント3
           "鍵が入った箱のアイテムを拡大したら、ドライバーを選択して、鍵が入った箱をタップすると開けれるようだ",
         }
       },
       {
         20, //こしょう少々
         new string[]{
           //ヒント1
           "廊下の壁にある「こしょう」・・・　黄色と青の枠線はどこかで見たような",
           //ヒント2
           "黄色と青の枠線はたしかデンモクのどこかの画面にあったはず",
           //ヒント3
           "デンモクの歌手名の画面にも「こしょう」と同じ枠線があるみたいだ",
           //ヒント4
           "歌手名で「こしょう」と検索すると、こしょう少々のふたつまみという曲がヒットする。とりあえず予約してみよう",
         }
       },
       {
         21, //五角形パズル
         new string[]{
           //ヒント1
           "５つの木のパーツは丸型の中に置けてパズルみたいだ。丸の中には五角形が描かれているけど・・・",
           //ヒント2
           "五角形はどこかでも見たような。テレビの採点画面だったかな",
           //ヒント3
           "「ふたつまみ」の採点画面にも五角形があるけど、パズルとどんな関係があるだろうか",
           //ヒント4
           "「ふたつまみ」の採点画面に描かれている青色の五角形と同じ形を、パズルで作るとOK",
         }
       },
       {
         22, //最後の扉のボタン
         new string[]{
           //ヒント1
           "廊下の突き当たりの扉のボタンは、4つの部屋と何か関係がありそうだ。右下の押せないボタンはさっき出てきた部屋のことかな",
           //ヒント2
           "ボタンを見ると扉が開いている絵のようにも見える。出てきた部屋は、部屋方向に開くから右下の絵と一致しているなあ",
           //ヒント3
           "他の部屋は動かないから、どの向きに開くかわからない・・・　閉まったままでも開く向きがわからないだろうか",
           //ヒント4
           "各扉の金具に注目するとどちらに扉が開くかわかるようだ！101は奥へ、102と103は手前へ。ボタンをこの向きに合わせよう",
         }
       },
       {
         23, //あとは脱出するだけ
         new string[]{
           //ヒント1
           "あとは鍵を使って脱出するだけ！",
           //ヒント2
           "どうした？あとは鍵を使って脱出するだけですぞ？",
           //ヒント3
           "何を戸惑っている？やっとここまできたではないか！さぁ脱出だ！",
           //ヒント4
           "さては他に謎がないか疑っているな？残念だが、謎は全てクリアされてしまった… 次回作でお会いしよう！さぁ、脱出だ！",
         }
       },
     };

    // Start is called before the first frame update
    void Start()
    {
        BtnAds2.GetComponent<Button>().onClick.AddListener(() =>
        {
            GoogleAds.ShowReawrd();
        });
        BtnAds3.GetComponent<Button>().onClick.AddListener(() =>
        {
            GoogleAds.ShowReawrd();
        });
        BtnAds4.GetComponent<Button>().onClick.AddListener(() =>
        {
            GoogleAds.ShowReawrd();
        });

        //テキスト表示状態を初期化
        ResetHint();
    }


    //<summary>
    //ヒントをヒントテキストにセットし、動画視聴数に応じてテキスト表示・非表示する
    //</summary>
    public void SetHint()
    {
        //ゲーム進捗を取得
        Progress = StartResetClass.CheckProgress();

        //現進捗のヒントフラグを取得
        if (DebugMode)
            HintFlg = "1111";
        else
            HintFlg = SaveLoadSystem.Instance.gameData.HintFlgArray[Progress];

        //ヒント1,2,3,4にテキストをセットする
        HintTxt1.text = HintDB[Progress][0];
        HintTxt2.text = HintDB[Progress][1];
        HintTxt3.text = HintDB[Progress][2];
        HintTxt4.text = HintDB[Progress].Length == 4 ? HintDB[Progress][3] : "";

        //動画視聴した数を取得
        CountWatch = 3;
        for (int i = 1; i < HintDB[Progress].Length; i++)
        {
            if (HintFlg.Substring(i, 1) == "0")
            {
                CountWatch = i - 1;
                break;
            }
        }

        //動画視聴数に合わせてオブジェクトを表示・非表示
        ShowHint();
        
    }


    //<summary>
    //動画視聴後のヒント表示・非表示
    //</summary>
    public void AfterWatch()
    {
        CountWatch++;
        ShowHint();

        //セーブデータのフラグ更新
        string NewHintFlg = "0000";

        if (CountWatch == 1)
            NewHintFlg = "0100";
        else if (CountWatch == 2)
            NewHintFlg = "0110";
        else if (CountWatch == 3)
            NewHintFlg = "0111";

        SaveLoadSystem.Instance.gameData.HintFlgArray[Progress] = NewHintFlg;
        SaveLoadSystem.Instance.Save();

    }



    //<summary>
    //ヒントをヒントテキストにセットし、動画視聴数に応じてテキスト表示・非表示する
    //</summary>
    public void ShowHint()
    {
        //ヒント2の動画を視聴済みの場合
        if (CountWatch >= 1)
        {
            Ads2.SetActive(false);
            TxtObject2.SetActive(true);
            Hint3.SetActive(true);
        }
        //ヒント3の動画を視聴済みの場合
        if (CountWatch >= 2)
        {
            Ads3.SetActive(false);
            TxtObject3.SetActive(true);

            //ヒント4まである場合
            if (HintDB[Progress].Length == 4)
                Hint4.SetActive(true);
        }
        //ヒント4の動画を視聴済みの場合
        if (CountWatch >= 3)
        {
            Ads4.SetActive(false);
            TxtObject4.SetActive(true);
        }
    }


    //<summary>
    //ヒント画面を閉じるときにヒントを初期状態にリセットする
    //</summary>
    public void ResetHint()
    {
        Ads2.SetActive(true);
        Ads3.SetActive(true);
        Ads4.SetActive(true);

        TxtObject2.SetActive(false);
        TxtObject3.SetActive(false);
        TxtObject4.SetActive(false);

        Hint3.SetActive(false);
        Hint4.SetActive(false);
    }
}
