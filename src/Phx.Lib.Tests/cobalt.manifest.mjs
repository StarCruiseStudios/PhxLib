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
            context.versions.plugins.phx.csprojcompany,
            context.versions.plugins.phx.cstest,
        )
        cobalt.stash.add(
            { artifact: context.versions.stash.phx.nlog.test, path: '.' }
        )
    },
    configure: async (cobalt, context) => {
        cobalt.config.set('project', {
            artifact: { artifact: 'Phx.Lib.Tests', version: '1.0.0' },
            props: {
                ImplicitUsings: 'enable',
                RootNamespace: '',
            }
        });
    }
}


// import slnConfig from '../../cobalt.manifest.mjs';

// export default {
//     ...slnConfig,
//     cobaltRoot: '../..',
//     configureCobalt: (cobalt) => {
//         const { config, libs, steps } = cobalt;
//         cobalt.project = {
//             version: { artifact: 'Phx.Lib.Tests', version: '1.0.0' },
//             props: {
//                 OutputType: 'Exe',
//                 TargetFramework: 'net7.0',
//                 LangVersion: '11.0',
//                 ImplicitUsings: 'enable',
//                 Nullable: 'enable',
//                 RootNamespace: '',
//                 TreatWarningsAsErrors: 'true',
//             }
//         };

//         cobalt.dependencies(
//             libs.coverlet.collector,
//             libs.microsoft.net.test.sdk,
//             libs.nlog.nlog,
//             libs.nlog.extensions.logging,
//             libs.nSubstitute.nSubstitute,
//             libs.nSubstitute.analyzers.csharp,
//             libs.nUnit.nUnit,
//             libs.nUnit.testAdapter,
//             libs.nUnit.analyzers,
//             libs.phx.test,
//             libs.phx.validation,
//         );
//     }
// }
