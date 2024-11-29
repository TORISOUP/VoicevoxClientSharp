using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VoicevoxClientSharp.ApiClient.Models;

namespace VoicevoxClientSharp.ApiClient
{
    public interface ILibraryClient : IDisposable
    {
        /// <summary>
        /// GET //downloadable_libraries
        /// ライブラリの情報の一覧を返します。
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<DownloadableLibraryInfo[]> GetDownloadableLibrariesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// GET //installed_libraries
        /// インストールした音声ライブラリの情報を返します。
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask<InstalledLibraryInfo[]> GetInstalledLibrariesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// POST /install_library/{library_uuid}
        /// 音声ライブラリをインストールします。
        /// 音声ライブラリのZIPファイルを送信してください。
        /// </summary>
        /// <param name="libraryId"></param>
        /// <param name="libraryZip"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask InstallLibraryAsync(string libraryId, byte[] libraryZip, CancellationToken cancellationToken = default);

        /// <summary>
        /// POST /uninstall_library/{library_uuid}
        /// 音声ライブラリをアンインストールします。
        /// </summary>
        /// <param name="libraryId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask UninstallLibraryAsync(string libraryId, CancellationToken cancellationToken = default);
    }

    public partial class VoicevoxApiClient
    {
        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<DownloadableLibraryInfo[]> GetDownloadableLibrariesAsync(CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/downloadable_libraries";
            return GetAsync<DownloadableLibraryInfo[]>(url, cancellationToken);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public ValueTask<InstalledLibraryInfo[]> GetInstalledLibrariesAsync(CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/installed_libraries";
            return GetAsync<InstalledLibraryInfo[]>(url, cancellationToken);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public async ValueTask InstallLibraryAsync(string libraryId, byte[] libraryZip, CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/install_library/{libraryId}";
            var contents = new ByteArrayContent(libraryZip);
            await _httpClient.PostAsync(url, contents, cancellationToken);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public async ValueTask UninstallLibraryAsync(string libraryId, CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/uninstall_library/{libraryId}";
            await _httpClient.PostAsync(url, null, cancellationToken);
        }
    }
}