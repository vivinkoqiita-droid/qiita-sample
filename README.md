# qiita-sample

Qiita 記事で扱ったサンプルコード置き場です。  
C#、.NET、WinForms を中心に、手元で動かして確認しやすい小さめのサンプルをまとめています。

## このリポジトリの見方

- サンプルは記事ごとにフォルダを分けています
- 最初にこの README で対象フォルダを見つけ、その後で各フォルダ配下の `README.md` を見る流れが分かりやすいです
- 実行手順、確認ポイント、補足は各サンプル側の `README.md` に書いています

## サンプル一覧

| フォルダ名 | 内容 | 技術 | 記事 | 補足 |
|---|---|---|---|---|
| `K35_LinqSample` | LINQ の動きや書き方を確認するサンプル | C# / LINQ / WinForms | [LINQ サンプル](https://qiita.com/vivinko/items/a0ba89eda3f48c8a848f) | 条件ごとの絞り込みや結果確認向け |
| `K39_ListDictionaryHashSetBench` | `List<T>` / `Dictionary<TKey, TValue>` / `HashSet<T>` の実測比較サンプル | C# / .NET 8 / WinForms | [10万件で比べる List / Dictionary / HashSet](https://qiita.com/vivinko/items/07b410062cc4b358c7d5) | `Contains`、ID検索、更新、重複排除、除外、突合を比較 |
| `W01_FileSystemObserver` | ファイル変更監視の基本を確認するサンプル | C# / .NET Framework / WinForms / FileSystemWatcher | 未公開 | 監視開始・停止、イベント確認向け |
| `W02_ShareUpdateAuditLoggerSample` | ファイル更新者検知のサンプル | C# / .NET Framework / WinForms / FileSystemWatcher / Security Event Log | 未公開 | 共有フォルダ監視、更新ログ出力、更新者確認向け。監査設定と管理者権限が前提 |

## 見る順

1. Qiita の記事で概要を確認
2. この README から対象サンプルを選ぶ
3. 対象フォルダ配下の `README.md` で前提、使い方、確認ポイントを見る
4. その後でソースコードを見る

## 動作環境

- Windows
- Visual Studio
- 基本は .NET 8 のサンプルです
- `W01_FileSystemObserver` と `W02_ShareUpdateAuditLoggerSample` は .NET Framework のサンプルです
- 実行手順や補足は各フォルダ配下の `README.md` を見てください

## 注意

- 学習用、検証用のサンプルを含みます
- 実運用へ入れる前には例外処理、ログ、再試行、権限、運用条件の確認が別途要ります
- 画面サンプルは理解しやすさを優先しています

## フォルダ構成

```text
qiita-sample/
├─ README.md
├─ K35_LinqSample/
├─ K39_ListDictionaryHashSetBench/
├─ W01_FileSystemObserver/
└─ W02_ShareUpdateAuditLoggerSample/
```