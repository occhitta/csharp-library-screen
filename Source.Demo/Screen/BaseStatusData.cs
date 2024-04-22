using System;

namespace Occhitta.Examples.Screen;

/// <summary>
/// 状態変更インターフェースです。
/// </summary>
#pragma warning disable IDE1006
internal interface BaseStatusData {
#pragma warning restore IDE1006
	/// <summary>
	/// 状態処理を追加または削除します。
	/// </summary>
	event EventHandler<string?> StatusHook;
}
