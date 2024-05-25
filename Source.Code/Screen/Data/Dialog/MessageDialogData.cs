using System;
using System.Diagnostics.Contracts;
using Occhitta.Libraries.Screen.Hook;

namespace Occhitta.Libraries.Screen.Data.Dialog;

/// <summary>
/// 通知画面情報クラスです。
/// </summary>
/// <param name="headerText">表題内容</param>
/// <param name="detailData">詳細情報</param>
/// <param name="acceptText">許可名称</param>
public sealed class MessageDialogData(string headerText, object detailData, string acceptText = "は　い") {
	#region メンバー変数定義
	/// <summary>
	/// 表題内容
	/// </summary>
	private readonly string headerText = headerText;
	/// <summary>
	/// 詳細情報
	/// </summary>
	private readonly object detailData = detailData;
	/// <summary>
	/// 許可名称
	/// </summary>
	private readonly string acceptText = acceptText;
	/// <summary>
	/// 許可操作
	/// </summary>
	private DelegateScreenLightHook? acceptMenu = null;
	/// <summary>
	/// 通知一覧
	/// </summary>
	private EventHandler? selectHook = null;
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
	/// <summary>
	/// 許可名称を取得します。
	/// </summary>
	/// <value>許可名称</value>
	public string AcceptText => this.acceptText;
	/// <summary>
	/// 許可操作を取得します。
	/// </summary>
	/// <value>選択操作</value>
	public AbstractScreenLightHook AcceptMenu => this.acceptMenu ??= new DelegateScreenLightHook(ActionSelectMenu);
	#endregion プロパティー定義

	#region 公開イベント定義
	/// <summary>
	/// 選択処理を追加または削除します。
	/// </summary>
	public event EventHandler? SelectHook {
		add    => this.selectHook += value;
		remove => this.selectHook -= value;
	}
	#endregion 公開イベント定義

	#region 内部メソッド定義
	/// <summary>
	/// 選択操作を処理します。
	/// </summary>
	private void ActionSelectMenu() =>
		this.selectHook?.Invoke(this, EventArgs.Empty);
	#endregion 内部メソッド定義
}
