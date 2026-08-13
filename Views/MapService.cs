using Microsoft.JSInterop;

public class MapService : IAsyncDisposable
{
    private readonly IJSRuntime _js;
    private DotNetObjectReference<object>? _dotNetRef;

    public MapService(IJSRuntime js) => _js = js;

    public async Task InitAsync(object callbackTarget)
    {
        _dotNetRef = DotNetObjectReference.Create(callbackTarget);
        await _js.InvokeVoidAsync("mapInterop.initMap", _dotNetRef);
    }

    public Task AddMarkerAsync(int id, double lat, double lng, string? emoji)
        => _js.InvokeVoidAsync("mapInterop.addMarker", id, lat, lng, emoji).AsTask();

    public Task FocusMarkerAsync(int id)
        => _js.InvokeVoidAsync("mapInterop.focusMarker", id).AsTask();

    public async ValueTask DisposeAsync()
    {
        if (_dotNetRef is not null)
        {
            await _js.InvokeVoidAsync("mapInterop.destroyMap");
            _dotNetRef.Dispose();
        }
    }
}