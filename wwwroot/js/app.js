// Copyright 2016 Google Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

(function() {
  'use strict';

  var app = {
    isLoading: true,
    visibleCards: {},
    selectedAirports: [],
    spinner: document.querySelector('.loader'),
    cardTemplate: document.querySelector('.cardTemplate'),
    container: document.querySelector('.main'),
    addDialog: document.querySelector('#addDialog'),
    removeDialog: document.querySelector('#removeDialog')
  };

  /*****************************************************************************
   *
   * Event listeners for UI elements
   *
   ****************************************************************************/

  document.getElementById('butRefresh').addEventListener('click', function() {
    // Refresh all of the forecasts
    app.updateForecasts();
  });

  document.getElementById('butAdd').addEventListener('click', function() {
    // Open/show the add new city dialog
    app.toggleAddDialog(app.addDialog, true);
  });

  document.getElementById('butRemove').addEventListener('click', function() {
    // Open/show the add new city dialog
    app.toggleAddDialog(app.removeDialog, true);
  });

  document.getElementById('butAddAirport').addEventListener('click', function() {
    // Add the newly selected city
    var input = document.getElementById('selectAirportToAdd');    
    var icao = input.value.toUpperCase();;

    if (icao != '')
      {
        if (!app.selectedAirports) {
          app.selectedAirports = [];
        }
        var buttonRemove = document.querySelector("#butRemove");
        buttonRemove.hidden = false;

        app.getForecast(icao);

        app.selectedAirports.push({icao: icao});
        app.saveselectedAirports();        
        document.getElementById('selectAirportToAdd').value = '';
      }
    
    app.toggleAddDialog(app.addDialog, false);
  });

  document.getElementById('butRemoveAirport').addEventListener('click', function() {
    var result = confirm("Want to delete?");
    if (result) {
        var icao = document.querySelector("#selectRemove").value;
        var visibleCards = app.visibleCards;

        app.selectedAirports = app.selectedAirports.filter(item => item.icao !== icao);

        app.saveselectedAirports();

        app.initialFunction();
    }
    app.toggleAddDialog(app.removeDialog, false);
  });

  document.getElementById('butAddCancel').addEventListener('click', function() {
    // Close the add new city dialog
    app.toggleAddDialog(app.addDialog, false);
  });

  document.getElementById('butRemoveCancel').addEventListener('click', function() {
    // Close the add new city dialog
    app.toggleAddDialog(app.removeDialog, false);
  });

  /*****************************************************************************
   *
   * Methods to update/refresh the UI
   *
   ****************************************************************************/

  // Toggles the visibility of the add new city dialog.
  app.toggleAddDialog = function(dialog, visible) {
    if (visible) {
      if (dialog.id == "removeDialog")
        {
          var selectedAirports = app.selectedAirports;
          var selectRemove = document.querySelector("#selectRemove");

          selectRemove.innerHTML = "";

          selectedAirports.forEach(function(element) {            
            selectRemove.innerHTML += "<option>" + element.icao + "</option'>";
          });
        }      

      dialog.classList.add('dialog-container--visible');
    } else {
      dialog.classList.remove('dialog-container--visible');
    }
  };

  // Updates a weather card with the latest weather forecast. If the card
  // doesn't already exist, it's cloned from the template.
  app.updateForecastCard = function(data) {
    var dataLastUpdated = new Date(data.created);
    //var sunrise = data.channel.astronomy.sunrise;
    //var sunset = data.channel.astronomy.sunset;
    //var current = data.channel.item.condition;
    //var humidity = data.channel.atmosphere.humidity;
    //v1ar wind = data.channel.wind;    

    var weather = data.met[0];
    var info = data.info[0];

    var card = app.visibleCards[weather.loc];
    if (!card) {
      card = app.cardTemplate.cloneNode(true);
      card.classList.remove('cardTemplate');
      card.querySelector('.name').textContent = info.name;
      card.querySelector('.location').textContent = weather.loc;
      card.removeAttribute('hidden');
      app.container.appendChild(card);
      app.visibleCards[weather.loc] = card;
    }

    // Verifies the data provide is newer than what's already visible
    // on the card, if it's not bail, if it is, continue and update the
    // time saved in the card
    var cardLastUpdatedElem = card.querySelector('.card-last-updated');
    var cardLastUpdated = cardLastUpdatedElem.textContent;
    if (cardLastUpdated) {
      cardLastUpdated = new Date(cardLastUpdated);
      // Bail if the card has more recent data then the data
      if (dataLastUpdated.getTime() < cardLastUpdated.getTime()) {
        return;
      }
    }
    cardLastUpdatedElem.textContent = weather.created;

    var metar = weather.metar.toString().slice(13);

    if (metar.indexOf("SPECI") >= 0)
      {
        metar = metar.substring(metar.indexOf("SPECI"), metar.lenght);
      }

    var taf = weather.taf.toString().slice(13);

    card.querySelector('.metar').textContent = metar;
    card.querySelector('.taf').textContent = taf;

    card.querySelector('.linkCharts').textContent = "Charts";
    card.querySelector('.linkCharts').setAttribute('href', "/Home/Charts?icao=" + weather.loc);

    card.querySelector('.linkNotam').textContent = "Notam";
    card.querySelector('.linkNotam').setAttribute('href', "/Home/Notam?icao=" + weather.loc);

    if (app.isLoading)
    {
      app.spinner.setAttribute('hidden', true);
      app.container.removeAttribute('hidden');
      app.isLoading = false;
    }
  };

  /*****************************************************************************
   *
   * Methods for dealing with the model
   *
   ****************************************************************************/

  /*
   * Gets a forecast for a specific city and updates the card with the data.
   * getForecast() first checks if the weather data is in the cache. If so,
   * then it gets that data and populates the card with the cached data.
   * Then, getForecast() goes to the network for fresh data. If the network
   * request goes through, then the card gets updated a second time with the
   * freshest data.
   */
  app.getForecast = function(icao) {
    var url = '/Weather/' + icao;
    // TODO add cache logic here
    if ('caches' in window) {
      /*
       * Check if the service worker has already cached this city's weather
       * data. If the service worker has the data, then display the cached
       * data while the app fetches the latest data.
       */
      caches.match(url).then(function(response) {
        if (response) {
          response.json().then(function updateFromCache(json) {
            var results = json;
            if (results.info[0].aeroCode == undefined)
              {
                alert("Invalid Airport");
              }
            else
              {
                app.updateForecastCard(results);
              }
            // results.loc = json.met[0].loc;
            // results.metar = json.met[0].metar;
            // results.taf = json.met[0].taf;
            
            // var metar = json.met[0].metar.toString();

            // var year = metar.substring(0, 4);
            // var month = metar.substring(4, 6);
            // var day = metar.substring(6, 8);
            // var hour = metar.substring(8, 10);

            // var date = new Date(year+"-"+month+"-"+day+" "+hour+":00");

            // results.created = date.toISOString();            
          });
        }
      });
    }
    // Fetch the latest data.
    var request = new XMLHttpRequest();
    request.onreadystatechange = function() {
      if (request.readyState === XMLHttpRequest.DONE) {
        if (request.status === 200) {
          var response = JSON.parse(request.response);
          var results = response;
          if (results.info[0].aeroCode == undefined)
            {
              alert("Invalid Airport");
            }
          else
            {
              app.updateForecastCard(results);
            }
          // results.loc = icao;
          // results.metar = response.met[0].metar;
          // results.taf = response.met[0].taf;

          // var metar = response.met[0].metar.toString();

          // var year = metar.substring(0, 4);
          // var month = metar.substring(4, 6);
          // var day = metar.substring(6, 8);
          // var hour = metar.substring(8, 10);

          // var strDate = year+"-"+month+"-"+day+" "+hour+":00"//initialWeatherForecast.created;//;

          // var date = new Date(strDate);

          // results.created = date.toISOString();
        }
      }
      // else {
      //   // Return the initial weather forecast since no data is available.
      //   app.updateForecastCard(initialWeatherForecast);
      // }
    };
    request.open('GET', url);
    request.send();
  };

  // Iterate all of the cards and attempt to get the latest forecast data
  app.updateForecasts = function() {
    var keys = Object.keys(app.visibleCards);
    keys.forEach(function(key) {
      app.getForecast(key);
    });
  };

  // TODO add saveselectedAirports function here
  // Save list of cities to localStorage.
  app.saveselectedAirports = function() 
  {
    var selectedAirports = JSON.stringify(app.selectedAirports);
    localStorage.selectedAirports = selectedAirports;
  };

  /*
   * Fake weather data that is presented when the user first uses the app,
   * or when the user has not saved any cities. See startup code for more
   * discussion.
   */
  // var initialWeatherForecast = 
  // {
  //   info:
  //     [
  //       {
  //         name: "CAMPO DE MARTE"
  //       }
  //     ],
  //   met:    
  //     [
  //       {
  //         loc: 'SBMT',
  //         metar: "METAR",
  //         taf: "TAF",
  //         created: '2016-07-22T01:00:00Z'
  //       }        
  //     ]
  // };
  // TODO uncomment line below to test app with fake data
  // app.updateForecastCard(initialWeatherForecast);

  /************************************************************************
   *
   * Code required to start the app
   *
   * NOTE: To simplify this codelab, we've used localStorage.
   *   localStorage is a synchronous API and has serious performance
   *   implications. It should not be used in production applications!
   *   Instead, check out IDB (https://www.npmjs.com/package/idb) or
   *   SimpleDB (https://gist.github.com/inexorabletash/c8069c042b734519680c)
   ************************************************************************/

  // TODO add startup code here
  app.initialFunction = function(){
    app.selectedAirports = localStorage.selectedAirports;

    if (app.selectedAirports != undefined)
    {      
      var buttonRemove = document.querySelector("#butRemove");
      buttonRemove.hidden = false;
      app.selectedAirports = JSON.parse(app.selectedAirports);
      app.selectedAirports.forEach(function(airport) 
      {
        app.getForecast(airport.icao);
      });
    } 
  };

  app.initialFunction();
  // else 
  // {
  //   /* The user is using the app for the first time, or the user has not
  //    * saved any cities, so show the user some fake data. A real app in this
  //    * scenario could guess the user's location via IP lookup and then inject
  //    * that data into the page.
  //    */
  //   app.updateForecastCard(initialWeatherForecast);
  //   app.selectedAirports = [ {icao: initialWeatherForecast.met[0].loc} ];
  //   app.saveselectedAirports();
  // }

  // TODO add service worker code here
  if ('serviceWorker' in navigator) 
  {
    navigator.serviceWorker
             .register('./service-worker.js')
             .then(function() { console.log('Service Worker Registered'); });
  }
})();
