using System.Windows;

//[assembly:ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

namespace Occhitta.Examples;

/// <summary>
/// 実行処理クラスです。
/// </summary>
internal static class MainModule {
	/// <summary>
	/// 参照情報へ変換します。
	/// </summary>
	/// <param name="source">参照経路</param>
	/// <returns>参照情報</returns>
	private static System.Uri ToPath(string source) =>
		new(source, System.UriKind.Relative);
	/// <summary>
	/// 参照情報を登録します。
	/// </summary>
	/// <param name="source">管理一覧</param>
	/// <param name="values">参照配列</param>
	private static void Regist(System.Collections.ObjectModel.Collection<ResourceDictionary> source, string[] values) {
		foreach (var choose in values) {
			source.Add(new ResourceDictionary() { Source = ToPath(choose) });
		}
	}
	/// <summary>
	/// 参照情報を登録します。
	/// </summary>
	/// <param name="source">管理一覧</param>
	/// <param name="values">参照配列</param>
	private static void Regist(Application source, params string[] values) =>
		Regist(source.Resources.MergedDictionaries, values);

	/// <summary>
	/// サンプルプログラムを実行します。
	/// </summary>
	[System.STAThread]
	public static void Main() {
		// 初期処理

		// 生成処理
		var source = new Application() {
			StartupUri = ToPath("/Screen/MainWindowView.xaml")
		};

		// 設定処理
		Regist(source
			, "/Common/FailureDialogData.xaml"
			, "/Common/WarningDialogData.xaml"
			, "/Common/MessageDialogData.xaml"
			, "/Common/ConfirmDialogData.xaml"
			, "/Screen/Dialog/FailureScreenData.xaml"
			, "/Screen/Dialog/WarningScreenData.xaml"
			, "/Screen/Dialog/MessageScreenData.xaml"
			, "/Screen/Dialog/ConfirmScreenData.xaml"
		);

		// 実行処理
		source.Run();
	}
}
