using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Occhitta.Libraries.Screen.Data;

/// <summary>
/// 基底画面一覧クラスです。
/// </summary>
/// <typeparam name="TRecord">要素種別</typeparam>
public abstract class AbstractScreenList<TRecord> : AbstractScreenData, IReadOnlyList<TRecord>, INotifyCollectionChanged {
	#region メンバー変数定義
	/// <summary>
	/// 要素一覧
	/// </summary>
	private readonly List<TRecord> sourceList;
	/// <summary>
	/// 通知管理
	/// </summary>
	private NotifyCollectionChangedEventHandler? listenList;
	#endregion メンバー変数定義

	#region プロパティー定義
	/// <summary>
	/// 要素個数を取得します。
	/// </summary>
	/// <value>要素個数</value>
	public int Count => this.sourceList.Count;
	/// <summary>
	/// 要素情報を取得します。
	/// </summary>
	/// <param name="index">要素番号</param>
	/// <returns>要素情報</returns>
	public TRecord this[int index] => this.sourceList[index];
	#endregion プロパティー定義

	#region 実装イベント定義(INotifyCollectionChanged)
	/// <summary>
	/// 監視処理を追加または削除します。
	/// </summary>
	/// <value>監視処理</value>
	event NotifyCollectionChangedEventHandler? INotifyCollectionChanged.CollectionChanged {
		add    => this.listenList += value;
		remove => this.listenList -= value;
	}
	/// <summary>
	/// 変更情報を通知します。
	/// </summary>
	/// <param name="option">変更情報</param>
	protected virtual void Notify(NotifyCollectionChangedEventArgs option) =>
		this.listenList?.Invoke(this, option);
	#endregion 実装イベント定義(INotifyCollectionChanged)

	#region 生成メソッド定義
	/// <summary>
	/// 基底画面一覧を生成します。
	/// </summary>
	public AbstractScreenList() {
		this.sourceList = [];
		this.listenList = null;
	}
	#endregion 生成メソッド定義

	#region 公開メソッド定義
	/// <summary>
	/// 要素情報を登録します。
	/// </summary>
	/// <param name="source">要素情報</param>
	/// <param name="offset">登録番号</param>
	protected virtual void RegistData(TRecord source, int offset) {
		this.sourceList.Insert(offset, source);
		Notify(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, source, offset));
	}
	/// <summary>
	/// 要素情報を登録します。
	/// </summary>
	/// <param name="source">要素情報</param>
	protected virtual void RegistData(TRecord source) =>
		RegistData(source, this.sourceList.Count);
	/// <summary>
	/// 保持要素を削除します。
	/// </summary>
	/// <param name="removeCode">削除判定</param>
	protected virtual void RemoveList(Predicate<TRecord> removeCode) {
		for (var index = this.sourceList.Count - 1; 0 <= index; index --) {
			var choose = this.sourceList[index];
			if (removeCode(choose)) {
				// TODO 将来ではIListを利用
				var removeData = this.sourceList[index];
				this.sourceList.RemoveAt(index);
				Notify(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,removeData, index));
			}
		}
	}
	#endregion 公開メソッド定義

	#region 実装メソッド定義
	/// <summary>
	/// 反復処理を取得します。
	/// </summary>
	/// <returns>反復処理</returns>
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		foreach (var choose in this.sourceList) {
			yield return choose;
		}
	}
	/// <summary>
	/// 反復処理を取得します。
	/// </summary>
	/// <returns>反復処理</returns>
	IEnumerator<TRecord> IEnumerable<TRecord>.GetEnumerator() {
		foreach (var choose in this.sourceList) {
			yield return choose;
		}
	}
	#endregion 実装メソッド定義
}
