using System;
using Occhitta.Libraries.Screen.Data.Dialog;

namespace Occhitta.Examples.Screen.Dialog;

/// <summary>
/// 異常画面情報クラスです。
/// </summary>
internal sealed class FailureScreenData : ElementScreenData {
	#region メンバー定数定義
	/// <summary>
	/// 表題名称
	/// </summary>
	private const string SourceName = "異常";
	/// <summary>
	/// 表示名称
	/// </summary>
	private const string DialogName = $"{SourceName}ダイアログ";
	#endregion メンバー定数定義

	#region メンバー変数定義
	/// <summary>
	/// 表題内容
	/// </summary>
	private string? headerText = $"{SourceName}情報";
	/// <summary>
	/// 詳細内容
	/// </summary>
	private string? detailData = "メッセージが○○様より届いております。\r\nグループウェアより確認してください。";
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
	/// 詳細内容を取得または設定します。
	/// </summary>
	/// <value>詳細内容</value>
	public string? DetailData {
		get => this.detailData;
		set => Update(ref this.detailData, value, nameof(DetailData));
	}
	#endregion プロパティー定義

	#region 継承メソッド定義(ActionInvokeMenu/ActionCancelMenu)
	/// <summary>
	/// 実行操作を実行します。
	/// </summary>
	/// <remarks>本来は入力情報が空欄かチェックするが空欄の表示を確認する意図で空欄を許可する</remarks>
	protected override void ActionInvokeMenu() {
		var headerText = this.headerText ?? String.Empty;
		var detailData = this.detailData ?? String.Empty;
		var dialogData = new FailureDialogData(headerText, detailData);
		DialogData = dialogData;
		StatusText = $"{DialogName}表示";
	}
	/// <summary>
	/// 取消操作を実行します。
	/// </summary>
	protected override void ActionCancelMenu() {
		DialogData = null;
		StatusText = $"{DialogName}取消";
	}
	#endregion 継承メソッド定義(ActionInvokeMenu/ActionCancelMenu)
}
