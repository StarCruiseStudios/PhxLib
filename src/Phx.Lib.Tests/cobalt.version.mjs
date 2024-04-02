export default {
  versionSetName: 'starcruisestudios.phx',
  versionSetVersion: '0.0.1',
  libs: {
    coverlet: {
      collector: { artifact: 'coverlet.collector', version: '6.0.0' }
    },

    microsoft: {
      codeAnalysis: {
        analyzers: { artifact: 'Microsoft.CodeAnalysis.Analyzers', version: '3.3.3' },
        csharp: {
          csharp: { artifact: 'Microsoft.CodeAnalysis.CSharp', version: '4.3.0' },
          sourceGenerators: {
            testing: {
              nUnit: { artifact: 'Microsoft.CodeAnalysis.SourceGenerators.Testing.NUnit', version: '1.1.1' }
            }
          },
          workspaces: { artifact: 'Microsoft.CodeAnalysis.Workspaces', version: '4.3.0' }
        }
      },
      net: {
        test: {
          sdk: { artifact: 'Microsoft.NET.Test.Sdk', version: '17.7.2' }
        }
      }
    },

    nlog: {
      nlog: {artifact: 'NLog', version: '5.2.4'},
      extensions: {
        logging: { artifact: 'NLog.Extensions.Logging', version: '5.3.4' },
      }
    },

    nSubstitute: {
      nSubstitute: { artifact: 'NSubstitute', version: '5.1.0' },
      analyzers: {
        csharp: { artifact: 'NSubstitute.Analyzers.CSharp', version: '1.0.16' },
      }
    },

    nUnit: {
      nUnit: { artifact: 'NUnit', version: '3.13.3' },
      testAdapter: { artifact: 'NUnit3TestAdapter', version: '4.5.0' },
      analyzers: { artifact: 'NUnit.Analyzers', version: '3.8.0' },
    },

    phx: {
      inject: { artifact: 'Phx.Inject', version: '0.4.4' },
      lib: { artifact: 'Phx.Lib', version: '0.3.1' },
      test: { artifact: 'Phx.Test', version: '0.3.0' },
      validation: { artifact: 'Phx.Validation', version: '0.1.1' },
    },
  }
}