using System.Diagnostics;
using Kantan.Text;
using Novus.OS;
using Novus.OS.Win32.Wrappers;

namespace Ion;

public static class Program
{
	public static void Main(string[] args)
	{

#if DEBUG
		args = new[]
		{
			"symbols",
			@"C:\Symbols\charmap.pdb",

			"GetUName"
			// "*"
		};

#endif

		Debug.WriteLine($"{args.QuickJoin()}");

		if (!args.Any()) {
			Help();
			return;
		}


		var op = args.First();
		var sp = new Span<string>(args);

		switch (op) {
			case "symbols":
				if (args.Length < 2) {
					break;
				}

				Run_Symbols(sp[1..]);
				break;
			default:
				break;
		}

	


	private static void Run_Symbols(Span<string> args)
	{
		const string ALL = "*";

		var img = args[0];
		var id  = args.Length < 2 ? ALL : args[1];

		Debug.WriteLine($"{nameof(Run_Symbols)}: {img} {id}");

		using var r = new SymbolReader(img);

		if (id == ALL) {
			r.LoadAll();

			foreach (Symbol symbol in r.SymbolsCache) {
				Console.WriteLine(symbol);
			}

		}
		else {
			var s = r.GetSymbol(id);
			Console.WriteLine(s);
		}
	}

	private static void Help() { }
}