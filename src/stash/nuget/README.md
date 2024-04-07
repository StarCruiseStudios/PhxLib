* **version-set.mjs**
```typescript
libs: { 
    stash: {
        phx: {
            resources: {
                nuget: { artifact: 'starcruisestudios.phx.resources.nuget', version: '0.0.2' },
            }
        }
    }
}
```

* **cobalt.manifest.mjs**
```typescript
import configureNuget from './stash/nuget/nuget.mjs';

configureCobalt: (cobalt) => {
        cobalt.stash(
            { ...libs.stash.phx.resources.nuget, path: './stash/nuget' },
        )
        configureNuget(cobalt);
}
```

* **project.csproj**
```xml
<Project Sdk="Microsoft.NET.Sdk">    
    <Import Project="./stash/nuget/NugetProperties.props" Condition="Exists('./stash/nuget/NugetProperties.props')"/>
    
    <Target Name="EnsureBuildFileImports" BeforeTargets="PrepareForBuild">
        <PropertyGroup>
            <ErrorText>This package relies on imported build files that are not found. Missing: {0}</ErrorText>
        </PropertyGroup>
        <Error Condition="!Exists('./stash/nuget/NugetProperties.props')" Text="$([System.String]::Format('$(ErrorText)', './stash/nuget/NugetProperties.props'))"/>
    </Target>
</Project>
```
