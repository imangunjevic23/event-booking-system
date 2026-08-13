window.mapInterop = (function () {
    let map;
    let markers = {};
    let dotNetHelper;
    let selectedId = null;
    let createMapInstance = null;

    const southWest = L.latLng(43.70, 18.05);
    const northEast = L.latLng(43.98, 18.64);
    const bounds = L.latLngBounds(southWest, northEast);
    const center = [43.8447821, 18.3398074];

    function setMarkerState(id, state) {
        const marker = markers[id];
        if (!marker) return;
        const el = marker.getElement();
        if (!el) return;

        el.classList.remove('map-marker-active');
        if (state === 'hover' || state === 'selected') {
            el.classList.add('map-marker-active');
        }
    }

    return {
        initMap: function (helper) {
            dotNetHelper = helper;
            map = L.map('map', {
                center: center,
                zoom: 13,
                minZoom: 12,
                maxZoom: 18,
                maxBounds: bounds,
                maxBoundsViscosity: 1.0
            });
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; OpenStreetMap contributors'
            }).addTo(map);

            map.on('click', function () {
                dotNetHelper.invokeMethodAsync('OnMapClicked');
            });
        },

        initCreateMap: function (helper) {
            createMapInstance = L.map('create-map', {
                center: center,
                zoom: 13,
                minZoom: 12,
                maxZoom: 18,
                maxBounds: bounds,
                maxBoundsViscosity: 1.0
            });

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; OpenStreetMap contributors'
            }).addTo(createMapInstance);

            createMapInstance.on('click', function (e) {
                helper.invokeMethodAsync('OnMapClick', e.latlng.lat, e.latlng.lng);
            });
        },

        destroyCreateMap: function () {
            if (createMapInstance) {
                createMapInstance.remove();
                createMapInstance = null;
            }
        },

        setMarkers: function (markersData) {
            for (var id in markers) {
                if (markers.hasOwnProperty(id)) {
                    map.removeLayer(markers[id]);
                }
            }
            markers = {};
            selectedId = null;

            markersData.forEach(function (data) {
                const icon = L.divIcon({
                    html: `<span class="marker-emoji">${data.emoji || '📍'}</span>`,
                    className: 'map-marker',
                    iconSize: [48, 48],
                    iconAnchor: [24, 24]
                });
                const marker = L.marker([data.latitude, data.longitude], { icon }).addTo(map);
                marker.on('click', function () {
                    dotNetHelper.invokeMethodAsync('OnMarkerClicked', data.id);
                });
                marker.on('mouseover', function () {
                    if (selectedId !== data.id) {
                        setMarkerState(data.id, 'hover');
                    }
                });
                marker.on('mouseout', function () {
                    if (selectedId !== data.id) {
                        setMarkerState(data.id, 'normal');
                    }
                });
                markers[data.id] = marker;
            });
        },

        setMarkerHover: function (id) {
            if (selectedId === id || selectedId !== null) return;
            setMarkerState(id, 'hover');
        },

        setMarkerUnhover: function (id) {
            if (selectedId === id) return;
            setMarkerState(id, 'normal');
        },

        selectMarker: function (id) {
            if (selectedId !== null && selectedId !== id) {
                setMarkerState(selectedId, 'normal');
            }
            selectedId = id;
            setMarkerState(id, 'selected');
        },

        deselectMarker: function () {
            if (selectedId !== null) {
                setMarkerState(selectedId, 'normal');
                selectedId = null;
            }
        },

        focusMarker: function (id) {
            const m = markers[id];
            if (m) map.panTo(m.getLatLng());
        },

        destroyMap: function () {
            if (map) {
                map.remove();
                map = null;
                markers = {};
                selectedId = null;
            }
        }
    };
})();
window.mapInterop = (function () {
    let map;
    let markers = {};
    let dotNetHelper;
    let selectedId = null;
    let createMapInstance = null;

    const southWest = L.latLng(43.70, 18.05);
    const northEast = L.latLng(43.98, 18.64);
    const bounds = L.latLngBounds(southWest, northEast);
    const center = [43.8447821, 18.3398074];

    function setMarkerState(id, state) {
        const marker = markers[id];
        if (!marker) return;
        const el = marker.getElement();
        if (!el) return;

        el.classList.remove('map-marker-active');
        if (state === 'hover' || state === 'selected') {
            el.classList.add('map-marker-active');
        }
    }

    return {
        initMap: function (helper) {
            dotNetHelper = helper;
            map = L.map('map', {
                center: center,
                zoom: 13,
                minZoom: 12,
                maxZoom: 18,
                maxBounds: bounds,
                maxBoundsViscosity: 1.0
            });
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; OpenStreetMap contributors'
            }).addTo(map);

            map.on('click', function () {
                dotNetHelper.invokeMethodAsync('OnMapClicked');
            });
        },

        initCreateMap: function (helper) {
            createMapInstance = L.map('create-map', {
                center: center,
                zoom: 13,
                minZoom: 12,
                maxZoom: 18,
                maxBounds: bounds,
                maxBoundsViscosity: 1.0
            });

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; OpenStreetMap contributors'
            }).addTo(createMapInstance);

            createMapInstance.on('click', function (e) {
                helper.invokeMethodAsync('OnMapClick', e.latlng.lat, e.latlng.lng);
            });
        },

        destroyCreateMap: function () {
            if (createMapInstance) {
                createMapInstance.remove();
                createMapInstance = null;
            }
        },

        setMarkers: function (markersData) {
            for (var id in markers) {
                if (markers.hasOwnProperty(id)) {
                    map.removeLayer(markers[id]);
                }
            }
            markers = {};
            selectedId = null;

            markersData.forEach(function (data) {
                const icon = L.divIcon({
                    html: `<span class="marker-emoji">${data.emoji || '📍'}</span>`,
                    className: 'map-marker',
                    iconSize: [48, 48],
                    iconAnchor: [24, 24]
                });
                const marker = L.marker([data.latitude, data.longitude], { icon }).addTo(map);
                marker.on('click', function () {
                    dotNetHelper.invokeMethodAsync('OnMarkerClicked', data.id);
                });
                marker.on('mouseover', function () {
                    if (selectedId !== data.id) {
                        setMarkerState(data.id, 'hover');
                    }
                });
                marker.on('mouseout', function () {
                    if (selectedId !== data.id) {
                        setMarkerState(data.id, 'normal');
                    }
                });
                markers[data.id] = marker;
            });
        },

        setMarkerHover: function (id) {
            if (selectedId === id || selectedId !== null) return;
            setMarkerState(id, 'hover');
        },

        setMarkerUnhover: function (id) {
            if (selectedId === id) return;
            setMarkerState(id, 'normal');
        },

        selectMarker: function (id) {
            if (selectedId !== null && selectedId !== id) {
                setMarkerState(selectedId, 'normal');
            }
            selectedId = id;
            setMarkerState(id, 'selected');
        },

        deselectMarker: function () {
            if (selectedId !== null) {
                setMarkerState(selectedId, 'normal');
                selectedId = null;
            }
        },

        focusMarker: function (id) {
            const m = markers[id];
            if (m) map.panTo(m.getLatLng());
        },

        destroyMap: function () {
            if (map) {
                map.remove();
                map = null;
                markers = {};
                selectedId = null;
            }
        }
    };
})();