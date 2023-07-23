The telemetry data points do not collect personal data, such as usernames or email addresses. The data is sent securely to Microsoft servers using Azure Monitor technology, held under restricted access, and published under strict security controls from secure Azure Storage systems.

Protecting your privacy is important to us. If you suspect the telemetry is collecting sensitive data or the data is being insecurely or inappropriately handled, file an issue in the [nuke-build/nuke](https://github.com/nuke-build/nuke/issues) repository or email us for investigation.

The telemetry feature collects the following data:

| Version | Data |
| --- | --- |
| All | Timestamp of invocation |
| All | Operating system |
| All | Version of .NET SDK |
| All | Repository provider (GitHub, GitLab, Bitbucket, etc.) |
| All | Repository Branch (main, develop, feature, hotfix, custom) |
| All | Hashed Repository URL (SHA256; first 6 characters) |
| All | Hashed Commit Sha (SHA256; first 6 characters) |
| All | Compile time of build project in seconds |
| All | Target framework |
| All | Version of Nuke.Common and Nuke.GlobalTool |
| All | Host implementation (only non-custom) |
| All | Build type (project/global tool) |
| All | Number of executable targets |
| All | Number of custom extensions |
| All | Number of custom components |
| All | Used configuration generators and build components (only non-custom) |
| All | Target execution time in seconds (only for targets named Restore, Compile, or Test) |

**NOTE**
Whenever a type does not originate from the Nuke namespace, it is replaced with `<External>`.

## How to opt out

The telemetry feature is enabled by default. To opt out, set the `NUKE_TELEMETRY_OPTOUT` environment variable to `1` or `true`.
