# PhxLib Resources
This submodule contains the common build and development resources needed by the PhxLib packages.

## Set Up
After creating a git repository where you want to use the PhxLib Resources submodule, from the root directory:
```shell
git submodule add https://github.com/StarCruiseStudios/PhxLibResources.git ./resources
```

Then import the relevant MSBuild files in the .csproj files.
* PhxLib.common.props for class libraries.
    ```msbuild
    <Project Sdk="Microsoft.NET.Sdk">
        <Import Project="../../resources/build/PhxLib.common.props" Condition="Exists('../../resources/build/PhxLib.common.props')" />
        
        <!-- ... -->
        
        <!-- If EnsureBuildFileImports target already exists, add just the error condition to the existing target. -->
        <Target Name="EnsureBuildFileImports" BeforeTargets="PrepareForBuild">
            <PropertyGroup>
                <ErrorText>This package relies on imported build files that are not found. Missing: {0}</ErrorText>
            </PropertyGroup>
            <Error Condition="!Exists('../../resources/build/PhxLib.common.props')" Text="$([System.String]::Format('$(ErrorText)', '../../resources/build/PhxLib.common.props'))" />
        </Target>
    </Project>
    ```
* NugetProperties.props for Nuget published packages.
    ```msbuild
    <Project Sdk="Microsoft.NET.Sdk">
        <Import Project="../../resources/build/NugetProperties.props" Condition="Exists('../../resources/build/NugetProperties.props')"/>
        
        <!-- ... -->
        
        <!-- If EnsureBuildFileImports target already exists, add just the error condition to the existing target. -->
        <Target Name="EnsureBuildFileImports" BeforeTargets="PrepareForBuild">
            <PropertyGroup>
                <ErrorText>This package relies on imported build files that are not found. Missing: {0}</ErrorText>
            </PropertyGroup>
            <Error Condition="!Exists('../../resources/build/NugetProperties.props')" Text="$([System.String]::Format('$(ErrorText)', '../../resources/build/NugetProperties.props'))" />
        </Target>
    </Project>
    ```
    Also add the necessary Nuget properties to the .csproj file.
    ```msbuild
    <Project Sdk="Microsoft.NET.Sdk">
        <PropertyGroup>
            <Version> </Version>
            <Description> </Description>
            <PackageProjectUrl> </PackageProjectUrl>
            <RepositoryUrl> </RepositoryUrl>
        </PropertyGroup>
    </Project>
    ```
* PhxLib.test.common.props for NUnit test packages.
    ```msbuild
    <Project Sdk="Microsoft.NET.Sdk">
        <Import Project="../../resources/build/PhxLib.test.common.props" Condition="Exists('../../resources/build/PhxLib.test.common.props')"/>
        
        <!-- ... -->
        
        <!-- If EnsureBuildFileImports target already exists, add just the error condition to the existing target. -->
        <Target Name="EnsureBuildFileImports" BeforeTargets="PrepareForBuild">
            <PropertyGroup>
                <ErrorText>This package relies on imported build files that are not found. Missing: {0}</ErrorText>
            </PropertyGroup>
            <Error Condition="!Exists('../../resources/build/PhxLib.test.common.props')" Text="$([System.String]::Format('$(ErrorText)', '../../resources/build/PhxLib.test.common.props'))" />
        </Target>
    </Project>
    ```
    Test Packages should also include `PhxLib.common.props`.

## Making Changes
If changes are made to the submodule, they must be commited and pushed to the submodule repository using `git commit -a`/`git push` inside the submodule directory.
After that is complete, the changes to the submodule metadata will need to be commited to the parent repository as well.

## Checking for Updated Versions
To check for outdated packages in `Versions.props`, use:
```shell
dotnet list package --outdated
```

<div align="center">
Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.  
Licensed under the Apache License, Version 2.0.  
See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
</div>