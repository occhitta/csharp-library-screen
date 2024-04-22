using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Occhitta.Examples.Screen.Dialog;
using Occhitta.Libraries.Screen.Data;

namespace Occhitta.Examples.Screen;

/// <summary>
/// 主要画面情報クラスです。
/// </summary>
internal sealed class MainWindowData : AbstractScreenData {
	#region メンバー変数定義
	/// <summary>
	/// 選択一覧
	/// </summary>
	private ReadOnlyCollection<Tuple<string, object>>? selectList = null;
	/// <summary>
	/// 選択情報
	/// </summary>
	private object? selectData = null;
	/// <summary>
	/// 状態日時
	/// </summary>
	private DateTime? statusTime = null;
	/// <summary>
	/// 状態内容
	/// </summary>
	private string? statusText = null;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 選択一覧を取得します。
	/// </summary>
	/// <returns>選択一覧</returns>
	public ReadOnlyCollection<Tuple<string, object>> SelectList =>
		this.selectList ??= CreateSelectList();
	/// <summary>
	/// 選択情報を取得または設定します。
	/// </summary>
	/// <value>選択情報</value>
	public object? SelectData {
		get => this.selectData;
		set => Update(ref this.selectData, value, nameof(SelectData), ActionSelectData);
	}
	/// <summary>
	/// 状態日時を取得します。
	/// </summary>
	/// <value>状態日時</value>
	public DateTime? StatusTime {
		get => this.statusTime;
		private set => Update(ref this.statusTime, value, nameof(StatusTime));
	}
	/// <summary>
	/// 状態内容を取得します。
	/// </summary>
	/// <value>状態内容</value>
	public string? StatusText {
		get => this.statusText;
		private set => Update(ref this.statusText, value, nameof(StatusText));
	}
	#endregion プロパティー定義

	#region 内部メソッド定義(選択情報関連:ActionSelectData)
	/// <summary>
	/// 選択情報を処理します。
	/// </summary>
	/// <param name="beforeData">旧情報</param>
	/// <param name="updateData">新情報</param>
	private void ActionSelectData(object? beforeData, object? updateData) {
		if (beforeData is BaseStatusData cache1) cache1.StatusHook -= ActionStatusText;
		if (updateData is BaseStatusData cache2) cache2.StatusHook += ActionStatusText;
	}
	#endregion 内部メソッド定義(選択情報関連:ActionSelectData)

	#region 内部メソッド定義(選択一覧関連:CreateSourceList/CreateSelectList)
	/// <summary>
	/// 選択集合を生成します。
	/// </summary>
	/// <returns>選択集合</returns>
	private static IEnumerable<(string, object)> CreateSourceList() {
		yield return ("異常画面", new FailureScreenData());
		yield return ("警告画面", new WarningScreenData());
		yield return ("通知画面", new MessageScreenData());
		yield return ("確認画面", new ConfirmScreenData());
	}
	/// <summary>
	/// 選択一覧を生成します。
	/// </summary>
	/// <returns>選択一覧</returns>
	private static ReadOnlyCollection<Tuple<string, object>> CreateSelectList() {
		var result = new List<Tuple<string, object>>();
		foreach (var choose in CreateSourceList()) {
			result.Add(Tuple.Create(choose.Item1, choose.Item2));
		}
		return new ReadOnlyCollection<Tuple<string, object>>(result);
	}
	#endregion 内部メソッド定義(選択一覧関連:CreateSourceList/CreateSelectList)

	#region 内部メソッド定義(状態内容関連:ActionStatusText)
	/// <summary>
	/// 状態内容を処理します。
	/// </summary>
	/// <param name="sourceData">発行情報</param>
	/// <param name="statusText">状態内容</param>
	private void ActionStatusText(object? sourceData, string? statusText) {
		StatusText = statusText;
		StatusTime = DateTime.Now;
	}
	#endregion 内部メソッド定義(状態内容関連:ActionStatusText)
}
