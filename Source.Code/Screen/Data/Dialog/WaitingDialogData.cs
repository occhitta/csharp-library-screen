namespace Occhitta.Libraries.Screen.Data.Dialog;

/// <summary>
/// 待機画面情報クラスです。
/// <para>当該情報は起動時の画面表示を想定しています。</para>
/// </summary>
public sealed class WaitingDialogData : AbstractScreenData {
	#region メンバー変数定義
	/// <summary>
	/// 表題内容
	/// </summary>
	private string? headerText = null;
	/// <summary>
	/// 詳細情報
	/// </summary>
	private object? detailData = null;
	/// <summary>
	/// 進捗割合
	/// </summary>
	private double? outputData = null;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 表題内容を取得または設定します。
	/// </summary>
	/// <value>表題内容</value>
	public string? HeaderText {
		get => this.headerText;
		set => Update(ref this.headerText, value, nameof(HeaderText));
	}
	/// <summary>
	/// 詳細情報を取得または設定します。
	/// </summary>
	/// <value>詳細情報</value>
	public object? DetailData {
		get => this.detailData;
		set => Update(ref this.detailData, value, nameof(DetailData));
	}
	/// <summary>
	/// 進捗割合を取得または設定します。
	/// <para>想定としては「0.0」から「1.0」の範囲で指定</para>
	/// </summary>
	/// <value>進捗割合</value>
	public double? OutputData {
		get => this.outputData;
		set => Update(ref this.outputData, value, nameof(OutputData));
	}
	#endregion プロパティー定義
}
