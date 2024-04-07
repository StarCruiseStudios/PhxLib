/* 
 * nuget.mjs
 * 
 * Copyright (c) 2024 Star Cruise Studios LLC. All rights reserved.
 * Licensed under the Apache License, Version 2.0.
 * See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
 */
import path from 'path';

export default function configureNuget(cobalt) {
    const { steps } = cobalt;
    const NUGET_OUTPUT_DIR = path.join(cobalt.projectDir, 'bin', 'nuget');
    cobalt.step('pack', (step) => {
        step.exec('dotnet', ['pack', '--output', NUGET_OUTPUT_DIR]);
    })

    cobalt.step('publish_local', (step) => {
        const NUGET_LOCAL_REPO = process.env.NUGET_LOCAL_REPO;
        const artifact = cobalt.project.version;
        const nupkgName = `${artifact.artifact}.${artifact.version}.nupkg`;
        const nupkgPath = path.join(NUGET_OUTPUT_DIR, nupkgName);
        step.exec('dotnet', ['nuget', 'push', nupkgPath, '--source', NUGET_LOCAL_REPO]);
    })
    steps.publish_local.preStep.dependsOn(steps.pack);

    steps.publish.override((step) => {
        const artifact = cobalt.project.version;
        const nupkgName = `${artifact.artifact}.${artifact.version}.nupkg`;
        const nupkgPath = path.join(NUGET_OUTPUT_DIR, nupkgName);
        step.exec('dotnet', ['nuget', 'push', nupkgPath, '--source', 'https://api.nuget.org/v3/index.json']);
    })
    steps.publish.preStep.dependsOn(steps.pack);
}
