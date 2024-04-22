using System;
using Occhitta.Libraries.Screen.Data.Dialog;

namespace Occhitta.Examples.Screen.Dialog;

/// <summary>
/// 通知画面情報クラスです。
/// </summary>
internal sealed class ConfirmScreenData : ElementScreenData {
	#region メンバー定数定義
	/// <summary>
	/// 表題名称
	/// </summary>
	private const string SourceName = "確認";
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
	private string? detailData = "メッセージが○○様より届いております。\r\nグループウェアを表示しますか？";
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

	#region 内部メソッド定義(ChooseSelectText/ActionSelectMenu)
	/// <summary>
	/// 選択内容を抽出します。
	/// </summary>
	/// <param name="source">選択情報</param>
	/// <returns>選択内容</returns>
	private static string ChooseSelectText(bool? source) => source switch {
		true  => "許可",
		false => "否定",
		_     => "保留",
	};
	/// <summary>
	/// 選択情報を処理します。
	/// </summary>
	/// <param name="dialogData">発行情報</param>
	/// <param name="optionData">引数情報</param>
	/// <remarks>ダイアログにてボタンが押下された場合に実行される</remarks>
	private void ActionSelectMenu(object? dialogData, EventArgs optionData) {
		if (dialogData is ConfirmDialogData chooseData) {
			chooseData.SelectHook -= ActionSelectMenu;
			StatusText = $"{DialogName}終了({ChooseSelectText(chooseData.SelectData)})";
		}
		DialogData = null;
	}
	#endregion 内部メソッド定義(ChooseSelectText/ActionSelectMenu)

	#region 継承メソッド定義(ActionInvokeMenu/ActionCancelMenu)
	/// <summary>
	/// 実行操作を実行します。
	/// </summary>
	/// <remarks>本来は入力情報が空欄かチェックするが空欄の表示を確認する意図で空欄を許可する</remarks>
	protected override void ActionInvokeMenu() {
		var headerText = this.headerText ?? String.Empty;
		var detailData = this.detailData ?? String.Empty;
		var dialogData = new ConfirmDialogData(headerText, detailData);
		dialogData.SelectHook += ActionSelectMenu;
		DialogData = dialogData;
		StatusText = $"{DialogName}表示";
	}
	/// <summary>
	/// 取消操作を実行します。
	/// </summary>
	protected override void ActionCancelMenu() {
		if (DialogData is ConfirmDialogData dialogData) dialogData.SelectHook -= ActionSelectMenu;
		DialogData = null;
		StatusText = $"{DialogName}取消";
	}
	#endregion 継承メソッド定義(ActionInvokeMenu/ActionCancelMenu)
}
