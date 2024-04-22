namespace Occhitta.Libraries.Screen.Data.Dialog;

/// <summary>
/// 異常画面情報クラスです。
/// </summary>
/// <param name="headerText">表題内容</param>
/// <param name="detailData">詳細情報</param>
public sealed class FailureDialogData(string headerText, object detailData) {
	#region メンバー変数定義
	/// <summary>
	/// 表題内容
	/// </summary>
	private readonly string headerText = headerText;
	/// <summary>
	/// 詳細情報
	/// </summary>
	private readonly object detailData = detailData;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 表題内容を取得します。
	/// </summary>
	/// <value>表題内容</value>
	public string HeaderText => this.headerText;
	/// <summary>
	/// 詳細情報を取得します。
	/// </summary>
	/// <value>詳細情報</value>
	public object DetailData => this.detailData;
	#endregion プロパティー定義
}
