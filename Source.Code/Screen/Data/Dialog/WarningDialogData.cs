using System;
using Occhitta.Libraries.Screen.Hook;

namespace Occhitta.Libraries.Screen.Data.Dialog;

/// <summary>
/// 警告画面情報クラスです。
/// </summary>
/// <param name="headerText">表題内容</param>
/// <param name="detailData">詳細情報</param>
public sealed class WarningDialogData(string headerText, object detailData) : AbstractScreenData {
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
	/// 許可操作
	/// </summary>
	private DelegateScreenLightHook? acceptMenu = null;
	/// <summary>
	/// 拒否操作
	/// </summary>
	private DelegateScreenLightHook? cancelMenu = null;
	/// <summary>
	/// 選択情報
	/// </summary>
	private bool selectData = false;
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
	/// 許可操作を取得します。
	/// </summary>
	/// <value>許可操作</value>
	public AbstractScreenLightHook AcceptMenu => this.acceptMenu ??= new DelegateScreenLightHook(ActionAcceptMenu);
	/// <summary>
	/// 拒否操作を取得します。
	/// </summary>
	/// <value>拒否操作</value>
	public AbstractScreenLightHook CancelMenu => this.cancelMenu ??= new DelegateScreenLightHook(ActionCancelMenu);
	/// <summary>
	/// 選択情報を取得します。
	/// </summary>
	/// <value>選択情報</value>
	public bool SelectData {
		get => this.selectData;
		private set => Update(ref this.selectData, value, nameof(SelectData));
	}
	#endregion プロパティー定義

	#region 公開イベント定義
	/// <summary>
	/// 警告処理を追加または削除します。
	/// </summary>
	public event EventHandler? SelectHook {
		add    => this.selectHook += value;
		remove => this.selectHook -= value;
	}
	#endregion 公開イベント定義

	#region 内部メソッド定義
	/// <summary>
	/// 許可操作を実行します。
	/// </summary>
	private void ActionAcceptMenu() {
		SelectData = true;
		this.selectHook?.Invoke(this, EventArgs.Empty);
	}
	/// <summary>
	/// 拒否操作を実行します。
	/// </summary>
	private void ActionCancelMenu() {
		SelectData = false;
		this.selectHook?.Invoke(this, EventArgs.Empty);
	}
	#endregion 内部メソッド定義
}
