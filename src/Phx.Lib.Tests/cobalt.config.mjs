export default {
    base: 'dotnet',
    configureCobalt: (cobalt) => {
        const { libs, steps } = cobalt;

        cobalt.dependency(
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