using System;
using System.Threading.Tasks;

namespace Occhitta.Libraries.Screen.Hook;

/// <summary>
/// <see cref="Delegate" />利用画面操作クラスです。
/// </summary>
public sealed class DelegateScreenHeavyHook : AbstractScreenHeavyHook {
	/// <summary>
	/// 可否判定
	/// </summary>
	private readonly Predicate<object?> accept;
	/// <summary>
	/// 実行処理
	/// </summary>
	private readonly Func<object?, Task> action;

	/// <summary>
	/// <see cref="Delegate" />利用画面操作を生成します。
	/// </summary>
	/// <param name="action">実行処理</param>
	public DelegateScreenHeavyHook(Func<Task> action) {
		if (action == null) {
			throw new ArgumentNullException(nameof(action));
		} else {
			this.accept = parameter => true;
			this.action = parameter => action();
		}
	}
	/// <summary>
	/// <see cref="Delegate" />利用画面操作を生成します。
	/// </summary>
	/// <param name="action">実行処理</param>
	/// <param name="accept">可否判定</param>
	public DelegateScreenHeavyHook(Func<Task> action, Func<bool> accept) {
		if (action == null) {
			throw new ArgumentNullException(nameof(action));
		} else if (accept == null) {
			throw new ArgumentNullException(nameof(accept));
		} else {
			this.accept = parameter => accept();
			this.action = parameter => action();
		}
	}
	/// <summary>
	/// <see cref="Delegate" />利用画面操作を生成します。
	/// </summary>
	/// <param name="action">実行処理</param>
	public DelegateScreenHeavyHook(Func<object?, Task> action) {
		if (action == null) {
			throw new ArgumentNullException(nameof(action));
		} else {
			this.accept = parameter => true;
			this.action = action;
		}
	}
	/// <summary>
	/// <see cref="Delegate" />利用画面操作を生成します。
	/// </summary>
	/// <param name="action">実行処理</param>
	/// <param name="accept">可否判定</param>
	public DelegateScreenHeavyHook(Func<object?, Task> action, Predicate<object?> accept) {
		if (action == null) {
			throw new ArgumentNullException(nameof(action));
		} else if (accept == null) {
			throw new ArgumentNullException(nameof(accept));
		} else {
			this.accept = accept;
			this.action = action;
		}
	}

	/// <summary>
	/// 可否変更を通知します。
	/// </summary>
	public new void Notify() => base.Notify();

	/// <summary>
	/// 操作可否を判定します。
	/// </summary>
	/// <param name="parameter">実行引数</param>
	/// <returns>操作可能である場合、<c>True</c>を返却</returns>
	protected override bool Accept(object? parameter) =>
		this.accept(parameter);
	/// <summary>
	/// 操作処理を実行します。
	/// </summary>
	/// <param name="parameter">実行引数</param>
	protected override async Task Invoke(object? parameter) =>
		await this.action(parameter);
}
