using System;
using Occhitta.Libraries.Screen.Data;
using Occhitta.Libraries.Screen.Hook;

namespace Occhitta.Examples.Screen.Dialog;

/// <summary>
/// 要素画面情報クラスです。
/// <para>ダイアログ操作における共通処理を行います。</para>
/// </summary>
internal abstract class ElementScreenData : AbstractScreenData, BaseStatusData {
	#region メンバー変数定義
	/// <summary>
	/// 表示情報
	/// </summary>
	private object? dialogData = null;
	/// <summary>
	/// 備考内容
	/// </summary>
	private string? remarkText = null;
	/// <summary>
	/// 状態内容
	/// </summary>
	private string? statusText = null;
	/// <summary>
	/// 実行可否
	/// </summary>
	private bool invokeFlag = true;
	/// <summary>
	/// 実行操作
	/// </summary>
	private DelegateScreenLightHook? invokeMenu = null;
	/// <summary>
	/// 取消可否
	/// </summary>
	private bool cancelFlag = false;
	/// <summary>
	/// 取消操作
	/// </summary>
	private DelegateScreenLightHook? cancelMenu = null;
	/// <summary>
	/// 状態処理
	/// </summary>
	private EventHandler<string?>? listenList = null;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 表示情報を取得します。
	/// </summary>
	/// <value>表示情報</value>
	public object? DialogData {
		get => this.dialogData;
		protected set => Update(ref this.dialogData, value, nameof(DialogData), ActionDialogData);
	}
	/// <summary>
	/// 備考内容を取得します。
	/// </summary>
	/// <value>備考内容</value>
	public string? RemarkText {
		get => this.remarkText;
		protected set => Update(ref this.remarkText, value, nameof(RemarkText));
	}
	/// <summary>
	/// 状態内容を取得します。
	/// </summary>
	/// <value>状態内容</value>
	public string? StatusText {
		get => this.statusText;
		protected set => Update(ref this.statusText, value, nameof(StatusText), ActionStatusText);
	}
	/// <summary>
	/// 実行可否を取得します。
	/// </summary>
	/// <value>実行可否</value>
	/// <remarks>セッターの第四引数が「<c>() => this.invokeMenu?.Notify()</c>」ではなく「<c>>this.invokeMenu?.Notify</c>」としたいがコンパイルエラーが発生した為、回避する。</remarks>
	public bool InvokeFlag {
		get => this.invokeFlag;
		private set => Update(ref this.invokeFlag, value, nameof(InvokeFlag), () => this.invokeMenu?.Notify());
	}
	/// <summary>
	/// 実行操作を取得します。
	/// </summary>
	/// <value>実行操作</value>
	public AbstractScreenLightHook InvokeMenu =>
		this.invokeMenu ??= new DelegateScreenLightHook(ActionInvokeMenu, () => this.invokeFlag);
	/// <summary>
	/// 取消可否を取得します。
	/// </summary>
	/// <value>取消可否</value>
	/// <remarks>セッターの第四引数が「<c>() => this.invokeMenu?.Notify()</c>」ではなく「<c>>this.invokeMenu?.Notify</c>」としたいがコンパイルエラーが発生した為、回避する。</remarks>
	public bool CancelFlag {
		get => this.cancelFlag;
		private set => Update(ref this.cancelFlag, value, nameof(CancelFlag), () => this.cancelMenu?.Notify());
	}
	/// <summary>
	/// 取消操作を取得します。
	/// </summary>
	/// <value>取消操作</value>
	public AbstractScreenLightHook CancelMenu =>
		this.cancelMenu ??= new DelegateScreenLightHook(ActionCancelMenu, () => this.cancelFlag);
	#endregion プロパティー定義

	#region 実装イベント定義(状態通知関連:StatusHook)
	/// <summary>
	/// 状態処理を追加または削除します。
	/// </summary>
	event EventHandler<string?>? BaseStatusData.StatusHook {
		add    => this.listenList += value;
		remove => this.listenList -= value;
	}
	#endregion 実装イベント定義(状態通知関連:StatusHook)

	#region 内部メソッド定義(表示情報関連:ActionDialogData)
	/// <summary>
	/// 表示情報を処理します。
	/// </summary>
	private void ActionDialogData() {
		if (this.dialogData == null) {
			// 表示情報が解除された場合：実行操作を有効化
			CancelFlag = false; // 取消不可
			InvokeFlag = true;  // 実行可能
		} else {
			// 表示情報が設定された場合：取消操作を有効化
			InvokeFlag = false; // 実行不可
			CancelFlag = true;  // 実行可能
		}
	}
	#endregion 内部メソッド定義(表示情報関連:ActionDialogData)

	#region 内部メソッド定義(状態内容関連:ActionStatusText)
	/// <summary>
	/// 状態内容を処理します。
	/// <para>上位情報へ状態内容を通知する処理を行います。</para>
	/// </summary>
	private void ActionStatusText() =>
		this.listenList?.Invoke(this, this.statusText);
	#endregion 内部メソッド定義(状態内容関連:ActionStatusText)

	#region 移譲メソッド定義(画面操作関連:ActionInvokeMenu/ActionCancelMenu)
	/// <summary>
	/// 実行操作を実行します。
	/// </summary>
	protected abstract void ActionInvokeMenu();
	/// <summary>
	/// 取消操作を実行します。
	/// </summary>
	protected abstract void ActionCancelMenu();
	#endregion 移譲メソッド定義(画面操作関連:ActionInvokeMenu/ActionCancelMenu)
}
