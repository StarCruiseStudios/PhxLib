/* 
 * cobalt.manifest.mjs
 * 
 * Copyright (c) 2024 Your Company. All rights reserved.
 * Licensed under the <<licensename>>.
 * See <<licenseurl>> for full license information.
 */

import slnConfig from '../../cobalt.manifest.mjs';

export default {
    ...slnConfig,
    cobaltRoot: '../..',
    configureCobalt: (cobalt) => {
        const { libs, steps } = cobalt;
        cobalt.project = {
            version: { artifact: 'Phx.Lib.Tests', version: '1.0.0' },
            props: {
                OutputType: 'Exe',
                TargetFramework: 'net7.0',
                LangVersion: '11.0',
                ImplicitUsings: 'enable',
                Nullable: 'enable',
                RootNamespace: '',
                TreatWarningsAsErrors: 'true',
            }
        };

        cobalt.dependencies(
            libs.coverlet.collector,
            libs.microsoft.net.test.sdk,
            libs.nlog.nlog,
            libs.nlog.extensions.logging,
            libs.nSubstitute.nSubstitute,
            libs.nSubstitute.analyzers.csharp,
            libs.nUnit.nUnit,
            libs.nUnit.testAdapter,
            libs.nUnit.analyzers,
            libs.phx.test,
            libs.phx.validation,
        );
    }
}
