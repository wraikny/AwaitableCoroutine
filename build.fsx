#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"

#load ".fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

[<AutoOpen>]
module Utils =
  let dotnet cmd arg =
    let res = DotNet.exec id cmd arg

    let msg = $"dotnet %s{cmd} %s{arg}"

    if res.OK then
      Trace.tracefn "Success '%s'" msg
    else
      failwithf "Failed '%s'" msg

  let (|LowerCase|_|) (x: string) (s: string) =
    if x.ToLower() = s.ToLower() then Some LowerCase else None

  let getConfiguration = function
    | Some (LowerCase("debug")) -> DotNet.BuildConfiguration.Debug
    | Some (LowerCase("release")) -> DotNet.BuildConfiguration.Release
    | Some (c) -> failwithf "Invalid configuration '%s'" c
    | _ -> DotNet.BuildConfiguration.Debug

Target.initEnvironment ()

let args = Target.getArguments()

Target.create "Format" (fun _ ->
  
  !! "src/**/*.csproj"
  ++ "tests/**/*.csproj"
  ++ "example/**/*.csproj"
  |> Seq.iter (fun proj ->
    dotnet "format" $"{proj} -v diag"
  )
)

Target.create "Format.Check" (fun _ ->
  !! "src/**/*.csproj"
  ++ "tests/**/*.csproj"
  ++ "example/**/*.csproj"
  |> Seq.iter (fun proj ->
    dotnet "format" $"{proj} --verify-no-changes -v diag"
  )
)

Target.create "Clean" (fun _ ->
  !! "src/**/bin"
  ++ "src/**/obj"
  ++ "tests/**/bin"
  ++ "tests/**/obj"
  ++ "example/**/bin"
  ++ "example/**/obj"
  |> Shell.cleanDirs
)

Target.create "Build" (fun _ ->
  let configuration =
    args
    |> Option.bind Array.tryHead
    |> getConfiguration

  !! "src/**/*.*proj"
  ++ "tests/**/*.*proj"
  ++ "example/**/*.*proj"
  |> Seq.iter (DotNet.build (fun p ->
    { p with
        Configuration = configuration
    }))
)

Target.create "Test" (fun _ ->
  !! "tests/**/*.*proj"
  |> Seq.iter(fun proj ->
    DotNet.test (fun p ->
      { p with
          Logger = Some "console;verbosity=normal"
      }
    ) proj
  )
)

Target.create "CopyDlls" (fun _ ->
  let shell cmd args =
    let res = Shell.Exec(cmd, args)
    if res <> 0 then failwithf "failed '%s %s'" cmd args

  Directory.ensure "output"

  [ "netstandard2.1"
    "net5.0"
    "net6.0"
  ] |> Seq.iter (fun target ->
    shell "mkdir" $"output/%s{target}"
    [ ""
      ".Altseed2"
      ".FSharp"
    ] |> Seq.iter (fun sufix ->
      shell "cp" $"src/AwaitableCoroutine%s{sufix}/bin/Release/%s{target}/AwaitableCoroutine%s{sufix}.dll output/%s{target}/."
    )
  )
)

Target.create "None" ignore
Target.create "Default" ignore

"Build" ==> "Default"

Target.runOrDefaultWithArguments "Default"