DesignerSafeControlSample

概要
このサンプルは、WinForms の継承コントロールで起きやすい「設計時の壊れ方」を、
実行画面から追えるように疑似再現した比較用サンプルです。

Visual Studio デザイナそのものを直接壊して見せる構成ではありません。
代わりに、設計時に起きやすい失敗を実行画面で再現し、
Broken 側と Safe 側の違いを観測しやすくしています。

このサンプルで見ること
1. static 初期化や ctor の早い段階で重い処理に触れると止まりやすい
2. 実行時だけ必要な初期化を後ろへ送ると、生成だけは通しやすい
3. DefaultValue と実際の初期値がずれると保存差が不安定になりやすい
4. TypeConverter は設計時に厳密に失敗させるより、安全値へ戻す方が通しやすい

画面の見方
Broken を疑似設計時で生成
- BrokenCaptionControl を疑似的な設計時条件で生成
- static 初期化や ctor の早い段階で止まる例を見る
- 左側の状態表示と下のログで、表面例外と原因例外を確認

Safe を疑似設計時で生成
- SafeCaptionControl を疑似的な設計時条件で生成
- 生成だけ通し、重い初期化は後ろへ送る例を見る
- 右側の状態表示で Broken 側との違いを確認

DefaultValue 保存差
- BrokenPropertyControl と SafePropertyControl の差を比較
- DefaultValue 属性と実際の初期値が一致しているかどうかで
  ShouldSerializeValue の結果がどう変わるかを見る

TypeConverter 比較
- BrokenPathSettingConverter と SafePathSettingConverter を比較
- 不正入力時に失敗で止めるか、安全値へ戻して継続するかの差を見る

見る順番
1. Broken を疑似設計時で生成
2. Safe を疑似設計時で生成
3. DefaultValue 保存差
4. TypeConverter 比較

確認ポイント
- Broken 側はどの段階で止まったか
- 表面例外ではなく、原因例外は何か
- Safe 側はどこまで通って、どこを止めたか
- 保存差や変換差がログにどう出るか

補足
このサンプルは、設計時の壊れ方を実行画面で疑似再現するためのものです。
そのため、Visual Studio デザイナ上でそのまま同じ画面を出すものではありません。

対応する記事
WinForms デザイナが落ちる原因｜継承コントロール設計の注意点【外伝G15】