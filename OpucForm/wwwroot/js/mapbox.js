// wwwroot/js/mapbox.js

let mapboxReady = false;

export function initMapbox() {
  const script = document.createElement("script");
  script.src = "https://api.mapbox.com/search-js/v1.5.0/web.js";
  script.defer = true;
  script.onload = () => {
    mapboxsearch.config.accessToken =
      "pk.eyJ1IjoiaHVudGVyaWRkaW5ncyIsImEiOiJjbWg1bnh6dWMwNDYwMm1wdXp4NzhlYWsxIn0.WuUlOtp28qpJszMEmaZqlg";

    mapboxsearch.autofill({
      options: {
        country: "us",
        state: "or",
      },
    });
  };

  document.body.appendChild(script);
}

/**
 * Called when the user unchecks Same as home address
 * Initializes Mapbox Autofill for mailing address inputs.
 */
export function initMailingAddressAutocomplete() {
  try {
    mapboxsearch.autofill({
      options: {
        country: "us",
        state: "or",
      },
    });
  } catch (err) {
    console.error("Failed to init mailing address autofill:", err);
  }
}
