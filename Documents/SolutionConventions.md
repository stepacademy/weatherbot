### Solution conventions
Each individual module must be included in the solution WeatherBot.sln

The settings of each individual project:

Application > Assembly name     = ProjectName<br>
Application > Default namespace = WeatherBot.ProjectName<br>
Application > Target Framework  = .NET Framework 4.5.2

Build > Output path = ..\ ..\Build\Debug\   - for debug<br>
Build > Output path = ..\ ..\Build\Release\ - for release<br>

The settings of each individual TEST project:

Application > Assembly name     = ProjectName<br>
Application > Default namespace = Test.ProjectName<br>
Application > Target Framework  = .NET Framework 4.5.2

Build > Output path = ..\ ..\ ..\ Build\Debug\   - for debug<br>
Build > Output path = ..\ ..\ ..\ Build\Release\ - for release<br>

Project structure example:
```
[weatherbot]                                <-- master
    [Build]
        [Debug]                             <-- NO commit
            ...dll
            ...exe
            ...xml
            ...json
            ...
            *ttoken.*
        [Release]
            ...dll
            ...exe
            ...xml
            ...json
            ...
            *ttoken.*
    [WeatherBot]                            <-- commit (solution)
        [TeleInteraction]
            TeleInteraction.csproj
        [ModuleA]
            ModuleA.csproj
        [ModuleN..]
            ModuleN...**proj
    WeatherBot.sln
```

 ~ must be supplemented...
