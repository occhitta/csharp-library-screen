using System;
using System.Threading.Tasks;
using Occhitta.Libraries.Screen.Data;
using Occhitta.Libraries.Screen.Data.Dialog;
using Occhitta.Libraries.Screen.Hook;

namespace Occhitta.Examples.Screen.Dialog;

/// <summary>
/// 待機画面情報クラスです。
/// </summary>
internal sealed class WaitingScreenData : AbstractScreenData, BaseStatusData {
	#region メンバー定数定義
	/// <summary>
	/// 表題名称
	/// </summary>
	private const string SourceName = "待機";
	/// <summary>
	/// 表示名称
	/// </summary>
	private const string DialogName = $"{SourceName}ダイアログ";
	#endregion メンバー定数定義

	#region メンバー変数定義
	/// <summary>
	/// 表示情報
	/// </summary>
	private object? dialogData = null;
	/// <summary>
	/// 表題内容
	/// </summary>
	private string? headerText = $"{SourceName}情報";
	/// <summary>
	/// 詳細内容
	/// </summary>
	private string? detailData = "○○の読込を行っています。";
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
	private DelegateScreenHeavyHook? invokeMenu = null;
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
		private set => Update(ref this.dialogData, value, nameof(DialogData));
	}
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
	/// <summary>
	/// 備考内容を取得します。
	/// </summary>
	/// <value>備考内容</value>
	public string? RemarkText {
		get => this.remarkText;
		private set => Update(ref this.remarkText, value, nameof(RemarkText));
	}
	/// <summary>
	/// 状態内容を取得します。
	/// </summary>
	/// <value>状態内容</value>
	public string? StatusText {
		get => this.statusText;
		private set => Update(ref this.statusText, value, nameof(StatusText), ActionStatusText);
	}
	/// <summary>
	/// 実行可否を取得します。
	/// </summary>
	/// <value>実行可否</value>
	/// <remarks>セッターの第四引数が「<c>() => this.invokeMenu?.Notify()</c>」ではなく「<c>>this.invokeMenu?.Notify</c>」としたいがコンパイルエラーが発生した為、回避する。</remarks>
	public bool InvokeFlag {
		get => this.invokeFlag;
		private set => Update(ref this.invokeFlag, value, nameof(InvokeFlag));
	}
	/// <summary>
	/// 実行操作を取得します。
	/// </summary>
	/// <value>実行操作</value>
	public AbstractScreenHeavyHook InvokeMenu =>
		this.invokeMenu ??= new DelegateScreenHeavyHook(ActionInvokeMenu, () => this.invokeFlag);
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

	#region 実装イベント定義(BaseStatusData)
	/// <summary>
	/// 状態処理を追加または削除します。
	/// </summary>
	/// <value>状態処理<value>
	event EventHandler<string?> BaseStatusData.StatusHook {
		add    => this.listenList += value;
		remove => this.listenList -= value;
	}
	#endregion 実装イベント定義(BaseStatusData)

	#region 内部メソッド定義(状態内容関連:ActionStatusText)
	/// <summary>
	/// 状態内容を処理します。
	/// <para>上位情報へ状態内容を通知する処理を行います。</para>
	/// </summary>
	private void ActionStatusText() =>
		this.listenList?.Invoke(this, this.statusText);
	#endregion 内部メソッド定義(状態内容関連:ActionStatusText)

	#region 継承メソッド定義(ActionInvokeMenu/ActionCancelMenu)
	/// <summary>
	/// 実行操作を実行します。
	/// </summary>
	/// <remarks>本来は入力情報が空欄かチェックするが空欄の表示を確認する意図で空欄を許可する</remarks>
	protected async Task ActionInvokeMenu() {
		var headerText = this.headerText ?? String.Empty;
		var detailData = this.detailData ?? String.Empty;
		var dialogData = new WaitingDialogData() {
			HeaderText = headerText,
			DetailData = detailData
		};
		DialogData = dialogData;
		StatusText = $"{DialogName}表示";

	}
	/// <summary>
	/// 取消操作を実行します。
	/// </summary>
	protected void ActionCancelMenu() {
		DialogData = null;
		StatusText = $"{DialogName}取消";
	}
	#endregion 継承メソッド定義(ActionInvokeMenu/ActionCancelMenu)
}
