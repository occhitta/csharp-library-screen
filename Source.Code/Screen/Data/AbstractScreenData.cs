using System.ComponentModel;

namespace Occhitta.Libraries.Screen.Data;

/// <summary>
/// 基底画面情報クラスです。
/// </summary>
public abstract class AbstractScreenData : INotifyPropertyChanged {
	/// <summary>
	/// 変更管理
	/// </summary>
	private PropertyChangedEventHandler? source;

	/// <summary>
	/// 変更処理を追加または削除します。
	/// </summary>
	event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged {
		add    => this.source += value;
		remove => this.source -= value;
	}

	/// <summary>
	/// 要素変更を通知します。
	/// </summary>
	/// <param name="memberName">要素名称</param>
	protected virtual void Notify(string memberName) =>
		this.source?.Invoke(this, new PropertyChangedEventArgs(memberName));

	/// <summary>
	/// 要素情報を更新します。
	/// </summary>
	/// <param name="sourceData">要素情報</param>
	/// <param name="updateData">更新情報</param>
	/// <param name="memberName">要素名称</param>
	/// <typeparam name="TValue">要素種別</typeparam>
	/// <returns><paramref name="sourceData" />を更新した場合、<c>True</c>を返却</returns>
	protected virtual bool Update<TValue>(ref TValue sourceData, TValue updateData, string memberName) {
		if (Equals(sourceData, updateData)) {
			return false;
		} else {
			sourceData = updateData;
			Notify(memberName);
			return true;
		}
	}
	/// <summary>
	/// 要素情報を更新します。
	/// </summary>
	/// <param name="sourceData">要素情報</param>
	/// <param name="updateData">更新情報</param>
	/// <param name="memberName">要素名称</param>
	/// <param name="updateHook">更新処理</param>
	/// <typeparam name="TValue">要素種別</typeparam>
	protected virtual void Update<TValue>(ref TValue sourceData, TValue updateData, string memberName, System.Action? updateHook) {
		if (Update(ref sourceData, updateData, memberName)) updateHook?.Invoke();
	}
	/// <summary>
	/// 要素情報を更新します。
	/// </summary>
	/// <param name="sourceData">要素情報</param>
	/// <param name="updateData">更新情報</param>
	/// <param name="memberName">要素名称</param>
	/// <param name="updateHook">更新処理</param>
	/// <typeparam name="TValue">要素種別</typeparam>
	protected virtual void Update<TValue>(ref TValue sourceData, TValue updateData, string memberName, System.Action<TValue, TValue>? updateHook) {
		if (Update(ref sourceData, updateData, memberName)) updateHook?.Invoke(sourceData, updateData);
	}
}
