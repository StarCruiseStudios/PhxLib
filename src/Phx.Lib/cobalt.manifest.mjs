/* 
 * cobalt.manifest.mjs
 * 
 * Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
 * Licensed under the Apache License 2.0.
 * See https://www.apache.org/licenses/LICENSE-2.0 for full license information.
 */
import { versionSet } from '../../cobalt.manifest.mjs';

export default {
    properties: {
        cobaltVersion: '1.0.x',
        root: '../..',
        versionSet: versionSet,
    },
    project: async (cobalt, context) => {
        cobalt.plugins.add(
            context.versions.cobalt.plugin.dotnet,
            context.versions.cobalt.plugin.nuget,
            context.versions.plugins.phx.csprojcompany,
            context.versions.plugins.phx.cslib,
        )
        cobalt.stash.add(
            { artifact: context.versions.stash.phx.nuget, path: '../stash/nuget' }
        )
    },
    configure: async (cobalt, context) => {
        cobalt.config.set('project', {
            artifact: { artifact: 'Phx.Lib', version: '0.3.7' },
            description: 'PHX Lib core utilities and extensions.',
            packageProjectUrl: 'https://github.com/StarCruiseStudios/PhxLib',
            tags: 'phxlib,starcruisestudios',
            repositoryUrl: 'https://github.com/StarCruiseStudios/PhxLib',
            repositoryType: 'Github',
            licenseUrl: 'https://www.apache.org/licenses/LICENSE-2.0',
            props: {
                ImplicitUsings: 'enable',
                RootNamespace: '',
            }
        });
    }
}
