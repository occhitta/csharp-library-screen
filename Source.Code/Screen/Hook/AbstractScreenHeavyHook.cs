using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Occhitta.Libraries.Screen.Hook;

/// <summary>
/// 基底画面遅延操作クラスです。
/// </summary>
public abstract class AbstractScreenHeavyHook : ICommand {
	/// <summary>
	/// 変更管理
	/// </summary>
	private EventHandler? source;

	/// <summary>
	/// 変更処理を追加または削除します。
	/// </summary>
	event EventHandler? ICommand.CanExecuteChanged {
		add    => this.source += value;
		remove => this.source -= value;
	}

	/// <summary>
	/// 可否変更を通知します。
	/// </summary>
	protected virtual void Notify() =>
		this.source?.Invoke(this, EventArgs.Empty);

	/// <summary>
	/// 操作可否を判定します。
	/// </summary>
	/// <param name="parameter">実行引数</param>
	/// <returns>操作可能である場合、<c>True</c>を返却</returns>
	protected abstract bool Accept(object? parameter);
	/// <summary>
	/// 操作処理を実行します。
	/// </summary>
	/// <param name="parameter">実行引数</param>
	protected abstract Task Invoke(object? parameter);

	/// <summary>
	/// 操作可否を判定します。
	/// </summary>
	/// <param name="parameter">実行引数</param>
	/// <returns>操作可能である場合、<c>True</c>を返却</returns>
	bool ICommand.CanExecute(object? parameter) =>
		Accept(parameter);
	/// <summary>
	/// 操作処理を実行します。
	/// </summary>
	/// <param name="parameter">実行引数</param>
	async void ICommand.Execute(object? parameter) =>
		await Invoke(parameter);
}
