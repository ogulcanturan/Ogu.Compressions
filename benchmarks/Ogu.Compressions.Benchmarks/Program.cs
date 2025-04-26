using BenchmarkDotNet.Running;

// dotnet run -c Release

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);