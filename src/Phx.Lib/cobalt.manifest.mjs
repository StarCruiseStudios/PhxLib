/* 
 * cobalt.manifest.mjs
 * 
 * Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
 * Licensed under the Apache License 2.0.
 * See https://www.apache.org/licenses/LICENSE-2.0 for full license information.
 */

import configureNuget from '../stash/nuget/nuget.mjs';
import slnConfig from '../../cobalt.manifest.mjs';

export default {
    ...slnConfig,
    cobaltRoot: '../..',
    configureCobalt: (cobalt) => {
        const { libs, steps } = cobalt;
        cobalt.project = {
            version: libs.phx.lib,
            description: 'PHX Lib core utilities and extensions.',
            authors: 'Star Cruise Studios',
            company: 'Star Cruise Studios LLC',
            copyright: 'Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.',
            packageProjectUrl: 'https://github.com/StarCruiseStudios/PhxLib',
            tags: 'phxlib,starcruisestudios',
            repositoryUrl: 'https://github.com/StarCruiseStudios/PhxLib',
            repositoryType: 'Github',
            licenseUrl: 'https://www.apache.org/licenses/LICENSE-2.0',
            props: {
                TargetFramework: 'netstandard2.0',
                LangVersion: '11.0',
                ImplicitUsings: 'enable',
                Nullable: 'enable',
                RootNamespace: '',
                TreatWarningsAsErrors: 'true',
            }
        };

        cobalt.stash(
            { ...libs.stash.phx.resources.nuget, path: '../stash/nuget' },
        );
        cobalt.dependencies(
        );

        configureNuget(cobalt);
    }
}
